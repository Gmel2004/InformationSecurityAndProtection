using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public partial class Form1 : Form
    {
        private readonly CompressionProgressManager progressManager = new CompressionProgressManager();
        private string currentStage = "";

        public Form1()
        {
            InitializeComponent();
            progressManager.ProgressUpdated += UpdateProgressBar;
        }

        private void UpdateProgressBar(double progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<double>(UpdateProgressBar), progress);
                return;
            }

            progressBar.Value = (int)Math.Round(progress);
            lblStatus.Text = $"{currentStage}: {progress:F2}%";
            Console.WriteLine("fee");
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                dialog.Filter = "GIF Files|*.gif|LZW Files|*.lzw;*.lzw2";
                if (dialog.ShowDialog() == DialogResult.OK)
                    listBoxFiles.Items.AddRange(dialog.FileNames);
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            var files = GetValidFiles(".gif");
            if (files.Count == 0) return;

            progressManager.Start();

            try
            {
                foreach (var file in files)
                {
                    // Основное сжатие (LZW)
                    currentStage = "Primary Compression";
                    var inputLZW = new InputData(File.ReadAllBytes(file));
                    progressManager.AddTask(inputLZW);

                    // Используем поток для сжатия
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try
                        {
                            byte[] lzwResult = LZW.Compress(inputLZW);
                            string outputFilePath = Path.ChangeExtension(file, ".lzw");

                            if (radioSecondaryCompression.Checked)
                            {
                                // Вторичное сжатие (LZ78)
                                currentStage = "Secondary Compression";
                                var inputLZ78 = new InputData78(lzwResult);  // Используем InputData78 для LZ78
                                progressManager.AddTask(inputLZ78);
                                byte[] finalResult = LZ78.Compress(inputLZ78);
                                File.WriteAllBytes(Path.ChangeExtension(file, ".lzw2"), finalResult);
                            }
                            else
                            {
                                File.WriteAllBytes(outputFilePath, lzwResult);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    progressManager.Stop();
            //}
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            string ext = radioSecondaryCompression.Checked ? ".lzw2" : ".lzw";
            var files = GetValidFiles(ext);
            if (files.Count == 0) return;

            progressManager.Start();

            try
            {
                foreach (var file in files)
                {
                    byte[] data = File.ReadAllBytes(file);

                    // Используем поток для распаковки
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try
                        {
                            if (radioSecondaryCompression.Checked)
                            {
                                // Вторичное распаковывание (LZ78)
                                currentStage = "Secondary Decompression";
                                var outDataLZ78 = new OutData78(ConvertBytesToTuples(data));
                                progressManager.AddTask(outDataLZ78);
                                byte[] lz78Result = LZ78.Decompress(outDataLZ78);

                                // Первичное распаковывание (LZW)
                                currentStage = "Primary Decompression";
                                var outDataLZW = new OutData(BytesToIntegers(lz78Result));
                                progressManager.AddTask(outDataLZW);
                                byte[] finalResult = LZW.Decompress(outDataLZW);
                                File.WriteAllBytes(Path.ChangeExtension(file, ".gif"), finalResult);
                            }
                            else
                            {
                                // Первичное распаковывание (LZW)
                                currentStage = "Primary Decompression";
                                var outDataLZW = new OutData(BytesToIntegers(data));
                                progressManager.AddTask(outDataLZW);
                                byte[] result = LZW.Decompress(outDataLZW);
                                File.WriteAllBytes(Path.ChangeExtension(file, ".gif"), result);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    progressManager.Stop();
            //}
        }

        private List<string> GetValidFiles(string extension)
        {
            var validFiles = new List<string>();
            foreach (var item in listBoxFiles.Items)
            {
                string path = item.ToString();
                if (Path.GetExtension(path).Equals(extension, StringComparison.OrdinalIgnoreCase))
                    validFiles.Add(path);
                else
                    MessageBox.Show($"Invalid file: {path}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return validFiles;
        }

        // Преобразование байтов в кортежи (индекс, байт)
        private static List<(int, byte)> ConvertBytesToTuples(byte[] input)
        {
            List<(int, byte)> tuples = new List<(int, byte)>();
            for (int i = 0; i < input.Length; i += 3)  // Каждые 3 байта: индекс и символ
            {
                int index = (input[i] << 8) | input[i + 1];
                byte value = input[i + 2];
                tuples.Add((index, value));
            }
            return tuples;
        }

        private static List<int> BytesToIntegers(byte[] input)
        {
            List<int> integers = new List<int>();
            for (int i = 0; i < input.Length; i += 4)
                integers.Add(BitConverter.ToInt32(input, i));
            return integers;
        }
    }
}
