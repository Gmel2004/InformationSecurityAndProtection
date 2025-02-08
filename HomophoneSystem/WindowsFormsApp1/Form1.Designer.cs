namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextFrom = new System.Windows.Forms.Label();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.ToText = new System.Windows.Forms.RichTextBox();
            this.FromText = new System.Windows.Forms.RichTextBox();
            this.TextTo = new System.Windows.Forms.Label();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextFrom
            // 
            this.TextFrom.AutoSize = true;
            this.TextFrom.Location = new System.Drawing.Point(178, 93);
            this.TextFrom.Name = "TextFrom";
            this.TextFrom.Size = new System.Drawing.Size(76, 16);
            this.TextFrom.TabIndex = 1;
            this.TextFrom.Text = "Original text";
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.Location = new System.Drawing.Point(362, 90);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(75, 23);
            this.ReplaceButton.TabIndex = 2;
            this.ReplaceButton.Text = "Replace";
            this.ReplaceButton.UseVisualStyleBackColor = true;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // ToText
            // 
            this.ToText.Enabled = false;
            this.ToText.Location = new System.Drawing.Point(401, 144);
            this.ToText.Name = "ToText";
            this.ToText.Size = new System.Drawing.Size(350, 150);
            this.ToText.TabIndex = 4;
            this.ToText.Text = "";
            // 
            // FromText
            // 
            this.FromText.Location = new System.Drawing.Point(46, 144);
            this.FromText.Name = "FromText";
            this.FromText.Size = new System.Drawing.Size(350, 150);
            this.FromText.TabIndex = 5;
            this.FromText.Text = "";
            // 
            // TextTo
            // 
            this.TextTo.AutoSize = true;
            this.TextTo.Location = new System.Drawing.Point(551, 93);
            this.TextTo.Name = "TextTo";
            this.TextTo.Size = new System.Drawing.Size(91, 16);
            this.TextTo.TabIndex = 6;
            this.TextTo.Text = "Encrypted text";
            // 
            // ConvertButton
            // 
            this.ConvertButton.Location = new System.Drawing.Point(46, 300);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(705, 60);
            this.ConvertButton.TabIndex = 7;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.TextTo);
            this.Controls.Add(this.FromText);
            this.Controls.Add(this.ToText);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.TextFrom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TextFrom;
        private System.Windows.Forms.Button ReplaceButton;
        private System.Windows.Forms.RichTextBox ToText;
        private System.Windows.Forms.RichTextBox FromText;
        private System.Windows.Forms.Label TextTo;
        private System.Windows.Forms.Button ConvertButton;
    }
}

