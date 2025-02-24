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
            this.GenerateNewKeysButton = new System.Windows.Forms.Button();
            this.ToTextLabel = new System.Windows.Forms.Label();
            this.FromText = new System.Windows.Forms.RichTextBox();
            this.ToText = new System.Windows.Forms.RichTextBox();
            this.CheckForMatchingButton = new System.Windows.Forms.Button();
            this.FromTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GenerateNewKeysButton
            // 
            this.GenerateNewKeysButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenerateNewKeysButton.Location = new System.Drawing.Point(273, 376);
            this.GenerateNewKeysButton.Name = "GenerateNewKeysButton";
            this.GenerateNewKeysButton.Size = new System.Drawing.Size(253, 48);
            this.GenerateNewKeysButton.TabIndex = 13;
            this.GenerateNewKeysButton.Text = "Generate new keys";
            this.GenerateNewKeysButton.UseVisualStyleBackColor = true;
            this.GenerateNewKeysButton.Click += new System.EventHandler(this.GenerateNewKeysButton_Click);
            // 
            // ToTextLabel
            // 
            this.ToTextLabel.AutoSize = true;
            this.ToTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToTextLabel.Location = new System.Drawing.Point(524, 37);
            this.ToTextLabel.Name = "ToTextLabel";
            this.ToTextLabel.Size = new System.Drawing.Size(112, 29);
            this.ToTextLabel.TabIndex = 12;
            this.ToTextLabel.Text = "Message";
            // 
            // FromText
            // 
            this.FromText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FromText.Location = new System.Drawing.Point(48, 88);
            this.FromText.Name = "FromText";
            this.FromText.Size = new System.Drawing.Size(350, 276);
            this.FromText.TabIndex = 11;
            this.FromText.Text = "";
            this.FromText.Click += new System.EventHandler(this.FromText_Click);
            this.FromText.TextChanged += new System.EventHandler(this.FromText_TextChanged);
            // 
            // ToText
            // 
            this.ToText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToText.Location = new System.Drawing.Point(403, 88);
            this.ToText.Name = "ToText";
            this.ToText.ReadOnly = true;
            this.ToText.Size = new System.Drawing.Size(350, 276);
            this.ToText.TabIndex = 10;
            this.ToText.Text = "";
            // 
            // CheckForMatchingButton
            // 
            this.CheckForMatchingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckForMatchingButton.Location = new System.Drawing.Point(323, 9);
            this.CheckForMatchingButton.Name = "CheckForMatchingButton";
            this.CheckForMatchingButton.Size = new System.Drawing.Size(155, 74);
            this.CheckForMatchingButton.TabIndex = 9;
            this.CheckForMatchingButton.Text = "Check for matching";
            this.CheckForMatchingButton.UseVisualStyleBackColor = true;
            this.CheckForMatchingButton.Click += new System.EventHandler(this.CheckForMatchingButton_Click);
            // 
            // FromTextLabel
            // 
            this.FromTextLabel.AutoSize = true;
            this.FromTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FromTextLabel.Location = new System.Drawing.Point(131, 37);
            this.FromTextLabel.Name = "FromTextLabel";
            this.FromTextLabel.Size = new System.Drawing.Size(141, 29);
            this.FromTextLabel.TabIndex = 8;
            this.FromTextLabel.Text = "Original text";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GenerateNewKeysButton);
            this.Controls.Add(this.ToTextLabel);
            this.Controls.Add(this.FromText);
            this.Controls.Add(this.ToText);
            this.Controls.Add(this.CheckForMatchingButton);
            this.Controls.Add(this.FromTextLabel);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenerateNewKeysButton;
        private System.Windows.Forms.Label ToTextLabel;
        private System.Windows.Forms.RichTextBox FromText;
        private System.Windows.Forms.RichTextBox ToText;
        private System.Windows.Forms.Button CheckForMatchingButton;
        private System.Windows.Forms.Label FromTextLabel;
    }
}

