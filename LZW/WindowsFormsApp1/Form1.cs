using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LZWCompressor
{
    public partial class Form1 : Form
    {
        private ProgressManager progressManager =
            new ProgressManager();
        private LZW LZW = new LZW();
        private LZ78 LZ78 = new LZ78();

        private string currentStage = "";
        private bool isSecondCompress = false;
        private bool isProcessStoped = true;

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

                dialog.Filter =
                    "Image and Text Files (*.bmp;*.txt)|*.bmp;*.txt|" +
                    "Compressed Files (*.lzw;*.lzw2)|*bmp.lzw;*bmp.lzw2;" +
                    "*txt.lzw;*txt.lzw2";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxFiles.Items.AddRange(dialog.FileNames);
                }
            }
        }

        private async void btnCompress_ClickAsync(object sender, EventArgs e)
        {
            if (!isProcessStoped)
            {
                MessageBox.Show("Wait for the operation to end");
                return;
            }

            isProcessStoped = false;
            var validFiles = GetValidFiles(".bmp", ".txt");

            if (validFiles.Count == 0)
            {
                isProcessStoped = true;
                return;
            }

            try
            {
                if (isSecondCompress)
                {
                    await CompressTwice(validFiles);
                }
                else
                {
                    await CompressOnce(validFiles);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    (
                        $"Error: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }

            isProcessStoped = true;
        }

        private async void btnDecompress_Click(object sender, EventArgs e)
        {
            if (!isProcessStoped)
            {
                MessageBox.Show("Wait for the operation to end");
                return;
            }

            isProcessStoped = false;
            var validFiles = GetValidFiles(".lzw", ".lzw2");

            List<string> lzwFiles = new List<string>();
            List<string> lz78Files = new List<string>();

            foreach (var i in validFiles)
            {
                if
                (
                    Path.GetExtension(i).
                    Equals(".lzw", StringComparison.OrdinalIgnoreCase)
                )
                {
                    lzwFiles.Add(i);
                }
                else
                {
                    lz78Files.Add(i);
                }
            }

            try
            {
                if (lzwFiles.Count > 0)
                {
                    await DecompressOnce(lzwFiles);
                }

                if (lz78Files.Count > 0)
                {
                    await DecompressTwice(lz78Files);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    (
                        $"Error: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }

            isProcessStoped = true;
        }

        private void btnCompressTwice_CheckedChanged(object sender, EventArgs e)
        {
            if (isProcessStoped)
            {
                isSecondCompress = btnCompressTwice.Checked;
            }
            else
            {
                btnCompressTwice.Checked = isSecondCompress;
            }
        }

        private void btnClearAllFiles_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
        }

        private void btnSelectAllFiles_Click(object sender, EventArgs e)
        {
            listBoxFiles.SelectedItems.Clear();
            for (int i = 0; i < listBoxFiles.Items.Count; i++)
            {
                listBoxFiles.SelectedItems.Add(listBoxFiles.Items[i]);
            }
        }

        #region Decompress
        private async Task DecompressOnce(List<string> lzwFiles)
        {
            var lzwResult = await DecompressByLZW(InitLZWDecompress(lzwFiles));
            CreateDecompressFiles(lzwFiles, lzwResult);
        }

        private async Task DecompressTwice(List<string> lz78Files)
        {
            var lz78Result = await DecompressByLZW78(InitLZ78Decompress(lz78Files));
            var lzwResult = await DecompressByLZW(InitLZWDecompress(lz78Result));
            CreateDecompressFiles(lz78Files, lzwResult);
        }

        private async Task<byte[][]> DecompressByLZW78(List<OutData78> data)
        {
            byte[][] decompressedData = new byte[data.Count][];
            progressManager.Start(data);
            await Task.Run(() =>
                Parallel.For(0, data.Count, i =>
                {
                    lock (decompressedData)
                    {
                        decompressedData[i] = LZ78.Decompress(data[i]);
                    }
                })
            );
            progressManager.Stop();

            return decompressedData;
        }

        private async Task<byte[][]> DecompressByLZW(List<OutData> data)
        {
            byte[][] decompressedData = new byte[data.Count][];
            progressManager.Start(data);
            await Task.Run(() =>
                Parallel.For(0, data.Count, i =>
                {
                    lock (decompressedData)
                    {
                        decompressedData[i] = LZW.Decompress(data[i]);
                    }
                })
            );
            progressManager.Stop();

            return decompressedData;
        }

        private List<OutData> InitLZWDecompress(List<string> lzwFiles)
        {
            currentStage = "LZW Decompression";
            UpdateProgressBar(0);
            Console.WriteLine(File.ReadAllBytes(lzwFiles.First()).Length);
            var data = lzwFiles.
                Select
                (
                    t =>

                    new OutData
                    (
                        BytesToIntegers(File.ReadAllBytes(t))
                    )
                ).ToList();

            return data;
        }

        private List<OutData> InitLZWDecompress(byte[][] lz78Result)
        {
            currentStage = "LZW Decompression";
            UpdateProgressBar(0);
            var data = lz78Result.
                Select
                (
                    t =>
                    new OutData
                    (
                        BytesToIntegers(t)
                    )
                ).ToList();

            return data;
        }

        private List<OutData78> InitLZ78Decompress(List<string> lzwFiles)
        {
            currentStage = "LZ78 Decompression";
            UpdateProgressBar(0);
            var data = lzwFiles.
                Select
                (
                    t =>
                    new OutData78
                    (
                        ConvertBytesToTuples(File.ReadAllBytes(t))
                    )
                ).ToList();

            return data;
        }

        private void CreateDecompressFiles
        (
            List<string> files,
            byte[][] data
        )
        {
            for (int i = 0; i < files.Count; i++)
            {
                string outputFilePath =
                    Path.ChangeExtension
                    (
                        files[i],
                        ""
                    );

                File.WriteAllBytes(outputFilePath, data[i]);
            }
        }
        #endregion

        #region Compress
        private async Task CompressOnce(List<string> files)
        {
            var lzwResult = await CompressByLZW(InitLZWCompress(files));
            CreateCompressFiles(files, lzwResult, ".lzw");
        }

        private async Task CompressTwice(List<string> files)
        {
            var lzwResult = await CompressByLZW(InitLZWCompress(files));
            var lz78Result = await CompressByLZ78(InitLZ78Compress(lzwResult));
            CreateCompressFiles(files, lz78Result, ".lzw2");
        }

        private List<InputData> InitLZWCompress(List<string> files)
        {
            currentStage = "LZW Compression";
            UpdateProgressBar(0);

            return
                files.
                Select
                (
                    t => new InputData(File.ReadAllBytes(t))
                ).
                ToList();
        }

        private List<InputDataLZ78> InitLZ78Compress(byte[][] lzwResult)
        {
            currentStage = "LZ78 Compression";
            UpdateProgressBar(0);
            return lzwResult.Select(t => new InputDataLZ78(t)).ToList();
        }

        private async Task<byte[][]> CompressByLZW(List<InputData> data)
        {
            byte[][] compressedData = new byte[data.Count][];
            progressManager.Start(data);
            await Task.Run(() =>
                Parallel.For(0, data.Count, i =>
                {
                    lock (compressedData)
                    {
                        compressedData[i] = LZW.Compress(data[i]);
                    }
                })
            );
            progressManager.Stop();

            return compressedData;
        }

        private async Task<byte[][]> CompressByLZ78(List<InputDataLZ78> data)
        {
            byte[][] compressedData = new byte[data.Count][];
            progressManager.Start(data);
            await Task.Run(() =>
                Parallel.For(0, data.Count, i =>
                {
                    lock (compressedData)
                    {
                        compressedData[i] = LZ78.Compress(data[i]);
                    }
                })
            );
            progressManager.Stop();

            return compressedData;
        }

        private void CreateCompressFiles
        (
            List<string> files,
            byte[][] data,
            string extension
        )
        {
            for (int i = 0; i < files.Count; i++)
            {
                string outputFilePath =
                    Path.ChangeExtension
                    (
                        files[i],
                        $"{Path.GetExtension(files[i])}{extension}"
                    );

                File.WriteAllBytes(outputFilePath, data[i]);
            }
        }
        #endregion

        #region Helps Methods
        private List<string> GetValidFiles(params string[] validExtensions)
        {
            var validFiles = new List<string>();

            foreach (var item in listBoxFiles.SelectedItems)
            {
                string path = item.ToString();

                bool valid =
                    validExtensions.
                    Any
                    (
                        t =>
                        Path.GetExtension(path).
                        Equals(t, StringComparison.OrdinalIgnoreCase)
                    );

                if (valid)
                {
                    validFiles.Add(path);
                }
                else
                {
                    MessageBox.
                        Show
                        (
                            $"Invalid file: {path}",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                }
            }

            return validFiles;
        }

        private static List<(int, byte)> ConvertBytesToTuples(byte[] input)
        {
            List<(int, byte)> tuples = new List<(int, byte)>();
            for (int i = 0; i < input.Length; i += 3)
            {
                int index = (input[i] << 8) | input[i + 1];
                byte value = input[i + 2];
                tuples.Add((index, value));
            }
            return tuples;
        }

        private static List<int> BytesToIntegers(byte[] input)
        {
            List<int> codes = new List<int>();
            int bitBuffer = 0;
            int bitsInBuffer = 0;
            const int MaxBits = 12;

            foreach (byte b in input)
            {
                bitBuffer |= b << bitsInBuffer;
                bitsInBuffer += 8;

                while (bitsInBuffer >= MaxBits)
                {
                    int code = bitBuffer & ((1 << MaxBits) - 1);
                    codes.Add(code);
                    bitBuffer >>= MaxBits;
                    bitsInBuffer -= MaxBits;
                }
            }

            if (bitsInBuffer >= MaxBits)
            {
                int code = bitBuffer & ((1 << MaxBits) - 1);
                codes.Add(code);
            }

            return codes;
        }
        #endregion
    }
}
