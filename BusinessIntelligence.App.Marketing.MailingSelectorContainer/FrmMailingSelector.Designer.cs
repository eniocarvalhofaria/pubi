namespace BusinessIntelligence.App.Marketing.MailingSelectorContainer
{
    partial class FrmMailingSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMailingSelector));
            this.mailingSelector1 = new BusinessIntelligence.App.Marketing.MailingSelector();
            this.SuspendLayout();
            // 
            // mailingSelector1
            // 
            this.mailingSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mailingSelector1.Location = new System.Drawing.Point(0, 0);
            this.mailingSelector1.Name = "mailingSelector1";
            this.mailingSelector1.QtySlices = 0;
            this.mailingSelector1.Size = new System.Drawing.Size(1262, 644);
            this.mailingSelector1.TabIndex = 0;
            // 
            // FrmMailingSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 644);
            this.Controls.Add(this.mailingSelector1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMailingSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mailing Selector";
            this.ResumeLayout(false);

        }

        #endregion

        public MailingSelector mailingSelector1;
    }
}

