namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnResOpenFile;

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
            this.components = new System.ComponentModel.Container();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnResOpenFile = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAttachFile.Location = new System.Drawing.Point(4, 4);
            this.btnAttachFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(321, 42);
            this.btnAttachFile.TabIndex = 0;
            this.btnAttachFile.Text = "Choose file";
            this.btnAttachFile.UseVisualStyleBackColor = true;
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEncrypt.Location = new System.Drawing.Point(4, 54);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(4);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(321, 42);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "Encrypt file";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDecrypt.Location = new System.Drawing.Point(4, 104);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(4);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(321, 42);
            this.btnDecrypt.TabIndex = 2;
            this.btnDecrypt.Text = "Decrypt file";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnResOpenFile
            // 
            this.btnResOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResOpenFile.Location = new System.Drawing.Point(4, 154);
            this.btnResOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnResOpenFile.Name = "btnResOpenFile";
            this.btnResOpenFile.Size = new System.Drawing.Size(321, 42);
            this.btnResOpenFile.TabIndex = 3;
            this.btnResOpenFile.Text = "Open Decrypt file";
            this.btnResOpenFile.UseVisualStyleBackColor = true;
            this.btnResOpenFile.Click += new System.EventHandler(this.btnOpenResFile_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenFile.Location = new System.Drawing.Point(4, 204);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(321, 45);
            this.btnOpenFile.TabIndex = 7;
            this.btnOpenFile.Text = "Open original file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnResOpenFile, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnOpenFile, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnEncrypt, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnDecrypt, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnAttachFile, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(97, 71);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(329, 253);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 394);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "DES";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}