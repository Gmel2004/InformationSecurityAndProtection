using System;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool isEncryptText = true;
        private const int bitLenght = 16;
        private RSA RSA = new RSA(bitLenght);

        private void FromText_Changed(object sender, EventArgs e)
        {
            ToText.ForeColor = Color.Black;
            ToText.Font = new Font(ToText.Font.FontFamily, 14);

            ToText.Text = isEncryptText ?
                EncryptText(FromText.Text) :
                DecryptText(FromText.Text);

            // Return try catch
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            isEncryptText = !isEncryptText;
            FromText.Text = ToText.Text;
            string c = FromTextLabel.Text;
            FromTextLabel.Text = ToTextLabel.Text;
            ToTextLabel.Text = c;
        }

        private string EncryptText(string text)
        {
            return RSA.Encrypt(text);
        }

        private string DecryptText(string text)
        {
            return RSA.Decrypt(text);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateNewKeysButton_Click(object sender, EventArgs e)
        {
            RSA = new RSA(bitLenght);
            FromText_Changed(this, e); //
        }
    }
}
