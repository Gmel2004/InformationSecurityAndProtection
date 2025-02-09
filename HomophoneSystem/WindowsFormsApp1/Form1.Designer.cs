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
            this.FromTextLabel = new System.Windows.Forms.Label();
            this.ReplaceButton = new System.Windows.Forms.Button();
            this.ToText = new System.Windows.Forms.RichTextBox();
            this.FromText = new System.Windows.Forms.RichTextBox();
            this.ToTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FromTextLabel
            // 
            this.FromTextLabel.AutoSize = true;
            this.FromTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FromTextLabel.Location = new System.Drawing.Point(144, 61);
            this.FromTextLabel.Name = "FromTextLabel";
            this.FromTextLabel.Size = new System.Drawing.Size(141, 29);
            this.FromTextLabel.TabIndex = 1;
            this.FromTextLabel.Text = "Original text";
            // 
            // ReplaceButton
            // 
            this.ReplaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReplaceButton.Location = new System.Drawing.Point(326, 51);
            this.ReplaceButton.Name = "ReplaceButton";
            this.ReplaceButton.Size = new System.Drawing.Size(135, 48);
            this.ReplaceButton.TabIndex = 2;
            this.ReplaceButton.Text = "Replace";
            this.ReplaceButton.UseVisualStyleBackColor = true;
            this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // ToText
            // 
            this.ToText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToText.Location = new System.Drawing.Point(398, 112);
            this.ToText.Name = "ToText";
            this.ToText.ReadOnly = true;
            this.ToText.Size = new System.Drawing.Size(350, 276);
            this.ToText.TabIndex = 4;
            this.ToText.Text = "";
            // 
            // FromText
            // 
            this.FromText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FromText.Location = new System.Drawing.Point(43, 112);
            this.FromText.Name = "FromText";
            this.FromText.Size = new System.Drawing.Size(350, 276);
            this.FromText.TabIndex = 5;
            this.FromText.Text = "";
            this.FromText.TextChanged += new System.EventHandler(this.FromText_Changed);
            // 
            // ToTextLabel
            // 
            this.ToTextLabel.AutoSize = true;
            this.ToTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToTextLabel.Location = new System.Drawing.Point(511, 61);
            this.ToTextLabel.Name = "ToTextLabel";
            this.ToTextLabel.Size = new System.Drawing.Size(164, 29);
            this.ToTextLabel.TabIndex = 6;
            this.ToTextLabel.Text = "Encrypted text";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ToTextLabel);
            this.Controls.Add(this.FromText);
            this.Controls.Add(this.ToText);
            this.Controls.Add(this.ReplaceButton);
            this.Controls.Add(this.FromTextLabel);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FromTextLabel;
        private System.Windows.Forms.Button ReplaceButton;
        private System.Windows.Forms.RichTextBox ToText;
        private System.Windows.Forms.RichTextBox FromText;
        private System.Windows.Forms.Label ToTextLabel;
    }
}

