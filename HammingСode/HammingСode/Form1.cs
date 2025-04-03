using System;
using System.Windows.Forms;

namespace HammingСode
{
    public partial class Form1 : Form
    {
        const int sizeBlock = 3;
        HammingCode hm;

        public Form1()
        {
            InitializeComponent();
        }

        private void txtOriginalMessage_TextChanged(object sender, EventArgs e)
        {
            txtSandedMessage.Text =
                txtOriginalMessage.Text == string.Empty ?
                "" : hm.AddPartlyBits(txtOriginalMessage.Text);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                txtSandedMessage.Text =
                    txtSandedMessage.Text == string.Empty ?
                    "" : hm.FixError(txtSandedMessage.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            hm = new HammingCode(sizeBlock);
        }
    }
}
