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
        public System.Windows.Forms.RadioButton radioPrimaryCompression;
        public System.Windows.Forms.RadioButton radioSecondaryCompression;
        public System.Windows.Forms.GroupBox groupCompression;

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
            this.radioPrimaryCompression = new System.Windows.Forms.RadioButton();
            this.radioSecondaryCompression = new System.Windows.Forms.RadioButton();
            this.groupCompression = new System.Windows.Forms.GroupBox();

            this.SuspendLayout();

            // btnAddFiles
            this.btnAddFiles.Location = new System.Drawing.Point(20, 20);
            this.btnAddFiles.Size = new System.Drawing.Size(120, 30);
            this.btnAddFiles.Text = "Add Files";
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);

            // btnCompress
            this.btnCompress.Location = new System.Drawing.Point(160, 20);
            this.btnCompress.Size = new System.Drawing.Size(100, 30);
            this.btnCompress.Text = "Compress";
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);

            // btnDecompress
            this.btnDecompress.Location = new System.Drawing.Point(280, 20);
            this.btnDecompress.Size = new System.Drawing.Size(100, 30);
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);

            // listBoxFiles
            this.listBoxFiles.Location = new System.Drawing.Point(20, 70);
            this.listBoxFiles.Size = new System.Drawing.Size(360, 120);
            this.listBoxFiles.AllowDrop = true;

            // groupCompression (GroupBox для выбора степени сжатия)
            this.groupCompression.Location = new System.Drawing.Point(20, 200);
            this.groupCompression.Size = new System.Drawing.Size(360, 60);
            this.groupCompression.Text = "Compression Type";

            // radioPrimaryCompression
            this.radioPrimaryCompression.Location = new System.Drawing.Point(10, 25);
            this.radioPrimaryCompression.Size = new System.Drawing.Size(160, 20);
            this.radioPrimaryCompression.Text = "Primary Compression";
            this.radioPrimaryCompression.Checked = true; // По умолчанию выбрана первичная степень

            // radioSecondaryCompression
            this.radioSecondaryCompression.Location = new System.Drawing.Point(180, 25);
            this.radioSecondaryCompression.Size = new System.Drawing.Size(160, 20);
            this.radioSecondaryCompression.Text = "Secondary Compression";

            // Добавляем радио-кнопки в GroupBox
            this.groupCompression.Controls.Add(this.radioPrimaryCompression);
            this.groupCompression.Controls.Add(this.radioSecondaryCompression);

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(20, 270);
            this.progressBar.Size = new System.Drawing.Size(360, 20);

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(20, 300);
            this.lblStatus.Size = new System.Drawing.Size(360, 20);
            this.lblStatus.Text = "Ready";

            // Form1
            this.ClientSize = new System.Drawing.Size(400, 340);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.groupCompression);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Text = "LZW Compressor";
            this.ResumeLayout(false);
        }
    }
}
