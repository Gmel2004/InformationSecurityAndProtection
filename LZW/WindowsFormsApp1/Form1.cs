using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public partial class Form1 : Form
    {
        private readonly CompressionProgressManager progressManager =
            new CompressionProgressManager();

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
            lblStatus.Text = $"Выполнено: {progress:F2}%";
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "GIF Files|*.gif|LZW Files|*.lzw";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxFiles.Items.AddRange(openFileDialog.FileNames);
                }
            }
        }

        private static List<int> BytesToIntegers(byte[] input)
        {
            List<int> integers = new List<int>();
            for (int i = 0; i < input.Length; i += 4)
            {
                integers.Add(BitConverter.ToInt32(input, i));
            }
            return integers;
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            var filesForCompress = new List<string>();
            foreach (var filePath in listBoxFiles.Items)
            {
                if (Path.GetExtension(filePath.ToString()).ToLower() != ".gif")
                {
                    MessageBox.Show($"Cannot compress not gif file  {filePath}");
                }
                else
                {
                    filesForCompress.Add(filePath.ToString());
                }
            }

            if (filesForCompress.Count == 0)
            {
                return;
            }

            progressManager.Start();
            foreach (var file in filesForCompress)
            {
                var inputData = new InputData(File.ReadAllBytes(file));
                progressManager.AddTask(inputData);

                System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        byte[] compressed = LZW.Compress(inputData);
                        File.WriteAllBytes
                        (
                            Path.ChangeExtension(file.ToString(), ".lzw"), compressed
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            var filesForDecompress = new List<string>();
            foreach (var filePath in listBoxFiles.Items)
            {
                if (Path.GetExtension(filePath.ToString()).ToLower() != ".lzw")
                {
                    MessageBox.Show($"Cannot decompress not lzw file  {filePath}");
                }
                else
                {
                    filesForDecompress.Add(filePath.ToString());
                }
            }

            if (filesForDecompress.Count == 0)
            {
                return;
            }

            progressManager.Start();
            foreach (var file in filesForDecompress)
            {
                if (Path.GetExtension(file).ToLower() != ".lzw") continue;
                var bytes = File.ReadAllBytes(file);
                var compressedData = new OutData(BytesToIntegers(bytes));
                progressManager.AddTask(compressedData);

                System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                {
                    byte[] decompressed = LZW.Decompress(compressedData);
                    File.WriteAllBytes
                    (
                        Path.ChangeExtension(file, ".gif"), decompressed
                    );
                });
            }
        }
    }
}