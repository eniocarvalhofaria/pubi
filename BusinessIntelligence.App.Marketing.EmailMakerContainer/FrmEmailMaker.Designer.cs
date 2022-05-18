using BusinessIntelligence.App.Marketing;
namespace BusinessIntelligence.App.Marketing.EmailMakerContainer
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.emailMaker1 = new BusinessIntelligence.App.Marketing.EmailMaker();
            this.SuspendLayout();
            // 
            // emailMaker1
            // 
         
            this.emailMaker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailMaker1.HtmlBodyLayout = "";
            this.emailMaker1.HtmlDealLayout = "";
            this.emailMaker1.HtmlTopLayout = "";
            this.emailMaker1.Location = new System.Drawing.Point(0, 0);
            this.emailMaker1.Name = "emailMaker1";
            this.emailMaker1.Size = new System.Drawing.Size(874, 642);
            this.emailMaker1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 642);
            this.Controls.Add(this.emailMaker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "EmailMaker";
            this.ResumeLayout(false);

        }

        #endregion

        private BusinessIntelligence.App.Marketing.EmailMaker emailMaker1;

      
    }
}

