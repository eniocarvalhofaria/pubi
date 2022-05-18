using BusinessIntelligence.Persistence.Controls;
using BusinessIntelligence.Controls;
namespace BusinessIntelligence.App.Marketing
{
    partial class MailingSelector
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
            this.grMailingSelector = new System.Windows.Forms.GroupBox();
            this.pnSite = new System.Windows.Forms.Panel();
            this.rdGroupon = new System.Windows.Forms.RadioButton();
            this.rdPU = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.CampaignExclusion = new BusinessIntelligence.Persistence.Controls.StoredObjectSelector();
            this.customizedLists = new BusinessIntelligence.Persistence.Controls.StoredObjectSelector();
            this.MailingList = new BusinessIntelligence.Persistence.Controls.StoredObjectSelector();
            this.AgeRange = new BusinessIntelligence.Persistence.Controls.StoredObjectSelector();
            this.btCategories = new System.Windows.Forms.Button();
            this.facebookConnect = new BusinessIntelligence.Controls.BoolSelector();
            this.Status = new BusinessIntelligence.Controls.Selector();
            this.btDeleteMailing = new System.Windows.Forms.Button();
            this.btModify = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btExportMailing = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.percControl = new System.Windows.Forms.NumericUpDown();
            this.btClear = new System.Windows.Forms.Button();
            this.birthDate = new BusinessIntelligence.Controls.Selector();
            this.btGenerateSql = new System.Windows.Forms.Button();
            this.Engagement = new BusinessIntelligence.Controls.Selector();
            this.btGenerateMailing = new System.Windows.Forms.Button();
            this.Gender = new BusinessIntelligence.ListSelector();
            this.grPurchaseProfile = new System.Windows.Forms.GroupBox();
            this.rdNeverBuy = new System.Windows.Forms.RadioButton();
            this.rdBuyer = new System.Windows.Forms.RadioButton();
            this.rdAny = new System.Windows.Forms.RadioButton();
            this.pnPurchaseAny = new System.Windows.Forms.Panel();
            this.LastPurchaseUsePromocode = new BusinessIntelligence.Controls.BoolSelector();
            this.HasAppPurchase = new BusinessIntelligence.Controls.BoolSelector();
            this.BuyOffers = new BusinessIntelligence.Controls.BigListSelector();
            this.pnPurchaseProfile = new System.Windows.Forms.Panel();
            this.PromocodeName = new BusinessIntelligence.Controls.Selector();
            this.maxPurchaseValue = new BusinessIntelligence.Controls.Selector();
            this.Promocodes = new BusinessIntelligence.Controls.Selector();
            this.NetRevenue = new BusinessIntelligence.Controls.Selector();
            this.QTYPurchases = new BusinessIntelligence.Controls.Selector();
            this.dateLastPurchase = new BusinessIntelligence.Controls.Selector();
            this.ckPurchaseProfile = new System.Windows.Forms.CheckBox();
            this.dtsUserRegistered = new BusinessIntelligence.Controls.Selector();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbParameter = new System.Windows.Forms.TabPage();
            this.tbSql = new System.Windows.Forms.TabPage();
            this.txtSql = new System.Windows.Forms.RichTextBox();
            this.tbSeeded = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSemeados = new System.Windows.Forms.TextBox();
            this.txtSeededs = new BusinessIntelligence.TextContent();
            this.exportMailing = new System.Windows.Forms.SaveFileDialog();
            this.dateSelector2 = new BusinessIntelligence.Controls.Selector();
            this.valueSelector1 = new BusinessIntelligence.Controls.Selector();
            this.grMailingSelector.SuspendLayout();
            this.pnSite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percControl)).BeginInit();
            this.grPurchaseProfile.SuspendLayout();
            this.pnPurchaseAny.SuspendLayout();
            this.pnPurchaseProfile.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbParameter.SuspendLayout();
            this.tbSql.SuspendLayout();
            this.tbSeeded.SuspendLayout();
            this.SuspendLayout();
            // 
            // grMailingSelector
            // 
            this.grMailingSelector.BackColor = System.Drawing.Color.White;
            this.grMailingSelector.Controls.Add(this.pnSite);
            this.grMailingSelector.Controls.Add(this.label1);
            this.grMailingSelector.Controls.Add(this.CampaignExclusion);
            this.grMailingSelector.Controls.Add(this.customizedLists);
            this.grMailingSelector.Controls.Add(this.MailingList);
            this.grMailingSelector.Controls.Add(this.AgeRange);
            this.grMailingSelector.Controls.Add(this.btCategories);
            this.grMailingSelector.Controls.Add(this.facebookConnect);
            this.grMailingSelector.Controls.Add(this.Status);
            this.grMailingSelector.Controls.Add(this.btDeleteMailing);
            this.grMailingSelector.Controls.Add(this.btModify);
            this.grMailingSelector.Controls.Add(this.btClose);
            this.grMailingSelector.Controls.Add(this.btExportMailing);
            this.grMailingSelector.Controls.Add(this.label3);
            this.grMailingSelector.Controls.Add(this.percControl);
            this.grMailingSelector.Controls.Add(this.btClear);
            this.grMailingSelector.Controls.Add(this.birthDate);
            this.grMailingSelector.Controls.Add(this.btGenerateSql);
            this.grMailingSelector.Controls.Add(this.Engagement);
            this.grMailingSelector.Controls.Add(this.btGenerateMailing);
            this.grMailingSelector.Controls.Add(this.Gender);
            this.grMailingSelector.Controls.Add(this.grPurchaseProfile);
            this.grMailingSelector.Controls.Add(this.dtsUserRegistered);
            this.grMailingSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grMailingSelector.Location = new System.Drawing.Point(3, 3);
            this.grMailingSelector.Name = "grMailingSelector";
            this.grMailingSelector.Size = new System.Drawing.Size(1243, 580);
            this.grMailingSelector.TabIndex = 2;
            this.grMailingSelector.TabStop = false;
            // 
            // pnSite
            // 
            this.pnSite.Controls.Add(this.rdGroupon);
            this.pnSite.Controls.Add(this.rdPU);
            this.pnSite.Location = new System.Drawing.Point(8, 56);
            this.pnSite.Name = "pnSite";
            this.pnSite.Size = new System.Drawing.Size(129, 100);
            this.pnSite.TabIndex = 36;
            // 
            // rdGroupon
            // 
            this.rdGroupon.AutoSize = true;
            this.rdGroupon.Location = new System.Drawing.Point(15, 53);
            this.rdGroupon.Name = "rdGroupon";
            this.rdGroupon.Size = new System.Drawing.Size(66, 17);
            this.rdGroupon.TabIndex = 1;
            this.rdGroupon.Text = "Groupon";
            this.rdGroupon.UseVisualStyleBackColor = true;
            // 
            // rdPU
            // 
            this.rdPU.AutoSize = true;
            this.rdPU.Location = new System.Drawing.Point(16, 21);
            this.rdPU.Name = "rdPU";
            this.rdPU.Size = new System.Drawing.Size(89, 17);
            this.rdPU.TabIndex = 0;
            this.rdPU.Text = "Peixe Urbano";
            this.rdPU.UseVisualStyleBackColor = true;
            this.rdPU.CheckedChanged += new System.EventHandler(this.rdPU_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 35;
            this.label1.Text = "Seleção de mailing";
            // 
            // CampaignExclusion
            // 
            this.CampaignExclusion.BackColor = System.Drawing.Color.White;
            this.CampaignExclusion.Checked = false;
            this.CampaignExclusion.FilterExpression = null;
            this.CampaignExclusion.LoadAction = null;
            this.CampaignExclusion.Location = new System.Drawing.Point(951, 19);
            this.CampaignExclusion.MultipleSelection = true;
            this.CampaignExclusion.Name = "CampaignExclusion";
            this.CampaignExclusion.SelectedId = 0;
            this.CampaignExclusion.Size = new System.Drawing.Size(269, 95);
            this.CampaignExclusion.SortExpression = null;
            this.CampaignExclusion.TabIndex = 16;
            this.CampaignExclusion.Text = "Usuários em campanhas";
            // 
            // customizedLists
            // 
            this.customizedLists.BackColor = System.Drawing.Color.White;
            this.customizedLists.Checked = false;
            this.customizedLists.FilterExpression = null;
            this.customizedLists.LoadAction = null;
            this.customizedLists.Location = new System.Drawing.Point(664, 19);
            this.customizedLists.MultipleSelection = true;
            this.customizedLists.Name = "customizedLists";
            this.customizedLists.SelectedId = 0;
            this.customizedLists.Size = new System.Drawing.Size(269, 95);
            this.customizedLists.SortExpression = null;
            this.customizedLists.TabIndex = 17;
            this.customizedLists.Text = "Listas customizadas";
            // 
            // MailingList
            // 
            this.MailingList.BackColor = System.Drawing.Color.White;
            this.MailingList.Checked = false;
            this.MailingList.FilterExpression = null;
            this.MailingList.LoadAction = null;
            this.MailingList.Location = new System.Drawing.Point(664, 120);
            this.MailingList.MultipleSelection = true;
            this.MailingList.Name = "MailingList";
            this.MailingList.SelectedId = 0;
            this.MailingList.Size = new System.Drawing.Size(269, 95);
            this.MailingList.SortExpression = null;
            this.MailingList.TabIndex = 17;
            this.MailingList.Text = "Listas de usuários";
            // 
            // AgeRange
            // 
            this.AgeRange.BackColor = System.Drawing.Color.White;
            this.AgeRange.Checked = false;
            this.AgeRange.FilterExpression = null;
            this.AgeRange.LoadAction = null;
            this.AgeRange.Location = new System.Drawing.Point(951, 120);
            this.AgeRange.MultipleSelection = true;
            this.AgeRange.Name = "AgeRange";
            this.AgeRange.SelectedId = 0;
            this.AgeRange.Size = new System.Drawing.Size(269, 95);
            this.AgeRange.SortExpression = null;
            this.AgeRange.TabIndex = 17;
            this.AgeRange.Text = "Faixa etária";
            // 
            // btCategories
            // 
            this.btCategories.BackColor = System.Drawing.Color.Teal;
            this.btCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCategories.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCategories.ForeColor = System.Drawing.Color.White;
            this.btCategories.Location = new System.Drawing.Point(1002, 221);
            this.btCategories.Name = "btCategories";
            this.btCategories.Size = new System.Drawing.Size(218, 39);
            this.btCategories.TabIndex = 34;
            this.btCategories.Text = "Segmentação por categoria";
            this.btCategories.UseVisualStyleBackColor = false;
            this.btCategories.Click += new System.EventHandler(this.btCategories_Click);
            // 
            // facebookConnect
            // 
            this.facebookConnect.Checked = false;
            this.facebookConnect.Criterial = null;
            this.facebookConnect.Location = new System.Drawing.Point(143, 106);
            this.facebookConnect.Name = "facebookConnect";
            this.facebookConnect.Size = new System.Drawing.Size(221, 50);
            this.facebookConnect.TabIndex = 31;
            this.facebookConnect.Text = "Possui conexão Facebook";
            // 
            // Status
            // 
            this.Status.Checked = false;
            this.Status.Criterial = null;
            this.Status.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.Status.FormSelector = null;
            this.Status.Location = new System.Drawing.Point(143, 57);
            this.Status.Name = "Status";
            this.Status.SelectionInfo = null;
            this.Status.Size = new System.Drawing.Size(221, 50);
            this.Status.TabIndex = 30;
            this.Status.Text = "Status";
            // 
            // btDeleteMailing
            // 
            this.btDeleteMailing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteMailing.BackColor = System.Drawing.Color.Teal;
            this.btDeleteMailing.Enabled = false;
            this.btDeleteMailing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDeleteMailing.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDeleteMailing.ForeColor = System.Drawing.Color.White;
            this.btDeleteMailing.Location = new System.Drawing.Point(796, 542);
            this.btDeleteMailing.Name = "btDeleteMailing";
            this.btDeleteMailing.Size = new System.Drawing.Size(130, 32);
            this.btDeleteMailing.TabIndex = 29;
            this.btDeleteMailing.Text = "Excluir mailing";
            this.btDeleteMailing.UseVisualStyleBackColor = false;
            this.btDeleteMailing.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btModify
            // 
            this.btModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btModify.BackColor = System.Drawing.Color.Teal;
            this.btModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btModify.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btModify.ForeColor = System.Drawing.Color.White;
            this.btModify.Location = new System.Drawing.Point(349, 542);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(130, 32);
            this.btModify.TabIndex = 28;
            this.btModify.Text = "Modificar filtros";
            this.btModify.UseVisualStyleBackColor = false;
            this.btModify.Click += new System.EventHandler(this.btModify_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackColor = System.Drawing.Color.Teal;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.ForeColor = System.Drawing.Color.White;
            this.btClose.Location = new System.Drawing.Point(1107, 542);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(130, 32);
            this.btClose.TabIndex = 27;
            this.btClose.Text = "Fechar";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btExportMailing
            // 
            this.btExportMailing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExportMailing.BackColor = System.Drawing.Color.Teal;
            this.btExportMailing.Enabled = false;
            this.btExportMailing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExportMailing.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExportMailing.ForeColor = System.Drawing.Color.White;
            this.btExportMailing.Location = new System.Drawing.Point(932, 542);
            this.btExportMailing.Name = "btExportMailing";
            this.btExportMailing.Size = new System.Drawing.Size(130, 32);
            this.btExportMailing.TabIndex = 26;
            this.btExportMailing.Text = "Exportar mailing";
            this.btExportMailing.UseVisualStyleBackColor = false;
            this.btExportMailing.Click += new System.EventHandler(this.btExportMailing_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(214, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 33);
            this.label3.TabIndex = 25;
            this.label3.Text = "% de representação do grupo controle";
            // 
            // percControl
            // 
            this.percControl.Location = new System.Drawing.Point(471, 21);
            this.percControl.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.percControl.Name = "percControl";
            this.percControl.Size = new System.Drawing.Size(41, 20);
            this.percControl.TabIndex = 24;
            this.percControl.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.BackColor = System.Drawing.Color.Teal;
            this.btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClear.ForeColor = System.Drawing.Color.White;
            this.btClear.Location = new System.Drawing.Point(213, 542);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(130, 32);
            this.btClear.TabIndex = 23;
            this.btClear.Text = "Limpar";
            this.btClear.UseVisualStyleBackColor = false;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // birthDate
            // 
            this.birthDate.Checked = false;
            this.birthDate.Criterial = null;
            this.birthDate.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Date;
            this.birthDate.FormSelector = null;
            this.birthDate.Location = new System.Drawing.Point(375, 109);
            this.birthDate.Name = "birthDate";
            this.birthDate.SelectionInfo = null;
            this.birthDate.Size = new System.Drawing.Size(279, 50);
            this.birthDate.TabIndex = 22;
            this.birthDate.Text = "Data de aniversário";
            // 
            // btGenerateSql
            // 
            this.btGenerateSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btGenerateSql.BackColor = System.Drawing.Color.Teal;
            this.btGenerateSql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGenerateSql.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGenerateSql.ForeColor = System.Drawing.Color.White;
            this.btGenerateSql.Location = new System.Drawing.Point(524, 542);
            this.btGenerateSql.Name = "btGenerateSql";
            this.btGenerateSql.Size = new System.Drawing.Size(130, 32);
            this.btGenerateSql.TabIndex = 21;
            this.btGenerateSql.Text = "Gerar Sql";
            this.btGenerateSql.UseVisualStyleBackColor = false;
            this.btGenerateSql.Click += new System.EventHandler(this.btGenerateSql_Click);
            // 
            // Engagement
            // 
            this.Engagement.Checked = false;
            this.Engagement.Criterial = null;
            this.Engagement.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.Engagement.FormSelector = null;
            this.Engagement.Location = new System.Drawing.Point(375, 165);
            this.Engagement.Name = "Engagement";
            this.Engagement.SelectionInfo = null;
            this.Engagement.Size = new System.Drawing.Size(279, 50);
            this.Engagement.TabIndex = 11;
            this.Engagement.Text = "Engajamento";
            // 
            // btGenerateMailing
            // 
            this.btGenerateMailing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btGenerateMailing.BackColor = System.Drawing.Color.Teal;
            this.btGenerateMailing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGenerateMailing.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGenerateMailing.ForeColor = System.Drawing.Color.White;
            this.btGenerateMailing.Location = new System.Drawing.Point(660, 542);
            this.btGenerateMailing.Name = "btGenerateMailing";
            this.btGenerateMailing.Size = new System.Drawing.Size(130, 32);
            this.btGenerateMailing.TabIndex = 14;
            this.btGenerateMailing.Text = "Gerar mailing";
            this.btGenerateMailing.UseVisualStyleBackColor = false;
            this.btGenerateMailing.Click += new System.EventHandler(this.btGenerateMailing_Click);
            // 
            // Gender
            // 
            this.Gender.AccessibleDescription = "";
            this.Gender.BackColor = System.Drawing.Color.White;
            this.Gender.Checked = false;
            this.Gender.Location = new System.Drawing.Point(143, 162);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(221, 77);
            this.Gender.TabIndex = 6;
            this.Gender.Text = "Sexo";
            // 
            // grPurchaseProfile
            // 
            this.grPurchaseProfile.Controls.Add(this.rdNeverBuy);
            this.grPurchaseProfile.Controls.Add(this.rdBuyer);
            this.grPurchaseProfile.Controls.Add(this.rdAny);
            this.grPurchaseProfile.Controls.Add(this.pnPurchaseAny);
            this.grPurchaseProfile.Controls.Add(this.pnPurchaseProfile);
            this.grPurchaseProfile.Controls.Add(this.ckPurchaseProfile);
            this.grPurchaseProfile.Location = new System.Drawing.Point(3, 265);
            this.grPurchaseProfile.Name = "grPurchaseProfile";
            this.grPurchaseProfile.Size = new System.Drawing.Size(1237, 271);
            this.grPurchaseProfile.TabIndex = 4;
            this.grPurchaseProfile.TabStop = false;
            // 
            // rdNeverBuy
            // 
            this.rdNeverBuy.AutoSize = true;
            this.rdNeverBuy.Checked = true;
            this.rdNeverBuy.Enabled = false;
            this.rdNeverBuy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rdNeverBuy.Location = new System.Drawing.Point(20, 27);
            this.rdNeverBuy.Name = "rdNeverBuy";
            this.rdNeverBuy.Size = new System.Drawing.Size(120, 21);
            this.rdNeverBuy.TabIndex = 2;
            this.rdNeverBuy.TabStop = true;
            this.rdNeverBuy.Text = "Nunca comprou";
            this.rdNeverBuy.UseVisualStyleBackColor = true;
            this.rdNeverBuy.CheckedChanged += new System.EventHandler(this.rdNeverBuy_CheckedChanged);
            // 
            // rdBuyer
            // 
            this.rdBuyer.AutoSize = true;
            this.rdBuyer.Enabled = false;
            this.rdBuyer.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rdBuyer.Location = new System.Drawing.Point(140, 27);
            this.rdBuyer.Name = "rdBuyer";
            this.rdBuyer.Size = new System.Drawing.Size(95, 21);
            this.rdBuyer.TabIndex = 3;
            this.rdBuyer.Text = "Já comprou";
            this.rdBuyer.UseVisualStyleBackColor = true;
            this.rdBuyer.CheckedChanged += new System.EventHandler(this.rdNeverBuy_CheckedChanged);
            // 
            // rdAny
            // 
            this.rdAny.AutoSize = true;
            this.rdAny.Enabled = false;
            this.rdAny.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rdAny.Location = new System.Drawing.Point(260, 27);
            this.rdAny.Name = "rdAny";
            this.rdAny.Size = new System.Drawing.Size(80, 21);
            this.rdAny.TabIndex = 12;
            this.rdAny.TabStop = true;
            this.rdAny.Text = "Tanto faz";
            this.rdAny.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rdAny.UseVisualStyleBackColor = true;
            this.rdAny.CheckedChanged += new System.EventHandler(this.rdNeverBuy_CheckedChanged);
            // 
            // pnPurchaseAny
            // 
            this.pnPurchaseAny.Controls.Add(this.LastPurchaseUsePromocode);
            this.pnPurchaseAny.Controls.Add(this.HasAppPurchase);
            this.pnPurchaseAny.Controls.Add(this.BuyOffers);
            this.pnPurchaseAny.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnPurchaseAny.Enabled = false;
            this.pnPurchaseAny.Location = new System.Drawing.Point(921, 16);
            this.pnPurchaseAny.Name = "pnPurchaseAny";
            this.pnPurchaseAny.Size = new System.Drawing.Size(313, 252);
            this.pnPurchaseAny.TabIndex = 35;
            // 
            // LastPurchaseUsePromocode
            // 
            this.LastPurchaseUsePromocode.Checked = false;
            this.LastPurchaseUsePromocode.Criterial = null;
            this.LastPurchaseUsePromocode.Location = new System.Drawing.Point(7, 151);
            this.LastPurchaseUsePromocode.Name = "LastPurchaseUsePromocode";
            this.LastPurchaseUsePromocode.Size = new System.Drawing.Size(299, 50);
            this.LastPurchaseUsePromocode.TabIndex = 34;
            this.LastPurchaseUsePromocode.Text = "Última Compra com Vale Presente";
            // 
            // HasAppPurchase
            // 
            this.HasAppPurchase.Checked = false;
            this.HasAppPurchase.Criterial = null;
            this.HasAppPurchase.Location = new System.Drawing.Point(7, 84);
            this.HasAppPurchase.Name = "HasAppPurchase";
            this.HasAppPurchase.Size = new System.Drawing.Size(299, 51);
            this.HasAppPurchase.TabIndex = 33;
            this.HasAppPurchase.Text = "Já comprou pelo App";
            // 
            // BuyOffers
            // 
            this.BuyOffers.Checked = false;
            this.BuyOffers.Criterial = null;
            this.BuyOffers.FormSelector = null;
            this.BuyOffers.IdField = null;
            this.BuyOffers.Location = new System.Drawing.Point(7, 3);
            this.BuyOffers.Name = "BuyOffers";
            this.BuyOffers.NameField = null;
            this.BuyOffers.QualifiedObjectName = null;
            this.BuyOffers.QueryEecutor = null;
            this.BuyOffers.SelectionInfo = null;
            this.BuyOffers.Size = new System.Drawing.Size(299, 77);
            this.BuyOffers.TabIndex = 32;
            this.BuyOffers.TemplateExpression = "";
            this.BuyOffers.Text = "Já comprou estas ofertas";
            // 
            // pnPurchaseProfile
            // 
            this.pnPurchaseProfile.Controls.Add(this.PromocodeName);
            this.pnPurchaseProfile.Controls.Add(this.maxPurchaseValue);
            this.pnPurchaseProfile.Controls.Add(this.Promocodes);
            this.pnPurchaseProfile.Controls.Add(this.NetRevenue);
            this.pnPurchaseProfile.Controls.Add(this.QTYPurchases);
            this.pnPurchaseProfile.Controls.Add(this.dateLastPurchase);
            this.pnPurchaseProfile.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPurchaseProfile.Enabled = false;
            this.pnPurchaseProfile.Location = new System.Drawing.Point(3, 16);
            this.pnPurchaseProfile.Name = "pnPurchaseProfile";
            this.pnPurchaseProfile.Size = new System.Drawing.Size(921, 252);
            this.pnPurchaseProfile.TabIndex = 1;
            // 
            // PromocodeName
            // 
            this.PromocodeName.Checked = false;
            this.PromocodeName.Criterial = null;
            this.PromocodeName.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Text;
            this.PromocodeName.FormSelector = null;
            this.PromocodeName.Location = new System.Drawing.Point(288, 143);
            this.PromocodeName.Name = "PromocodeName";
            this.PromocodeName.SelectionInfo = null;
            this.PromocodeName.Size = new System.Drawing.Size(279, 50);
            this.PromocodeName.TabIndex = 13;
            this.PromocodeName.Text = "Código de Vale Presente usado";
            // 
            // maxPurchaseValue
            // 
            this.maxPurchaseValue.Checked = false;
            this.maxPurchaseValue.Criterial = null;
            this.maxPurchaseValue.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.maxPurchaseValue.FormSelector = null;
            this.maxPurchaseValue.Location = new System.Drawing.Point(288, 87);
            this.maxPurchaseValue.Name = "maxPurchaseValue";
            this.maxPurchaseValue.SelectionInfo = null;
            this.maxPurchaseValue.Size = new System.Drawing.Size(279, 50);
            this.maxPurchaseValue.TabIndex = 11;
            this.maxPurchaseValue.Text = "Maior valor de compra";
            // 
            // Promocodes
            // 
            this.Promocodes.Checked = false;
            this.Promocodes.Criterial = null;
            this.Promocodes.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.Promocodes.FormSelector = null;
            this.Promocodes.Location = new System.Drawing.Point(573, 143);
            this.Promocodes.Name = "Promocodes";
            this.Promocodes.SelectionInfo = null;
            this.Promocodes.Size = new System.Drawing.Size(279, 50);
            this.Promocodes.TabIndex = 6;
            this.Promocodes.Text = "Valor utilizado de Vale presente";
            // 
            // NetRevenue
            // 
            this.NetRevenue.Checked = false;
            this.NetRevenue.Criterial = null;
            this.NetRevenue.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.NetRevenue.FormSelector = null;
            this.NetRevenue.Location = new System.Drawing.Point(3, 143);
            this.NetRevenue.Name = "NetRevenue";
            this.NetRevenue.SelectionInfo = null;
            this.NetRevenue.Size = new System.Drawing.Size(279, 50);
            this.NetRevenue.TabIndex = 5;
            this.NetRevenue.Text = "Receita Op. Líquida";
            // 
            // QTYPurchases
            // 
            this.QTYPurchases.Checked = false;
            this.QTYPurchases.Criterial = null;
            this.QTYPurchases.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.QTYPurchases.FormSelector = null;
            this.QTYPurchases.Location = new System.Drawing.Point(573, 87);
            this.QTYPurchases.Name = "QTYPurchases";
            this.QTYPurchases.SelectionInfo = null;
            this.QTYPurchases.Size = new System.Drawing.Size(279, 50);
            this.QTYPurchases.TabIndex = 4;
            this.QTYPurchases.Text = "Quantidade de compras";
            // 
            // dateLastPurchase
            // 
            this.dateLastPurchase.Checked = false;
            this.dateLastPurchase.Criterial = null;
            this.dateLastPurchase.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Date;
            this.dateLastPurchase.FormSelector = null;
            this.dateLastPurchase.Location = new System.Drawing.Point(3, 87);
            this.dateLastPurchase.Name = "dateLastPurchase";
            this.dateLastPurchase.SelectionInfo = null;
            this.dateLastPurchase.Size = new System.Drawing.Size(279, 50);
            this.dateLastPurchase.TabIndex = 3;
            this.dateLastPurchase.Text = "Data da última compra";
            // 
            // ckPurchaseProfile
            // 
            this.ckPurchaseProfile.AutoSize = true;
            this.ckPurchaseProfile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckPurchaseProfile.Location = new System.Drawing.Point(6, 0);
            this.ckPurchaseProfile.Name = "ckPurchaseProfile";
            this.ckPurchaseProfile.Size = new System.Drawing.Size(129, 21);
            this.ckPurchaseProfile.TabIndex = 0;
            this.ckPurchaseProfile.Text = "Perfil de compra";
            this.ckPurchaseProfile.UseVisualStyleBackColor = true;
            this.ckPurchaseProfile.CheckedChanged += new System.EventHandler(this.ckPurchaseProfile_CheckedChanged);
            // 
            // dtsUserRegistered
            // 
            this.dtsUserRegistered.Checked = false;
            this.dtsUserRegistered.Criterial = null;
            this.dtsUserRegistered.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Date;
            this.dtsUserRegistered.FormSelector = null;
            this.dtsUserRegistered.Location = new System.Drawing.Point(375, 57);
            this.dtsUserRegistered.Name = "dtsUserRegistered";
            this.dtsUserRegistered.SelectionInfo = null;
            this.dtsUserRegistered.Size = new System.Drawing.Size(279, 50);
            this.dtsUserRegistered.TabIndex = 2;
            this.dtsUserRegistered.Text = "Data de cadastro";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tbParameter);
            this.tabControl1.Controls.Add(this.tbSql);
            this.tabControl1.Controls.Add(this.tbSeeded);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1257, 612);
            this.tabControl1.TabIndex = 11;
            // 
            // tbParameter
            // 
            this.tbParameter.Controls.Add(this.grMailingSelector);
            this.tbParameter.Location = new System.Drawing.Point(4, 4);
            this.tbParameter.Name = "tbParameter";
            this.tbParameter.Padding = new System.Windows.Forms.Padding(3);
            this.tbParameter.Size = new System.Drawing.Size(1249, 586);
            this.tbParameter.TabIndex = 0;
            this.tbParameter.Text = "Parâmetros";
            this.tbParameter.UseVisualStyleBackColor = true;
            // 
            // tbSql
            // 
            this.tbSql.Controls.Add(this.txtSql);
            this.tbSql.Location = new System.Drawing.Point(4, 4);
            this.tbSql.Name = "tbSql";
            this.tbSql.Padding = new System.Windows.Forms.Padding(3);
            this.tbSql.Size = new System.Drawing.Size(1249, 586);
            this.tbSql.TabIndex = 1;
            this.tbSql.Text = "Sql";
            this.tbSql.UseVisualStyleBackColor = true;
            // 
            // txtSql
            // 
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSql.Location = new System.Drawing.Point(3, 3);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(1243, 580);
            this.txtSql.TabIndex = 0;
            this.txtSql.Text = "";
            // 
            // tbSeeded
            // 
            this.tbSeeded.Controls.Add(this.label2);
            this.tbSeeded.Controls.Add(this.txtSemeados);
            this.tbSeeded.Controls.Add(this.txtSeededs);
            this.tbSeeded.Location = new System.Drawing.Point(4, 4);
            this.tbSeeded.Name = "tbSeeded";
            this.tbSeeded.Size = new System.Drawing.Size(1249, 586);
            this.tbSeeded.TabIndex = 2;
            this.tbSeeded.Text = "Semeados";
            this.tbSeeded.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(400, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Ids Semeados separados por vírgula";
            // 
            // txtSemeados
            // 
            this.txtSemeados.Location = new System.Drawing.Point(397, 32);
            this.txtSemeados.Name = "txtSemeados";
            this.txtSemeados.Size = new System.Drawing.Size(287, 20);
            this.txtSemeados.TabIndex = 26;
            // 
            // txtSeededs
            // 
            this.txtSeededs.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSeededs.Location = new System.Drawing.Point(0, 0);
            this.txtSeededs.Name = "txtSeededs";
            this.txtSeededs.Size = new System.Drawing.Size(383, 586);
            this.txtSeededs.TabIndex = 0;
            this.txtSeededs.Title = "Lista de semeados";
            // 
            // exportMailing
            // 
            this.exportMailing.FileOk += new System.ComponentModel.CancelEventHandler(this.exportMailing_FileOk);
            // 
            // dateSelector2
            // 
            this.dateSelector2.Checked = false;
            this.dateSelector2.Criterial = null;
            this.dateSelector2.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.dateSelector2.FormSelector = null;
            this.dateSelector2.Location = new System.Drawing.Point(3, 15);
            this.dateSelector2.Name = "dateSelector2";
            this.dateSelector2.SelectionInfo = null;
            this.dateSelector2.Size = new System.Drawing.Size(299, 79);
            this.dateSelector2.TabIndex = 3;
            this.dateSelector2.Text = "Data da última compra";
            // 
            // valueSelector1
            // 
            this.valueSelector1.Checked = false;
            this.valueSelector1.Criterial = null;
            this.valueSelector1.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.valueSelector1.FormSelector = null;
            this.valueSelector1.Location = new System.Drawing.Point(4, 101);
            this.valueSelector1.Name = "valueSelector1";
            this.valueSelector1.SelectionInfo = null;
            this.valueSelector1.Size = new System.Drawing.Size(299, 77);
            this.valueSelector1.TabIndex = 4;
            this.valueSelector1.Text = "Quantidade de compras";
            // 
            // MailingSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "MailingSelector";
            this.Size = new System.Drawing.Size(1257, 612);
            this.Load += new System.EventHandler(this.MailingSelector_Load);
            this.grMailingSelector.ResumeLayout(false);
            this.grMailingSelector.PerformLayout();
            this.pnSite.ResumeLayout(false);
            this.pnSite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percControl)).EndInit();
            this.grPurchaseProfile.ResumeLayout(false);
            this.grPurchaseProfile.PerformLayout();
            this.pnPurchaseAny.ResumeLayout(false);
            this.pnPurchaseProfile.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbParameter.ResumeLayout(false);
            this.tbSql.ResumeLayout(false);
            this.tbSeeded.ResumeLayout(false);
            this.tbSeeded.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grMailingSelector;
        private Selector dtsUserRegistered;
        private Selector dateLastPurchase;
        private System.Windows.Forms.GroupBox grPurchaseProfile;
        private System.Windows.Forms.RadioButton rdBuyer;
        private System.Windows.Forms.RadioButton rdNeverBuy;
        private System.Windows.Forms.Panel pnPurchaseProfile;
        private System.Windows.Forms.CheckBox ckPurchaseProfile;
        private Selector QTYPurchases;
        private Selector dateSelector2;
        private Selector valueSelector1;
        private Selector Promocodes;
        private Selector NetRevenue;
        private ListSelector Gender;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbParameter;
        private System.Windows.Forms.TabPage tbSql;
        private Selector Engagement;
        private System.Windows.Forms.RichTextBox txtSql;
        private System.Windows.Forms.Button btGenerateMailing;
        private StoredObjectSelector CampaignExclusion;
        private StoredObjectSelector MailingList;
        private StoredObjectSelector AgeRange;
        private System.Windows.Forms.Button btGenerateSql;
        private System.Windows.Forms.SaveFileDialog exportMailing;
        private Selector birthDate;
        private System.Windows.Forms.Button btClear;
        private Selector maxPurchaseValue;
        private System.Windows.Forms.TabPage tbSeeded;
        private TextContent txtSeededs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSemeados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown percControl;
        private System.Windows.Forms.Button btExportMailing;
        private System.Windows.Forms.Button btModify;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDeleteMailing;
        private Selector Status;
        private BoolSelector facebookConnect;
        private BigListSelector BuyOffers;
        private System.Windows.Forms.RadioButton rdAny;
        private System.Windows.Forms.Panel pnPurchaseAny;
        private BoolSelector HasAppPurchase;
        private BoolSelector LastPurchaseUsePromocode;
        private Selector PromocodeName;
        private StoredObjectSelector customizedLists;
        private System.Windows.Forms.Button btCategories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnSite;
        private System.Windows.Forms.RadioButton rdGroupon;
        private System.Windows.Forms.RadioButton rdPU;
    }
}
