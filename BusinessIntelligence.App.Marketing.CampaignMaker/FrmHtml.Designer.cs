namespace BusinessIntelligence.App.Marketing.CampaignMaker
{
    partial class FrmHtml
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
            this.htmlMaker1 = new BusinessIntelligence.App.Marketing.HtmlMaker();
            this.SuspendLayout();
            // 
            // htmlMaker1
            // 
            this.htmlMaker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlMaker1.Html = "";
            this.htmlMaker1.Location = new System.Drawing.Point(0, 0);
            this.htmlMaker1.Name = "htmlMaker1";
            this.htmlMaker1.ReadOnly = false;
            this.htmlMaker1.Size = new System.Drawing.Size(713, 391);
            this.htmlMaker1.TabIndex = 0;
            this.htmlMaker1.Template = null;
            // 
            // FrmHtml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 391);
            this.Controls.Add(this.htmlMaker1);
            this.Name = "FrmHtml";
            this.Text = "FrmHtml";
            this.ResumeLayout(false);

        }

        #endregion

        private HtmlMaker htmlMaker1;
    }
}