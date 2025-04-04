using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public partial class Form1 : Form
    {
        private readonly ProgressManager progressManager = new ProgressManager();
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
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                // Объединяем bmp и txt в один фильтр
                dialog.Filter = "Image and Text Files (*.bmp;*.txt)|*.bmp;*.txt|Compressed Files (*.lzw;*.lzw2)|*.lzw;*.lzw2";
                if (dialog.ShowDialog() == DialogResult.OK)
                    listBoxFiles.Items.AddRange(dialog.FileNames);
            }
        }

        private async void btnCompress_ClickAsync(object sender, EventArgs e)
        {
            var files = GetValidFiles(".bmp", ".txt");
            if (files.Count == 0) return;

            try
            {
                List<InputData> datas = new List<InputData>();
                List<InputData78> lzwResults = new List<InputData78>();

                foreach (var file in files)
                {
                    currentStage = "Primary Compression";
                    var inputLZW = new InputData(File.ReadAllBytes(file));

                    datas.Add(inputLZW);
                }

                Console.WriteLine("---------------");
                progressManager.Start(datas);

                await Factory1(files, datas, lzwResults);

                progressManager.Stop();

                if (!radioSecondaryCompression.Checked) return;

                progressManager.Start(lzwResults);

                await Factory2(files, lzwResults);

                progressManager.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Factory2(List<string> files, List<InputData78> lzwResults)
        {
            await Task.Factory.StartNew(() =>
                Parallel.For(0, lzwResults.Count, i =>
                {
                    byte[] finalResult = LZ78.Compress(lzwResults[i]);
                    string outputFilePath = Path.ChangeExtension(files[i], Path.GetExtension(files[i]) + ".lzw2");
                    File.WriteAllBytes(outputFilePath, finalResult);
                })
            );
        }

        private async Task Factory1(List<string> files, List<InputData> datas, List<InputData78> lzwResults)
        {
            await Task.Factory.StartNew(() =>
                Parallel.For(0, datas.Count, i =>
                {
                    byte[] lzwResult = LZW.Compress(datas[i]);

                    string outputFilePath = Path.ChangeExtension(files[i], Path.GetExtension(files[i]) + ".lzw");

                    if (radioSecondaryCompression.Checked)
                    {
                        var inputLZ78 = new InputData78(lzwResult);
                        lzwResults.Add(inputLZ78);
                    }
                    else
                    {
                        File.WriteAllBytes(outputFilePath, lzwResult);
                    }
                })
            );
        }

        private List<string> GetValidFiles(params string[] validExtensions)
        {
            var validFiles = new List<string>();
            foreach (var item in listBoxFiles.Items)
            {
                string path = item.ToString();
                bool valid = validExtensions.Any(ext => Path.GetExtension(path).Equals(ext, StringComparison.OrdinalIgnoreCase));
                if (valid)
                    validFiles.Add(path);
                else
                    MessageBox.Show($"Invalid file: {path}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return validFiles;
        }

        private async void btnDecompress_Click(object sender, EventArgs e)
        {
            //string ext = radioSecondaryCompression.Checked ? ".lzw2" : ".lzw";
            //var files = GetValidFiles(ext);
            //if (files.Count == 0) return;

            //try
            //{
            //    foreach (var file in files)
            //    {
            //        byte[] data = File.ReadAllBytes(file);

            //        ThreadPool.QueueUserWorkItem(_ =>
            //        {
            //            try
            //            {
            //                if (radioSecondaryCompression.Checked)
            //                {
            //                    currentStage = "Secondary Decompression";
            //                    var outDataLZ78 = new OutData78(ConvertBytesToTuples(data));
            //                    progressManager.AddTask(outDataLZ78);
            //                    byte[] lz78Result = LZ78.Decompress(outDataLZ78);

            //                    currentStage = "Primary Decompression";
            //                    var outDataLZW = new OutData(BytesToIntegers(lz78Result));
            //                    progressManager.AddTask(outDataLZW);
            //                    byte[] finalResult = LZW.Decompress(outDataLZW);
            //                    File.WriteAllBytes(Path.ChangeExtension(file, Path.GetExtension(file).Replace(".lzw2", ".bmp")), finalResult);
            //                }
            //                else
            //                {
            //                    currentStage = "Primary Decompression";
            //                    var outDataLZW = new OutData(BytesToIntegers(data));
            //                    progressManager.AddTask(outDataLZW);
            //                    byte[] result = LZW.Decompress(outDataLZW);
            //                    File.WriteAllBytes(Path.ChangeExtension(file, Path.GetExtension(file).Replace(".lzw", ".bmp")), result);
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

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
