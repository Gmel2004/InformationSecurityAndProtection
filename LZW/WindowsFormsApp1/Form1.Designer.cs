namespace LZWCompressor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.RadioButton btnCompressTwice;

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
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCompress = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnCompressTwice = new System.Windows.Forms.RadioButton();
            this.buttonClearAllFiles = new System.Windows.Forms.Button();
            this.selectAllFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(12, 12);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(75, 23);
            this.btnAddFiles.TabIndex = 0;
            this.btnAddFiles.Text = "Add Files";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 16;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 41);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFiles.Size = new System.Drawing.Size(279, 180);
            this.listBoxFiles.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 231);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(279, 23);
            this.progressBar.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 257);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 3;
            // 
            // btnCompress
            // 
            this.btnCompress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompress.Location = new System.Drawing.Point(12, 275);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(75, 23);
            this.btnCompress.TabIndex = 4;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = true;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_ClickAsync);
            // 
            // btnDecompress
            // 
            this.btnDecompress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDecompress.Location = new System.Drawing.Point(93, 275);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(105, 23);
            this.btnDecompress.TabIndex = 5;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.UseVisualStyleBackColor = true;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // btnCompressTwice
            // 
            this.btnCompressTwice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompressTwice.AutoSize = true;
            this.btnCompressTwice.Location = new System.Drawing.Point(15, 304);
            this.btnCompressTwice.Name = "btnCompressTwice";
            this.btnCompressTwice.Size = new System.Drawing.Size(177, 20);
            this.btnCompressTwice.TabIndex = 6;
            this.btnCompressTwice.Text = "Secondary Compression";
            this.btnCompressTwice.UseVisualStyleBackColor = true;
            this.btnCompressTwice.CheckedChanged += new System.EventHandler(this.btnCompressTwice_CheckedChanged);
            // 
            // buttonClearAllFiles
            // 
            this.buttonClearAllFiles.Location = new System.Drawing.Point(195, 12);
            this.buttonClearAllFiles.Name = "buttonClearAllFiles";
            this.buttonClearAllFiles.Size = new System.Drawing.Size(96, 23);
            this.buttonClearAllFiles.TabIndex = 7;
            this.buttonClearAllFiles.Text = "Clear all";
            this.buttonClearAllFiles.UseVisualStyleBackColor = true;
            this.buttonClearAllFiles.Click += new System.EventHandler(this.btnClearAllFiles_Click);
            // 
            // selectAllFiles
            // 
            this.selectAllFiles.Location = new System.Drawing.Point(93, 12);
            this.selectAllFiles.Name = "selectAllFiles";
            this.selectAllFiles.Size = new System.Drawing.Size(96, 23);
            this.selectAllFiles.TabIndex = 8;
            this.selectAllFiles.Text = "Select All";
            this.selectAllFiles.UseVisualStyleBackColor = true;
            this.selectAllFiles.Click += new System.EventHandler(this.btnSelectAllFiles_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(295, 340);
            this.Controls.Add(this.selectAllFiles);
            this.Controls.Add(this.buttonClearAllFiles);
            this.Controls.Add(this.btnCompressTwice);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.btnAddFiles);
            this.Name = "Form1";
            this.Text = "LZW&LZ78 Compression";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonClearAllFiles;
        private System.Windows.Forms.Button selectAllFiles;
    }
}
