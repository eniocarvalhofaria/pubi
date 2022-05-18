namespace BusinessIntelligence
{
    partial class ListSelector
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
            this.cbSelector = new System.Windows.Forms.ComboBox();
            this.ckEnable = new System.Windows.Forms.CheckBox();
            this.grSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // grSelector
            // 
            this.grSelector.Controls.Add(this.cbSelector);
            this.grSelector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grSelector.Enabled = false;
            this.grSelector.Location = new System.Drawing.Point(0, 4);
            this.grSelector.Name = "grSelector";
            this.grSelector.Size = new System.Drawing.Size(299, 74);
            this.grSelector.TabIndex = 0;
            this.grSelector.TabStop = false;
            // 
            // cbSelector
            // 
            this.cbSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSelector.FormattingEnabled = true;
            this.cbSelector.Location = new System.Drawing.Point(30, 28);
            this.cbSelector.Name = "cbSelector";
            this.cbSelector.Size = new System.Drawing.Size(227, 21);
            this.cbSelector.TabIndex = 0;
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
            // ListSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ckEnable);
            this.Controls.Add(this.grSelector);
            this.Name = "ListSelector";
            this.Size = new System.Drawing.Size(299, 78);
            this.grSelector.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grSelector;
        private System.Windows.Forms.CheckBox ckEnable;
        public System.Windows.Forms.ComboBox cbSelector;
    }
}
