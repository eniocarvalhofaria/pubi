namespace BusinessIntelligence.Controls
{
  partial   class Selector
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
            this.imSelector = new System.Windows.Forms.PictureBox();
            this.lbValueSelected = new System.Windows.Forms.Label();
            this.ckEnable = new System.Windows.Forms.CheckBox();
            this.grSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // grSelector
            // 
            this.grSelector.BackColor = System.Drawing.Color.White;
            this.grSelector.Controls.Add(this.imSelector);
            this.grSelector.Controls.Add(this.lbValueSelected);
            this.grSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grSelector.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.grSelector.Location = new System.Drawing.Point(0, 0);
            this.grSelector.Name = "grSelector";
            this.grSelector.Size = new System.Drawing.Size(299, 44);
            this.grSelector.TabIndex = 0;
            this.grSelector.TabStop = false;
            // 
            // imSelector
            // 
            this.imSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imSelector.Image = global::BusinessIntelligence.Controls.Properties.Resources.filtergrey32;
            this.imSelector.Location = new System.Drawing.Point(261, 9);
            this.imSelector.Name = "imSelector";
            this.imSelector.Size = new System.Drawing.Size(32, 32);
            this.imSelector.TabIndex = 2;
            this.imSelector.TabStop = false;
            this.imSelector.Click += new System.EventHandler(this.imSelector_Click);
            // 
            // lbValueSelected
            // 
            this.lbValueSelected.AutoSize = true;
            this.lbValueSelected.Location = new System.Drawing.Point(37, 19);
            this.lbValueSelected.Name = "lbValueSelected";
            this.lbValueSelected.Size = new System.Drawing.Size(0, 15);
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
            // Selector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckEnable);
            this.Controls.Add(this.grSelector);
            this.Name = "Selector";
            this.Size = new System.Drawing.Size(299, 44);
            this.grSelector.ResumeLayout(false);
            this.grSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grSelector;
        private System.Windows.Forms.CheckBox ckEnable;
        private System.Windows.Forms.Label lbValueSelected;
        private System.Windows.Forms.PictureBox imSelector;
    }
}
