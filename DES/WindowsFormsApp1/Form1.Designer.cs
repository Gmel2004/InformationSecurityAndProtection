using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnAttachFile;
        private Label lblFilePath;
        private Button btnEncrypt;
        private TextBox txtEncryptedText;
        private Button btnDecrypt;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txtEncryptedText = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnAttachFile
            this.btnAttachFile.Location = new System.Drawing.Point(20, 20);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(150, 30);
            this.btnAttachFile.Text = "Прикрепить файл";
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);

            // lblFilePath
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(180, 25);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(0, 15);

            // btnEncrypt
            this.btnEncrypt.Location = new System.Drawing.Point(20, 70);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(150, 30);
            this.btnEncrypt.Text = "Зашифровать";
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);

            // txtEncryptedText
            this.txtEncryptedText.Location = new System.Drawing.Point(20, 120);
            this.txtEncryptedText.Multiline = true;
            this.txtEncryptedText.Name = "txtEncryptedText";
            this.txtEncryptedText.Size = new System.Drawing.Size(400, 150);
            this.txtEncryptedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // btnDecrypt
            this.btnDecrypt.Location = new System.Drawing.Point(20, 280);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(150, 30);
            this.btnDecrypt.Text = "Расшифровать";
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.btnAttachFile);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtEncryptedText);
            this.Controls.Add(this.btnDecrypt);
            this.Name = "MainForm";
            this.Text = "DES Шифрование";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

