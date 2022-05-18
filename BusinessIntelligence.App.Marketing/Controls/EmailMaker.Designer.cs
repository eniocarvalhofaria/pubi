namespace BusinessIntelligence.App.Marketing
{
    partial class EmailMaker
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
            this.tbConfiguration = new System.Windows.Forms.TabPage();
            this.btSaveHtml = new System.Windows.Forms.Button();
            this.grTemplates = new System.Windows.Forms.GroupBox();
            this.cbMiddleDeals = new System.Windows.Forms.ComboBox();
            this.cbTopDeals = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEmailBodyTemplate = new System.Windows.Forms.ComboBox();
            this.lbTop = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCampaign = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEmailBodyLayout = new System.Windows.Forms.TabPage();
            this.tbTop = new System.Windows.Forms.TabPage();
            this.tbDeal = new System.Windows.Forms.TabPage();
            this.tbEmailBody = new System.Windows.Forms.TabPage();
            this.saveHtmlContent = new System.Windows.Forms.SaveFileDialog();
            this.dealsSelector1 = new BusinessIntelligence.App.Marketing.DealsSelector();
            this.hmLayoutEmailBody = new BusinessIntelligence.App.Marketing.HtmlMaker();
            this.hmLayoutDealTop = new BusinessIntelligence.App.Marketing.HtmlMaker();
            this.hmLayoutDeals = new BusinessIntelligence.App.Marketing.HtmlMaker();
            this.hmEmailBody = new BusinessIntelligence.App.Marketing.HtmlMaker();
            this.tabControl1.SuspendLayout();
            this.tbConfiguration.SuspendLayout();
            this.grTemplates.SuspendLayout();
            this.tbEmailBodyLayout.SuspendLayout();
            this.tbTop.SuspendLayout();
            this.tbDeal.SuspendLayout();
            this.tbEmailBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbConfiguration);
            this.tabControl1.Controls.Add(this.tbEmailBodyLayout);
            this.tabControl1.Controls.Add(this.tbTop);
            this.tabControl1.Controls.Add(this.tbDeal);
            this.tabControl1.Controls.Add(this.tbEmailBody);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(717, 385);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tbConfiguration
            // 
            this.tbConfiguration.Controls.Add(this.btSaveHtml);
            this.tbConfiguration.Controls.Add(this.grTemplates);
            this.tbConfiguration.Controls.Add(this.txtCampaign);
            this.tbConfiguration.Controls.Add(this.label1);
            this.tbConfiguration.Controls.Add(this.dealsSelector1);
            this.tbConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tbConfiguration.Name = "tbConfiguration";
            this.tbConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tbConfiguration.Size = new System.Drawing.Size(709, 359);
            this.tbConfiguration.TabIndex = 1;
            this.tbConfiguration.Text = "Configuração";
            this.tbConfiguration.UseVisualStyleBackColor = true;
            // 
            // btSaveHtml
            // 
            this.btSaveHtml.Location = new System.Drawing.Point(576, 330);
            this.btSaveHtml.Name = "btSaveHtml";
            this.btSaveHtml.Size = new System.Drawing.Size(124, 23);
            this.btSaveHtml.TabIndex = 6;
            this.btSaveHtml.Text = "Salvar Html";
            this.btSaveHtml.UseVisualStyleBackColor = true;
            this.btSaveHtml.Click += new System.EventHandler(this.btSaveHtml_Click);
            // 
            // grTemplates
            // 
            this.grTemplates.Controls.Add(this.cbMiddleDeals);
            this.grTemplates.Controls.Add(this.cbTopDeals);
            this.grTemplates.Controls.Add(this.label2);
            this.grTemplates.Controls.Add(this.cbEmailBodyTemplate);
            this.grTemplates.Controls.Add(this.lbTop);
            this.grTemplates.Controls.Add(this.label3);
            this.grTemplates.Location = new System.Drawing.Point(410, 48);
            this.grTemplates.Name = "grTemplates";
            this.grTemplates.Size = new System.Drawing.Size(290, 150);
            this.grTemplates.TabIndex = 5;
            this.grTemplates.TabStop = false;
            this.grTemplates.Text = "Templates";
            // 
            // cbMiddleDeals
            // 
            this.cbMiddleDeals.FormattingEnabled = true;
            this.cbMiddleDeals.Location = new System.Drawing.Point(93, 101);
            this.cbMiddleDeals.Name = "cbMiddleDeals";
            this.cbMiddleDeals.Size = new System.Drawing.Size(191, 21);
            this.cbMiddleDeals.TabIndex = 9;
            this.cbMiddleDeals.SelectedIndexChanged += new System.EventHandler(this.cbMiddleDeals_SelectedIndexChanged);
            // 
            // cbTopDeals
            // 
            this.cbTopDeals.FormattingEnabled = true;
            this.cbTopDeals.Location = new System.Drawing.Point(93, 60);
            this.cbTopDeals.Name = "cbTopDeals";
            this.cbTopDeals.Size = new System.Drawing.Size(191, 21);
            this.cbTopDeals.TabIndex = 8;
            this.cbTopDeals.SelectedIndexChanged += new System.EventHandler(this.cbTopDeals_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Corpo do email";
            // 
            // cbEmailBodyTemplate
            // 
            this.cbEmailBodyTemplate.FormattingEnabled = true;
            this.cbEmailBodyTemplate.Location = new System.Drawing.Point(93, 26);
            this.cbEmailBodyTemplate.Name = "cbEmailBodyTemplate";
            this.cbEmailBodyTemplate.Size = new System.Drawing.Size(191, 21);
            this.cbEmailBodyTemplate.TabIndex = 6;
            this.cbEmailBodyTemplate.SelectedIndexChanged += new System.EventHandler(this.cbEmailBodyTemplate_SelectedIndexChanged);
            // 
            // lbTop
            // 
            this.lbTop.AutoSize = true;
            this.lbTop.Location = new System.Drawing.Point(6, 63);
            this.lbTop.Name = "lbTop";
            this.lbTop.Size = new System.Drawing.Size(32, 13);
            this.lbTop.TabIndex = 5;
            this.lbTop.Text = "Topo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Meio";
            // 
            // txtCampaign
            // 
            this.txtCampaign.Location = new System.Drawing.Point(124, 22);
            this.txtCampaign.Name = "txtCampaign";
            this.txtCampaign.Size = new System.Drawing.Size(171, 20);
            this.txtCampaign.TabIndex = 3;
            this.txtCampaign.Text = "CRMBRYYMMDDNNNN";
            this.txtCampaign.Leave += new System.EventHandler(this.OntxtCampaignLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome da campanha";
            // 
            // tbEmailBodyLayout
            // 
            this.tbEmailBodyLayout.Controls.Add(this.hmLayoutEmailBody);
            this.tbEmailBodyLayout.Location = new System.Drawing.Point(4, 22);
            this.tbEmailBodyLayout.Name = "tbEmailBodyLayout";
            this.tbEmailBodyLayout.Padding = new System.Windows.Forms.Padding(3);
            this.tbEmailBodyLayout.Size = new System.Drawing.Size(709, 359);
            this.tbEmailBodyLayout.TabIndex = 0;
            this.tbEmailBodyLayout.Text = "Layout do corpo do email";
            this.tbEmailBodyLayout.UseVisualStyleBackColor = true;
            // 
            // tbTop
            // 
            this.tbTop.Controls.Add(this.hmLayoutDealTop);
            this.tbTop.Location = new System.Drawing.Point(4, 22);
            this.tbTop.Name = "tbTop";
            this.tbTop.Size = new System.Drawing.Size(709, 359);
            this.tbTop.TabIndex = 2;
            this.tbTop.Text = "Layout do destaque";
            this.tbTop.UseVisualStyleBackColor = true;
            // 
            // tbDeal
            // 
            this.tbDeal.Controls.Add(this.hmLayoutDeals);
            this.tbDeal.Location = new System.Drawing.Point(4, 22);
            this.tbDeal.Name = "tbDeal";
            this.tbDeal.Size = new System.Drawing.Size(709, 359);
            this.tbDeal.TabIndex = 3;
            this.tbDeal.Text = "Layout das ofertas";
            this.tbDeal.UseVisualStyleBackColor = true;
            // 
            // tbEmailBody
            // 
            this.tbEmailBody.Controls.Add(this.hmEmailBody);
            this.tbEmailBody.Location = new System.Drawing.Point(4, 22);
            this.tbEmailBody.Name = "tbEmailBody";
            this.tbEmailBody.Size = new System.Drawing.Size(709, 359);
            this.tbEmailBody.TabIndex = 4;
            this.tbEmailBody.Text = "Corpo do email";
            this.tbEmailBody.UseVisualStyleBackColor = true;
            // 
            // saveHtmlContent
            // 
            this.saveHtmlContent.FileOk += new System.ComponentModel.CancelEventHandler(this.saveHtmlContent_FileOk);
            // 
            // dealsSelector1
            // 
            this.dealsSelector1.Date = new System.DateTime(((long)(0)));
            this.dealsSelector1.Location = new System.Drawing.Point(3, 48);
            this.dealsSelector1.Name = "dealsSelector1";
            this.dealsSelector1.Size = new System.Drawing.Size(401, 150);
            this.dealsSelector1.TabIndex = 0;
            this.dealsSelector1.Load += new System.EventHandler(this.dealsSelector1_Load);
            // 
            // hmLayoutEmailBody
            // 
            this.hmLayoutEmailBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmLayoutEmailBody.Html = "";
            this.hmLayoutEmailBody.Location = new System.Drawing.Point(3, 3);
            this.hmLayoutEmailBody.Name = "hmLayoutEmailBody";
            this.hmLayoutEmailBody.ReadOnly = false;
            this.hmLayoutEmailBody.Size = new System.Drawing.Size(703, 353);
            this.hmLayoutEmailBody.TabIndex = 0;
            this.hmLayoutEmailBody.Template = null;
            // 
            // hmLayoutDealTop
            // 
            this.hmLayoutDealTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmLayoutDealTop.Html = "";
            this.hmLayoutDealTop.Location = new System.Drawing.Point(0, 0);
            this.hmLayoutDealTop.Name = "hmLayoutDealTop";
            this.hmLayoutDealTop.ReadOnly = false;
            this.hmLayoutDealTop.Size = new System.Drawing.Size(709, 359);
            this.hmLayoutDealTop.TabIndex = 0;
            this.hmLayoutDealTop.Template = null;
            // 
            // hmLayoutDeals
            // 
            this.hmLayoutDeals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmLayoutDeals.Html = "";
            this.hmLayoutDeals.Location = new System.Drawing.Point(0, 0);
            this.hmLayoutDeals.Name = "hmLayoutDeals";
            this.hmLayoutDeals.ReadOnly = false;
            this.hmLayoutDeals.Size = new System.Drawing.Size(709, 359);
            this.hmLayoutDeals.TabIndex = 0;
            this.hmLayoutDeals.Template = null;
            // 
            // hmEmailBody
            // 
            this.hmEmailBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmEmailBody.Html = "";
            this.hmEmailBody.Location = new System.Drawing.Point(0, 0);
            this.hmEmailBody.Name = "hmEmailBody";
            this.hmEmailBody.ReadOnly = true;
            this.hmEmailBody.Size = new System.Drawing.Size(709, 359);
            this.hmEmailBody.TabIndex = 0;
            this.hmEmailBody.Template = null;
            // 
            // EmailMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "EmailMaker";
            this.Size = new System.Drawing.Size(717, 385);
            this.Load += new System.EventHandler(this.EmailMaker_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbConfiguration.ResumeLayout(false);
            this.tbConfiguration.PerformLayout();
            this.grTemplates.ResumeLayout(false);
            this.grTemplates.PerformLayout();
            this.tbEmailBodyLayout.ResumeLayout(false);
            this.tbTop.ResumeLayout(false);
            this.tbDeal.ResumeLayout(false);
            this.tbEmailBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbConfiguration;
        private System.Windows.Forms.TabPage tbEmailBodyLayout;
        private HtmlMaker hmLayoutEmailBody;
        private System.Windows.Forms.TabPage tbTop;
        private HtmlMaker hmLayoutDealTop;
        private System.Windows.Forms.TabPage tbDeal;
        private HtmlMaker hmLayoutDeals;
        private System.Windows.Forms.TabPage tbEmailBody;
        private HtmlMaker hmEmailBody;
        private DealsSelector dealsSelector1;
        private System.Windows.Forms.TextBox txtCampaign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grTemplates;
        private System.Windows.Forms.Label lbTop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEmailBodyTemplate;
        private System.Windows.Forms.Button btSaveHtml;
        private System.Windows.Forms.SaveFileDialog saveHtmlContent;
        private System.Windows.Forms.ComboBox cbTopDeals;
        private System.Windows.Forms.ComboBox cbMiddleDeals;
    }
}
