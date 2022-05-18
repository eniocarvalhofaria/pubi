namespace BusinessIntelligence.Controls
{
    partial class FormTextSelector : FormSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rdStartsWith = new System.Windows.Forms.RadioButton();
            this.rdEndsWith = new System.Windows.Forms.RadioButton();
            this.rdEqual = new System.Windows.Forms.RadioButton();
            this.rdNotEqual = new System.Windows.Forms.RadioButton();
            this.rdNotStartsWith = new System.Windows.Forms.RadioButton();
            this.rdNotEndsWith = new System.Windows.Forms.RadioButton();
            this.rdContains = new System.Windows.Forms.RadioButton();
            this.rdNotContains = new System.Windows.Forms.RadioButton();
            this.txtText = new System.Windows.Forms.TextBox();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdStartsWith
            // 
            this.rdStartsWith.AutoSize = true;
            this.rdStartsWith.Location = new System.Drawing.Point(12, 35);
            this.rdStartsWith.Name = "rdStartsWith";
            this.rdStartsWith.Size = new System.Drawing.Size(87, 17);
            this.rdStartsWith.TabIndex = 0;
            this.rdStartsWith.Text = "Começa com";
            this.rdStartsWith.UseVisualStyleBackColor = true;
            // 
            // rdEndsWith
            // 
            this.rdEndsWith.AutoSize = true;
            this.rdEndsWith.Location = new System.Drawing.Point(12, 58);
            this.rdEndsWith.Name = "rdEndsWith";
            this.rdEndsWith.Size = new System.Drawing.Size(86, 17);
            this.rdEndsWith.TabIndex = 1;
            this.rdEndsWith.Text = "Termina com";
            this.rdEndsWith.UseVisualStyleBackColor = true;
            // 
            // rdEqual
            // 
            this.rdEqual.AutoSize = true;
            this.rdEqual.Checked = true;
            this.rdEqual.Location = new System.Drawing.Point(12, 12);
            this.rdEqual.Name = "rdEqual";
            this.rdEqual.Size = new System.Drawing.Size(48, 17);
            this.rdEqual.TabIndex = 2;
            this.rdEqual.TabStop = true;
            this.rdEqual.Text = "Igual";
            this.rdEqual.UseVisualStyleBackColor = true;
            // 
            // rdNotEqual
            // 
            this.rdNotEqual.AutoSize = true;
            this.rdNotEqual.Location = new System.Drawing.Point(141, 12);
            this.rdNotEqual.Name = "rdNotEqual";
            this.rdNotEqual.Size = new System.Drawing.Size(68, 17);
            this.rdNotEqual.TabIndex = 3;
            this.rdNotEqual.Text = "Diferente";
            this.rdNotEqual.UseVisualStyleBackColor = true;
            // 
            // rdNotStartsWith
            // 
            this.rdNotStartsWith.AutoSize = true;
            this.rdNotStartsWith.Location = new System.Drawing.Point(141, 35);
            this.rdNotStartsWith.Name = "rdNotStartsWith";
            this.rdNotStartsWith.Size = new System.Drawing.Size(109, 17);
            this.rdNotStartsWith.TabIndex = 4;
            this.rdNotStartsWith.Text = "Não começa com";
            this.rdNotStartsWith.UseVisualStyleBackColor = true;
            // 
            // rdNotEndsWith
            // 
            this.rdNotEndsWith.AutoSize = true;
            this.rdNotEndsWith.Location = new System.Drawing.Point(141, 58);
            this.rdNotEndsWith.Name = "rdNotEndsWith";
            this.rdNotEndsWith.Size = new System.Drawing.Size(105, 17);
            this.rdNotEndsWith.TabIndex = 5;
            this.rdNotEndsWith.Text = "Não termina com";
            this.rdNotEndsWith.UseVisualStyleBackColor = true;
            // 
            // rdContains
            // 
            this.rdContains.AutoSize = true;
            this.rdContains.Location = new System.Drawing.Point(12, 81);
            this.rdContains.Name = "rdContains";
            this.rdContains.Size = new System.Drawing.Size(61, 17);
            this.rdContains.TabIndex = 6;
            this.rdContains.Text = "Contém";
            this.rdContains.UseVisualStyleBackColor = true;
            // 
            // rdNotContains
            // 
            this.rdNotContains.AutoSize = true;
            this.rdNotContains.Location = new System.Drawing.Point(141, 81);
            this.rdNotContains.Name = "rdNotContains";
            this.rdNotContains.Size = new System.Drawing.Size(83, 17);
            this.rdNotContains.TabIndex = 7;
            this.rdNotContains.Text = "Não contém";
            this.rdNotContains.UseVisualStyleBackColor = true;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(13, 121);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(259, 20);
            this.txtText.TabIndex = 8;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(197, 161);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 9;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // FormTextSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 196);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.rdNotContains);
            this.Controls.Add(this.rdContains);
            this.Controls.Add(this.rdNotEndsWith);
            this.Controls.Add(this.rdNotStartsWith);
            this.Controls.Add(this.rdNotEqual);
            this.Controls.Add(this.rdEqual);
            this.Controls.Add(this.rdEndsWith);
            this.Controls.Add(this.rdStartsWith);
            this.Name = "FormTextSelector";
            this.Text = "Seleção de texto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdStartsWith;
        private System.Windows.Forms.RadioButton rdEndsWith;
        private System.Windows.Forms.RadioButton rdEqual;
        private System.Windows.Forms.RadioButton rdNotEqual;
        private System.Windows.Forms.RadioButton rdNotStartsWith;
        private System.Windows.Forms.RadioButton rdNotEndsWith;
        private System.Windows.Forms.RadioButton rdContains;
        private System.Windows.Forms.RadioButton rdNotContains;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btOk;
    }
}