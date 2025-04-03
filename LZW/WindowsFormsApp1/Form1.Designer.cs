namespace LZWCompressor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.Button btnAddFiles;
        public System.Windows.Forms.Button btnCompress;
        public System.Windows.Forms.Button btnDecompress;
        public System.Windows.Forms.ListBox listBoxFiles;
        public System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Label lblStatus;

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
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnCompress = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnAddFiles
            this.btnAddFiles.Location = new System.Drawing.Point(20, 20);
            this.btnAddFiles.Size = new System.Drawing.Size(120, 30);
            this.btnAddFiles.Text = "Добавить файлы";
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);

            // btnCompress
            this.btnCompress.Location = new System.Drawing.Point(160, 20);
            this.btnCompress.Size = new System.Drawing.Size(100, 30);
            this.btnCompress.Text = "Сжать";
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);

            // btnDecompress
            this.btnDecompress.Location = new System.Drawing.Point(280, 20);
            this.btnDecompress.Size = new System.Drawing.Size(100, 30);
            this.btnDecompress.Text = "Распаковать";
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);

            // listBoxFiles
            this.listBoxFiles.Location = new System.Drawing.Point(20, 70);
            this.listBoxFiles.Size = new System.Drawing.Size(360, 150);
            this.listBoxFiles.AllowDrop = true;

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(20, 240);
            this.progressBar.Size = new System.Drawing.Size(360, 20);

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(20, 270);
            this.lblStatus.Size = new System.Drawing.Size(360, 20);
            this.lblStatus.Text = "Готово к работе";

            // MainForm
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Text = "LZW Compressor";
            this.ResumeLayout(false);
        }
    }
}