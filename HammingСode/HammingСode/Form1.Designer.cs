namespace HammingСode
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblOriginalMessage = new System.Windows.Forms.Label();
            this.txtOriginalMessage = new System.Windows.Forms.TextBox();
            this.lblSandedMessage = new System.Windows.Forms.Label();
            this.txtSandedMessage = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOriginalMessage
            // 
            this.lblOriginalMessage.AutoSize = true;
            this.lblOriginalMessage.Location = new System.Drawing.Point(27, 25);
            this.lblOriginalMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOriginalMessage.Name = "lblOriginalMessage";
            this.lblOriginalMessage.Size = new System.Drawing.Size(116, 16);
            this.lblOriginalMessage.TabIndex = 0;
            this.lblOriginalMessage.Text = "Original message:";
            // 
            // txtOriginalMessage
            // 
            this.txtOriginalMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOriginalMessage.Location = new System.Drawing.Point(27, 49);
            this.txtOriginalMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtOriginalMessage.Name = "txtOriginalMessage";
            this.txtOriginalMessage.Size = new System.Drawing.Size(479, 22);
            this.txtOriginalMessage.TabIndex = 1;
            this.txtOriginalMessage.TextChanged += new System.EventHandler(this.txtOriginalMessage_TextChanged);
            // 
            // lblSandedMessage
            // 
            this.lblSandedMessage.AutoSize = true;
            this.lblSandedMessage.Location = new System.Drawing.Point(27, 98);
            this.lblSandedMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSandedMessage.Name = "lblSandedMessage";
            this.lblSandedMessage.Size = new System.Drawing.Size(209, 16);
            this.lblSandedMessage.TabIndex = 2;
            this.lblSandedMessage.Text = "Message with parity bits (sanded):";
            // 
            // txtSandedMessage
            // 
            this.txtSandedMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSandedMessage.Location = new System.Drawing.Point(27, 123);
            this.txtSandedMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtSandedMessage.Name = "txtSandedMessage";
            this.txtSandedMessage.Size = new System.Drawing.Size(479, 22);
            this.txtSandedMessage.TabIndex = 3;
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Location = new System.Drawing.Point(184, 174);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(160, 37);
            this.btnCheck.TabIndex = 4;
            this.btnCheck.Text = "Check sanded message";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 234);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtSandedMessage);
            this.Controls.Add(this.lblSandedMessage);
            this.Controls.Add(this.txtOriginalMessage);
            this.Controls.Add(this.lblOriginalMessage);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(394, 260);
            this.Name = "Form1";
            this.Text = "Hamming Code Demo";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOriginalMessage;
        private System.Windows.Forms.TextBox txtOriginalMessage;
        private System.Windows.Forms.Label lblSandedMessage;
        private System.Windows.Forms.TextBox txtSandedMessage;
        private System.Windows.Forms.Button btnCheck;
    }
}

