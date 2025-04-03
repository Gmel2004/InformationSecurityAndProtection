using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string filePath;
        private DES des;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Error: choose your file");
                return;
            }

            byte[] data = File.ReadAllBytes(filePath);
            Console.Clear();
            Console.WriteLine($"Original bytes: {string.Join(" ", data)}");

            byte[] encryptedData = des.Encrypt(data);

            string encryptedFilePath = filePath + ".enc";
            File.WriteAllBytes(encryptedFilePath, encryptedData);
            Console.WriteLine($"Encrypt bytes: {string.Join(" ", encryptedData)}");
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Error: choose your file");
                return;
            }

            byte[] encryptedData = File.ReadAllBytes(filePath + ".enc");

            byte[] decryptedData = des.Decrypt(encryptedData);

            string decryptedFilePath = filePath + ".dec";
            File.WriteAllBytes(decryptedFilePath, decryptedData);
            Console.WriteLine($"Decrypt bytes: {string.Join(" ", decryptedData)}");
        }

        private void btnOpenResFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Error: choose your file");
                return;
            }

            try
            {
                Process.Start("notepad.exe", filePath + ".dec");
            }
            catch (Exception)
            {
                MessageBox.Show("Error: cannot open file");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            byte[] key = Encoding.ASCII.GetBytes("mykey123");
            des = new DES(key);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Error: choose your file");
                return;
            }

            try
            {
                Process.Start("notepad.exe", filePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: cannot open file");
            }
        }
    }
}