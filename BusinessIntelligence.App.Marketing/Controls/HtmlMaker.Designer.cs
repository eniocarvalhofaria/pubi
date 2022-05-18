namespace BusinessIntelligence.App.Marketing
{
    partial class HtmlMaker
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbHtml = new System.Windows.Forms.TabPage();
            this.txtHtml = new System.Windows.Forms.RichTextBox();
            this.tbView = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl1.SuspendLayout();
            this.tbHtml.SuspendLayout();
            this.tbView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tbView);
            this.tabControl1.Controls.Add(this.tbHtml);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(491, 320);
            this.tabControl1.TabIndex = 0;
            // 
            // tbHtml
            // 
            this.tbHtml.Controls.Add(this.txtHtml);
            this.tbHtml.Location = new System.Drawing.Point(4, 4);
            this.tbHtml.Name = "tbHtml";
            this.tbHtml.Padding = new System.Windows.Forms.Padding(3);
            this.tbHtml.Size = new System.Drawing.Size(483, 294);
            this.tbHtml.TabIndex = 0;
            this.tbHtml.Text = "Html";
            this.tbHtml.UseVisualStyleBackColor = true;
            // 
            // txtHtml
            // 
            this.txtHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHtml.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHtml.Location = new System.Drawing.Point(3, 3);
            this.txtHtml.Name = "txtHtml";
            this.txtHtml.Size = new System.Drawing.Size(477, 288);
            this.txtHtml.TabIndex = 0;
            this.txtHtml.Text = "";
            this.txtHtml.TextChanged += new System.EventHandler(this.OnHtmlTextChanged);
            // 
            // tbView
            // 
            this.tbView.Controls.Add(this.webBrowser1);
            this.tbView.Location = new System.Drawing.Point(4, 4);
            this.tbView.Name = "tbView";
            this.tbView.Padding = new System.Windows.Forms.Padding(3);
            this.tbView.Size = new System.Drawing.Size(483, 294);
            this.tbView.TabIndex = 1;
            this.tbView.Text = "Visualização";
            this.tbView.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(477, 288);
            this.webBrowser1.TabIndex = 0;
            // 
            // HtmlMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "HtmlMaker";
            this.Size = new System.Drawing.Size(491, 320);
            this.tabControl1.ResumeLayout(false);
            this.tbHtml.ResumeLayout(false);
            this.tbView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbHtml;
        private System.Windows.Forms.RichTextBox txtHtml;
        private System.Windows.Forms.TabPage tbView;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
