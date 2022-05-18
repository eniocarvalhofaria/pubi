namespace BusinessIntelligence.Controls
{
    partial class FormValueSelector
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
            this.rdNotEqual = new System.Windows.Forms.RadioButton();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.rdGreater = new System.Windows.Forms.RadioButton();
            this.rdEqual = new System.Windows.Forms.RadioButton();
            this.rdLess = new System.Windows.Forms.RadioButton();
            this.rdLessOrEqual = new System.Windows.Forms.RadioButton();
            this.rdGreaterOrEqual = new System.Windows.Forms.RadioButton();
            this.rdBetween = new System.Windows.Forms.RadioButton();
            this.txtEndValue = new System.Windows.Forms.TextBox();
            this.rdNotBetween = new System.Windows.Forms.RadioButton();
            this.lbAnd = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdNotEqual
            // 
            this.rdNotEqual.AutoSize = true;
            this.rdNotEqual.Location = new System.Drawing.Point(103, 12);
            this.rdNotEqual.Name = "rdNotEqual";
            this.rdNotEqual.Size = new System.Drawing.Size(68, 17);
            this.rdNotEqual.TabIndex = 9;
            this.rdNotEqual.Text = "Diferente";
            this.rdNotEqual.UseVisualStyleBackColor = true;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(12, 130);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(82, 20);
            this.txtValue.TabIndex = 8;
            this.txtValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValue_KeyPress);
            // 
            // rdGreater
            // 
            this.rdGreater.AutoSize = true;
            this.rdGreater.Location = new System.Drawing.Point(12, 58);
            this.rdGreater.Name = "rdGreater";
            this.rdGreater.Size = new System.Drawing.Size(51, 17);
            this.rdGreater.TabIndex = 7;
            this.rdGreater.Text = "Maior";
            this.rdGreater.UseVisualStyleBackColor = true;
            // 
            // rdEqual
            // 
            this.rdEqual.AutoSize = true;
            this.rdEqual.Checked = true;
            this.rdEqual.Location = new System.Drawing.Point(12, 12);
            this.rdEqual.Name = "rdEqual";
            this.rdEqual.Size = new System.Drawing.Size(48, 17);
            this.rdEqual.TabIndex = 6;
            this.rdEqual.TabStop = true;
            this.rdEqual.Text = "Igual";
            this.rdEqual.UseVisualStyleBackColor = true;
            // 
            // rdLess
            // 
            this.rdLess.AutoSize = true;
            this.rdLess.Location = new System.Drawing.Point(12, 35);
            this.rdLess.Name = "rdLess";
            this.rdLess.Size = new System.Drawing.Size(55, 17);
            this.rdLess.TabIndex = 5;
            this.rdLess.Text = "Menor";
            this.rdLess.UseVisualStyleBackColor = true;
            // 
            // rdLessOrEqual
            // 
            this.rdLessOrEqual.AutoSize = true;
            this.rdLessOrEqual.Location = new System.Drawing.Point(103, 35);
            this.rdLessOrEqual.Name = "rdLessOrEqual";
            this.rdLessOrEqual.Size = new System.Drawing.Size(95, 17);
            this.rdLessOrEqual.TabIndex = 10;
            this.rdLessOrEqual.Text = "Menor ou igual";
            this.rdLessOrEqual.UseVisualStyleBackColor = true;
            // 
            // rdGreaterOrEqual
            // 
            this.rdGreaterOrEqual.AutoSize = true;
            this.rdGreaterOrEqual.Location = new System.Drawing.Point(103, 58);
            this.rdGreaterOrEqual.Name = "rdGreaterOrEqual";
            this.rdGreaterOrEqual.Size = new System.Drawing.Size(91, 17);
            this.rdGreaterOrEqual.TabIndex = 11;
            this.rdGreaterOrEqual.Text = "Maior ou igual";
            this.rdGreaterOrEqual.UseVisualStyleBackColor = true;
            // 
            // rdBetween
            // 
            this.rdBetween.AutoSize = true;
            this.rdBetween.Location = new System.Drawing.Point(12, 81);
            this.rdBetween.Name = "rdBetween";
            this.rdBetween.Size = new System.Drawing.Size(73, 17);
            this.rdBetween.TabIndex = 12;
            this.rdBetween.Text = "Está entre";
            this.rdBetween.UseVisualStyleBackColor = true;
            this.rdBetween.CheckedChanged += new System.EventHandler(this.rdBetween_CheckedChanged);
            // 
            // txtEndValue
            // 
            this.txtEndValue.Location = new System.Drawing.Point(112, 130);
            this.txtEndValue.Name = "txtEndValue";
            this.txtEndValue.Size = new System.Drawing.Size(82, 20);
            this.txtEndValue.TabIndex = 14;
            this.txtEndValue.Visible = false;
            this.txtEndValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValue_KeyPress);
            // 
            // rdNotBetween
            // 
            this.rdNotBetween.AutoSize = true;
            this.rdNotBetween.Location = new System.Drawing.Point(103, 81);
            this.rdNotBetween.Name = "rdNotBetween";
            this.rdNotBetween.Size = new System.Drawing.Size(95, 17);
            this.rdNotBetween.TabIndex = 15;
            this.rdNotBetween.Text = "Não está entre";
            this.rdNotBetween.UseVisualStyleBackColor = true;
            this.rdNotBetween.CheckedChanged += new System.EventHandler(this.rdBetween_CheckedChanged);
            // 
            // lbAnd
            // 
            this.lbAnd.AutoSize = true;
            this.lbAnd.Location = new System.Drawing.Point(100, 130);
            this.lbAnd.Name = "lbAnd";
            this.lbAnd.Size = new System.Drawing.Size(13, 13);
            this.lbAnd.TabIndex = 16;
            this.lbAnd.Text = "e";
            this.lbAnd.Visible = false;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(38, 168);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 17;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(123, 168);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 18;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // FormValueSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(215, 203);
            this.ControlBox = false;
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.lbAnd);
            this.Controls.Add(this.rdNotBetween);
            this.Controls.Add(this.txtEndValue);
            this.Controls.Add(this.rdBetween);
            this.Controls.Add(this.rdGreaterOrEqual);
            this.Controls.Add(this.rdLessOrEqual);
            this.Controls.Add(this.rdNotEqual);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.rdGreater);
            this.Controls.Add(this.rdEqual);
            this.Controls.Add(this.rdLess);
            this.Name = "FormValueSelector";
            this.Text = "Seleção de Valor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdNotEqual;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.RadioButton rdGreater;
        private System.Windows.Forms.RadioButton rdEqual;
        private System.Windows.Forms.RadioButton rdLess;
        private System.Windows.Forms.RadioButton rdLessOrEqual;
        private System.Windows.Forms.RadioButton rdGreaterOrEqual;
        private System.Windows.Forms.RadioButton rdBetween;
        private System.Windows.Forms.TextBox txtEndValue;
        private System.Windows.Forms.RadioButton rdNotBetween;
        private System.Windows.Forms.Label lbAnd;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
    }
}