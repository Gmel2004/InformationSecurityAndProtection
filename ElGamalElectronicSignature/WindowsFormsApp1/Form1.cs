using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ELGamal ELGamal = new ELGamal();
        private List<Message> messages = new List<Message>();

        private void ConvertText()
        {
            CheckForMatchingButton.BackColor = Color.White;
            ToText.ForeColor = Color.Black;
            ToText.Font = new Font(ToText.Font.FontFamily, 14);

            try
            {
                messages = ELGamal.GenerateMessages(FromText.Text);
                StringBuilder sb = new StringBuilder();

                foreach (var i in messages)
                {
                    sb.Append($"{i.hash} ({i.s}, {i.r})\n");
                }

                ToText.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                ToText.Text = ex.Message;
                ToText.Font = new Font(ToText.Font.FontFamily, 25);
                ToText.ForeColor = Color.Red;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void CheckForMatchingButton_Click(object sender, EventArgs e)
        {
            CheckForMatchingButton.BackColor =
                ELGamal.CheckMessages(messages) ?
                Color.GreenYellow :
                Color.IndianRed;
        }

        private void GenerateNewKeysButton_Click(object sender, EventArgs e)
        {
            ELGamal = new ELGamal();
            ConvertText();
        }

        private void FromText_TextChanged(object sender, EventArgs e)
        {
            ConvertText();
        }

        private void FromText_Click(object sender, EventArgs e)
        {
            CheckForMatchingButton.BackColor = Color.White;
        }
    }
}
