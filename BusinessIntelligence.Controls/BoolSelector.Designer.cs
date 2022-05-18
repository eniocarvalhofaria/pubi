namespace BusinessIntelligence.Controls
{
    partial class BoolSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grSelector = new System.Windows.Forms.GroupBox();
            this.rdNo = new System.Windows.Forms.RadioButton();
            this.rdYes = new System.Windows.Forms.RadioButton();
            this.lbValueSelected = new System.Windows.Forms.Label();
            this.ckEnable = new System.Windows.Forms.CheckBox();
            this.grSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // grSelector
            // 
            this.grSelector.BackColor = System.Drawing.Color.White;
            this.grSelector.Controls.Add(this.rdNo);
            this.grSelector.Controls.Add(this.rdYes);
            this.grSelector.Controls.Add(this.lbValueSelected);
            this.grSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grSelector.Location = new System.Drawing.Point(0, 0);
            this.grSelector.Name = "grSelector";
            this.grSelector.Size = new System.Drawing.Size(299, 50);
            this.grSelector.TabIndex = 0;
            this.grSelector.TabStop = false;
            // 
            // rdNo
            // 
            this.rdNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdNo.AutoSize = true;
            this.rdNo.Enabled = false;
            this.rdNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNo.Location = new System.Drawing.Point(222, 24);
            this.rdNo.Name = "rdNo";
            this.rdNo.Size = new System.Drawing.Size(47, 19);
            this.rdNo.TabIndex = 2;
            this.rdNo.Text = "Não";
            this.rdNo.UseVisualStyleBackColor = true;
            // 
            // rdYes
            // 
            this.rdYes.AutoSize = true;
            this.rdYes.Checked = true;
            this.rdYes.Enabled = false;
            this.rdYes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdYes.Location = new System.Drawing.Point(46, 24);
            this.rdYes.Name = "rdYes";
            this.rdYes.Size = new System.Drawing.Size(45, 19);
            this.rdYes.TabIndex = 1;
            this.rdYes.TabStop = true;
            this.rdYes.Text = "Sim";
            this.rdYes.UseVisualStyleBackColor = true;
            // 
            // lbValueSelected
            // 
            this.lbValueSelected.AutoSize = true;
            this.lbValueSelected.Location = new System.Drawing.Point(170, 13);
            this.lbValueSelected.Name = "lbValueSelected";
            this.lbValueSelected.Size = new System.Drawing.Size(0, 13);
            this.lbValueSelected.TabIndex = 0;
            // 
            // ckEnable
            // 
            this.ckEnable.AutoSize = true;
            this.ckEnable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckEnable.Location = new System.Drawing.Point(6, 3);
            this.ckEnable.Name = "ckEnable";
            this.ckEnable.Size = new System.Drawing.Size(15, 14);
            this.ckEnable.TabIndex = 5;
            this.ckEnable.UseVisualStyleBackColor = true;
            this.ckEnable.CheckedChanged += new System.EventHandler(this.ckEnable_CheckedChanged);
            // 
            // BoolSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckEnable);
            this.Controls.Add(this.grSelector);
            this.Name = "BoolSelector";
            this.Size = new System.Drawing.Size(299, 50);
            this.grSelector.ResumeLayout(false);
            this.grSelector.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grSelector;
        private System.Windows.Forms.CheckBox ckEnable;
        private System.Windows.Forms.Label lbValueSelected;
        private System.Windows.Forms.RadioButton rdNo;
        private System.Windows.Forms.RadioButton rdYes;
    }
}
