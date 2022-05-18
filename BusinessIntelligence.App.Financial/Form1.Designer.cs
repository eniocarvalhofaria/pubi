namespace BusinessIntelligence.App.Financial
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbCreateAccount = new System.Windows.Forms.TabPage();
            this.btClose = new System.Windows.Forms.Button();
            this.btCreate = new System.Windows.Forms.Button();
            this.grTax = new System.Windows.Forms.GroupBox();
            this.ckCOFINS = new System.Windows.Forms.CheckBox();
            this.ckPIS = new System.Windows.Forms.CheckBox();
            this.ckISS = new System.Windows.Forms.CheckBox();
            this.ckTax = new System.Windows.Forms.CheckBox();
            this.ckAnotherAccount = new System.Windows.Forms.CheckBox();
            this.grAnotherAccount = new System.Windows.Forms.GroupBox();
            this.cbAnotherAccount = new System.Windows.Forms.ComboBox();
            this.grManagement = new System.Windows.Forms.GroupBox();
            this.tvManagement = new System.Windows.Forms.TreeView();
            this.tvImages = new System.Windows.Forms.ImageList(this.components);
            this.txtManagementGroup = new System.Windows.Forms.Label();
            this.grAccounting = new System.Windows.Forms.GroupBox();
            this.tvAccounting = new System.Windows.Forms.TreeView();
            this.txtAccountingGroup = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCod = new System.Windows.Forms.TextBox();
            this.tbAcountNotRegistered = new System.Windows.Forms.TabPage();
            this.btRefreshAccountNotRegistered = new System.Windows.Forms.Button();
            this.dgAccountNotRegistered = new System.Windows.Forms.DataGridView();
            this.tbManagementAdjustment = new System.Windows.Forms.TabPage();
            this.btCreateAdjustment = new System.Windows.Forms.Button();
            this.grAdjustment = new System.Windows.Forms.GroupBox();
            this.btClear = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.lblAddAdjustments = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtValueAdjustment = new System.Windows.Forms.TextBox();
            this.dtAdjustment = new System.Windows.Forms.DateTimePicker();
            this.btAdd = new System.Windows.Forms.Button();
            this.grInvoice = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.dtInvoice = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAccount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbAdjustmentType = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tbCreateAccount.SuspendLayout();
            this.grTax.SuspendLayout();
            this.grAnotherAccount.SuspendLayout();
            this.grManagement.SuspendLayout();
            this.grAccounting.SuspendLayout();
            this.tbAcountNotRegistered.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccountNotRegistered)).BeginInit();
            this.tbManagementAdjustment.SuspendLayout();
            this.grAdjustment.SuspendLayout();
            this.grInvoice.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbCreateAccount);
            this.tabControl1.Controls.Add(this.tbAcountNotRegistered);
            this.tabControl1.Controls.Add(this.tbManagementAdjustment);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 396);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.OnSelectIndexChange);
            // 
            // tbCreateAccount
            // 
            this.tbCreateAccount.Controls.Add(this.btClose);
            this.tbCreateAccount.Controls.Add(this.btCreate);
            this.tbCreateAccount.Controls.Add(this.grTax);
            this.tbCreateAccount.Controls.Add(this.ckTax);
            this.tbCreateAccount.Controls.Add(this.ckAnotherAccount);
            this.tbCreateAccount.Controls.Add(this.grAnotherAccount);
            this.tbCreateAccount.Controls.Add(this.grManagement);
            this.tbCreateAccount.Controls.Add(this.grAccounting);
            this.tbCreateAccount.Controls.Add(this.label2);
            this.tbCreateAccount.Controls.Add(this.txtDescription);
            this.tbCreateAccount.Controls.Add(this.label1);
            this.tbCreateAccount.Controls.Add(this.txtCod);
            this.tbCreateAccount.Location = new System.Drawing.Point(4, 22);
            this.tbCreateAccount.Name = "tbCreateAccount";
            this.tbCreateAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tbCreateAccount.Size = new System.Drawing.Size(1152, 370);
            this.tbCreateAccount.TabIndex = 0;
            this.tbCreateAccount.Text = "Cadastro/Alteração de contas";
            this.tbCreateAccount.UseVisualStyleBackColor = true;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(994, 344);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 12;
            this.btClose.Text = "Fechar";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(1075, 344);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(75, 23);
            this.btCreate.TabIndex = 11;
            this.btCreate.Text = "Cadastrar";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.OnCreate);
            // 
            // grTax
            // 
            this.grTax.Controls.Add(this.ckCOFINS);
            this.grTax.Controls.Add(this.ckPIS);
            this.grTax.Controls.Add(this.ckISS);
            this.grTax.Enabled = false;
            this.grTax.Location = new System.Drawing.Point(132, 228);
            this.grTax.Name = "grTax";
            this.grTax.Size = new System.Drawing.Size(292, 62);
            this.grTax.TabIndex = 10;
            this.grTax.TabStop = false;
            this.grTax.Text = "Impostos incidentes";
            // 
            // ckCOFINS
            // 
            this.ckCOFINS.AutoSize = true;
            this.ckCOFINS.Location = new System.Drawing.Point(208, 29);
            this.ckCOFINS.Name = "ckCOFINS";
            this.ckCOFINS.Size = new System.Drawing.Size(65, 17);
            this.ckCOFINS.TabIndex = 2;
            this.ckCOFINS.Text = "COFINS";
            this.ckCOFINS.UseVisualStyleBackColor = true;
            // 
            // ckPIS
            // 
            this.ckPIS.AutoSize = true;
            this.ckPIS.Location = new System.Drawing.Point(113, 29);
            this.ckPIS.Name = "ckPIS";
            this.ckPIS.Size = new System.Drawing.Size(43, 17);
            this.ckPIS.TabIndex = 1;
            this.ckPIS.Text = "PIS";
            this.ckPIS.UseVisualStyleBackColor = true;
            // 
            // ckISS
            // 
            this.ckISS.AutoSize = true;
            this.ckISS.Location = new System.Drawing.Point(14, 29);
            this.ckISS.Name = "ckISS";
            this.ckISS.Size = new System.Drawing.Size(43, 17);
            this.ckISS.TabIndex = 0;
            this.ckISS.Text = "ISS";
            this.ckISS.UseVisualStyleBackColor = true;
            // 
            // ckTax
            // 
            this.ckTax.AutoSize = true;
            this.ckTax.Location = new System.Drawing.Point(23, 257);
            this.ckTax.Name = "ckTax";
            this.ckTax.Size = new System.Drawing.Size(94, 17);
            this.ckTax.TabIndex = 9;
            this.ckTax.Text = "Incide imposto";
            this.ckTax.UseVisualStyleBackColor = true;
            this.ckTax.CheckedChanged += new System.EventHandler(this.OnckTaxChange);
            // 
            // ckAnotherAccount
            // 
            this.ckAnotherAccount.AutoSize = true;
            this.ckAnotherAccount.Location = new System.Drawing.Point(23, 112);
            this.ckAnotherAccount.Name = "ckAnotherAccount";
            this.ckAnotherAccount.Size = new System.Drawing.Size(179, 17);
            this.ckAnotherAccount.TabIndex = 8;
            this.ckAnotherAccount.Text = "Colocar no grupo de outra conta";
            this.ckAnotherAccount.UseVisualStyleBackColor = true;
            this.ckAnotherAccount.CheckedChanged += new System.EventHandler(this.OnAnotherAccountChange);
            // 
            // grAnotherAccount
            // 
            this.grAnotherAccount.Controls.Add(this.cbAnotherAccount);
            this.grAnotherAccount.Enabled = false;
            this.grAnotherAccount.Location = new System.Drawing.Point(11, 135);
            this.grAnotherAccount.Name = "grAnotherAccount";
            this.grAnotherAccount.Size = new System.Drawing.Size(413, 76);
            this.grAnotherAccount.TabIndex = 7;
            this.grAnotherAccount.TabStop = false;
            this.grAnotherAccount.Text = "Outra conta do mesmo grupo";
            // 
            // cbAnotherAccount
            // 
            this.cbAnotherAccount.FormattingEnabled = true;
            this.cbAnotherAccount.Location = new System.Drawing.Point(12, 33);
            this.cbAnotherAccount.Name = "cbAnotherAccount";
            this.cbAnotherAccount.Size = new System.Drawing.Size(395, 21);
            this.cbAnotherAccount.TabIndex = 0;
            this.cbAnotherAccount.SelectedIndexChanged += new System.EventHandler(this.OnSelect);
            // 
            // grManagement
            // 
            this.grManagement.Controls.Add(this.tvManagement);
            this.grManagement.Controls.Add(this.txtManagementGroup);
            this.grManagement.Location = new System.Drawing.Point(793, 6);
            this.grManagement.Name = "grManagement";
            this.grManagement.Size = new System.Drawing.Size(357, 332);
            this.grManagement.TabIndex = 5;
            this.grManagement.TabStop = false;
            this.grManagement.Text = "Grupo Gerencial";
            // 
            // tvManagement
            // 
            this.tvManagement.ImageIndex = 0;
            this.tvManagement.ImageList = this.tvImages;
            this.tvManagement.Location = new System.Drawing.Point(7, 19);
            this.tvManagement.Name = "tvManagement";
            this.tvManagement.SelectedImageIndex = 0;
            this.tvManagement.Size = new System.Drawing.Size(344, 277);
            this.tvManagement.TabIndex = 1;
            this.tvManagement.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTvDbClick);
            this.tvManagement.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTvDbClick);
            // 
            // tvImages
            // 
            this.tvImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tvImages.ImageStream")));
            this.tvImages.TransparentColor = System.Drawing.Color.Transparent;
            this.tvImages.Images.SetKeyName(0, "BolinhaAzul.png");
            this.tvImages.Images.SetKeyName(1, "SetaLaranja.png");
            // 
            // txtManagementGroup
            // 
            this.txtManagementGroup.Location = new System.Drawing.Point(6, 302);
            this.txtManagementGroup.Name = "txtManagementGroup";
            this.txtManagementGroup.Size = new System.Drawing.Size(345, 20);
            this.txtManagementGroup.TabIndex = 0;
            // 
            // grAccounting
            // 
            this.grAccounting.Controls.Add(this.tvAccounting);
            this.grAccounting.Controls.Add(this.txtAccountingGroup);
            this.grAccounting.Location = new System.Drawing.Point(430, 6);
            this.grAccounting.Name = "grAccounting";
            this.grAccounting.Size = new System.Drawing.Size(357, 332);
            this.grAccounting.TabIndex = 4;
            this.grAccounting.TabStop = false;
            this.grAccounting.Text = "Grupo Contabilidade";
            // 
            // tvAccounting
            // 
            this.tvAccounting.ImageIndex = 0;
            this.tvAccounting.ImageList = this.tvImages;
            this.tvAccounting.Location = new System.Drawing.Point(7, 19);
            this.tvAccounting.Name = "tvAccounting";
            this.tvAccounting.SelectedImageIndex = 0;
            this.tvAccounting.Size = new System.Drawing.Size(344, 277);
            this.tvAccounting.TabIndex = 1;
            this.tvAccounting.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTvDbClick);
            this.tvAccounting.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTvDbClick);
            // 
            // txtAccountingGroup
            // 
            this.txtAccountingGroup.Location = new System.Drawing.Point(6, 302);
            this.txtAccountingGroup.Name = "txtAccountingGroup";
            this.txtAccountingGroup.Size = new System.Drawing.Size(345, 20);
            this.txtAccountingGroup.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descrição";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(111, 62);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(307, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código da conta";
            // 
            // txtCod
            // 
            this.txtCod.Location = new System.Drawing.Point(111, 28);
            this.txtCod.Name = "txtCod";
            this.txtCod.Size = new System.Drawing.Size(100, 20);
            this.txtCod.TabIndex = 0;
            this.txtCod.TextChanged += new System.EventHandler(this.txtCod_TextChanged);
            this.txtCod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnCodKeyPress);
            // 
            // tbAcountNotRegistered
            // 
            this.tbAcountNotRegistered.Controls.Add(this.btRefreshAccountNotRegistered);
            this.tbAcountNotRegistered.Controls.Add(this.dgAccountNotRegistered);
            this.tbAcountNotRegistered.Location = new System.Drawing.Point(4, 22);
            this.tbAcountNotRegistered.Name = "tbAcountNotRegistered";
            this.tbAcountNotRegistered.Size = new System.Drawing.Size(1152, 370);
            this.tbAcountNotRegistered.TabIndex = 1;
            this.tbAcountNotRegistered.Text = "Contas não cadastradas";
            this.tbAcountNotRegistered.UseVisualStyleBackColor = true;
            // 
            // btRefreshAccountNotRegistered
            // 
            this.btRefreshAccountNotRegistered.Location = new System.Drawing.Point(498, 342);
            this.btRefreshAccountNotRegistered.Name = "btRefreshAccountNotRegistered";
            this.btRefreshAccountNotRegistered.Size = new System.Drawing.Size(75, 23);
            this.btRefreshAccountNotRegistered.TabIndex = 1;
            this.btRefreshAccountNotRegistered.Text = "Atualizar";
            this.btRefreshAccountNotRegistered.UseVisualStyleBackColor = true;
            this.btRefreshAccountNotRegistered.Click += new System.EventHandler(this.btRefreshAccountNotRegistered_Click);
            // 
            // dgAccountNotRegistered
            // 
            this.dgAccountNotRegistered.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAccountNotRegistered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAccountNotRegistered.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgAccountNotRegistered.Location = new System.Drawing.Point(0, 0);
            this.dgAccountNotRegistered.Name = "dgAccountNotRegistered";
            this.dgAccountNotRegistered.Size = new System.Drawing.Size(1152, 336);
            this.dgAccountNotRegistered.TabIndex = 0;
            // 
            // tbManagementAdjustment
            // 
            this.tbManagementAdjustment.Controls.Add(this.cbAdjustmentType);
            this.tbManagementAdjustment.Controls.Add(this.btCreateAdjustment);
            this.tbManagementAdjustment.Controls.Add(this.grAdjustment);
            this.tbManagementAdjustment.Controls.Add(this.grInvoice);
            this.tbManagementAdjustment.Location = new System.Drawing.Point(4, 22);
            this.tbManagementAdjustment.Name = "tbManagementAdjustment";
            this.tbManagementAdjustment.Size = new System.Drawing.Size(1152, 370);
            this.tbManagementAdjustment.TabIndex = 2;
            this.tbManagementAdjustment.Text = "Ajustes gerenciais";
            this.tbManagementAdjustment.UseVisualStyleBackColor = true;
            // 
            // btCreateAdjustment
            // 
            this.btCreateAdjustment.Location = new System.Drawing.Point(428, 319);
            this.btCreateAdjustment.Name = "btCreateAdjustment";
            this.btCreateAdjustment.Size = new System.Drawing.Size(98, 23);
            this.btCreateAdjustment.TabIndex = 8;
            this.btCreateAdjustment.Text = "Cadastrar ajuste";
            this.btCreateAdjustment.UseVisualStyleBackColor = true;
            this.btCreateAdjustment.Click += new System.EventHandler(this.OnCreateAdjustment);
            // 
            // grAdjustment
            // 
            this.grAdjustment.Controls.Add(this.btClear);
            this.grAdjustment.Controls.Add(this.label10);
            this.grAdjustment.Controls.Add(this.lblAddAdjustments);
            this.grAdjustment.Controls.Add(this.label9);
            this.grAdjustment.Controls.Add(this.label8);
            this.grAdjustment.Controls.Add(this.txtValueAdjustment);
            this.grAdjustment.Controls.Add(this.dtAdjustment);
            this.grAdjustment.Controls.Add(this.btAdd);
            this.grAdjustment.Location = new System.Drawing.Point(19, 166);
            this.grAdjustment.Name = "grAdjustment";
            this.grAdjustment.Size = new System.Drawing.Size(507, 147);
            this.grAdjustment.TabIndex = 1;
            this.grAdjustment.TabStop = false;
            this.grAdjustment.Text = "Ajustes";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(127, 115);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(98, 23);
            this.btClear.TabIndex = 7;
            this.btClear.Text = "Limpar tudo";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.OnAdjustmentClear);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(262, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "label10";
            // 
            // lblAddAdjustments
            // 
            this.lblAddAdjustments.AutoSize = true;
            this.lblAddAdjustments.Location = new System.Drawing.Point(18, 319);
            this.lblAddAdjustments.Name = "lblAddAdjustments";
            this.lblAddAdjustments.Size = new System.Drawing.Size(0, 13);
            this.lblAddAdjustments.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Valor (somente números)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Mês do ajuste";
            // 
            // txtValueAdjustment
            // 
            this.txtValueAdjustment.Location = new System.Drawing.Point(147, 60);
            this.txtValueAdjustment.Name = "txtValueAdjustment";
            this.txtValueAdjustment.Size = new System.Drawing.Size(78, 20);
            this.txtValueAdjustment.TabIndex = 2;
            this.txtValueAdjustment.Text = ",00";
            this.txtValueAdjustment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnDecimalKeyPress);
            // 
            // dtAdjustment
            // 
            this.dtAdjustment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAdjustment.Location = new System.Drawing.Point(110, 26);
            this.dtAdjustment.Name = "dtAdjustment";
            this.dtAdjustment.Size = new System.Drawing.Size(115, 20);
            this.dtAdjustment.TabIndex = 1;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(127, 86);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(98, 23);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "Adicionar ajuste";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.OnAddAdjustment);
            // 
            // grInvoice
            // 
            this.grInvoice.Controls.Add(this.label11);
            this.grInvoice.Controls.Add(this.label7);
            this.grInvoice.Controls.Add(this.txtSupplier);
            this.grInvoice.Controls.Add(this.dtInvoice);
            this.grInvoice.Controls.Add(this.label6);
            this.grInvoice.Controls.Add(this.label5);
            this.grInvoice.Controls.Add(this.txtInvoiceNumber);
            this.grInvoice.Controls.Add(this.label4);
            this.grInvoice.Controls.Add(this.cbAccount);
            this.grInvoice.Controls.Add(this.label3);
            this.grInvoice.Controls.Add(this.txtDocument);
            this.grInvoice.Location = new System.Drawing.Point(19, 12);
            this.grInvoice.Name = "grInvoice";
            this.grInvoice.Size = new System.Drawing.Size(507, 148);
            this.grInvoice.TabIndex = 0;
            this.grInvoice.TabStop = false;
            this.grInvoice.Text = "Nota Fiscal";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Fornecedor";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(142, 113);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(348, 20);
            this.txtSupplier.TabIndex = 9;
            // 
            // dtInvoice
            // 
            this.dtInvoice.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInvoice.Location = new System.Drawing.Point(142, 87);
            this.dtInvoice.Name = "dtInvoice";
            this.dtInvoice.Size = new System.Drawing.Size(114, 20);
            this.dtInvoice.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Data da Nota fiscal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Número da Nota fiscal";
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(390, 58);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(100, 20);
            this.txtInvoiceNumber.TabIndex = 4;
            this.txtInvoiceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnCodKeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Conta associada";
            // 
            // cbAccount
            // 
            this.cbAccount.FormattingEnabled = true;
            this.cbAccount.Location = new System.Drawing.Point(142, 31);
            this.cbAccount.Name = "cbAccount";
            this.cbAccount.Size = new System.Drawing.Size(348, 21);
            this.cbAccount.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Número do documento";
            // 
            // txtDocument
            // 
            this.txtDocument.Location = new System.Drawing.Point(142, 58);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(114, 20);
            this.txtDocument.TabIndex = 0;
            this.txtDocument.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnCodKeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(262, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Tipo de ajuste";
            // 
            // cbAdjustmentType
            // 
            this.cbAdjustmentType.FormattingEnabled = true;
            this.cbAdjustmentType.Location = new System.Drawing.Point(361, 99);
            this.cbAdjustmentType.Name = "cbAdjustmentType";
            this.cbAdjustmentType.Size = new System.Drawing.Size(148, 21);
            this.cbAdjustmentType.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 396);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BI-Financeiro";
            this.Load += new System.EventHandler(this.OnLoad);
            this.tabControl1.ResumeLayout(false);
            this.tbCreateAccount.ResumeLayout(false);
            this.tbCreateAccount.PerformLayout();
            this.grTax.ResumeLayout(false);
            this.grTax.PerformLayout();
            this.grAnotherAccount.ResumeLayout(false);
            this.grManagement.ResumeLayout(false);
            this.grAccounting.ResumeLayout(false);
            this.tbAcountNotRegistered.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAccountNotRegistered)).EndInit();
            this.tbManagementAdjustment.ResumeLayout(false);
            this.grAdjustment.ResumeLayout(false);
            this.grAdjustment.PerformLayout();
            this.grInvoice.ResumeLayout(false);
            this.grInvoice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbCreateAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox grAccounting;
        private System.Windows.Forms.TreeView tvAccounting;
        private System.Windows.Forms.Label txtAccountingGroup;
        private System.Windows.Forms.GroupBox grManagement;
        private System.Windows.Forms.TreeView tvManagement;
        private System.Windows.Forms.Label txtManagementGroup;
        private System.Windows.Forms.CheckBox ckAnotherAccount;
        private System.Windows.Forms.GroupBox grAnotherAccount;
        private System.Windows.Forms.ComboBox cbAnotherAccount;
        private System.Windows.Forms.GroupBox grTax;
        private System.Windows.Forms.CheckBox ckCOFINS;
        private System.Windows.Forms.CheckBox ckPIS;
        private System.Windows.Forms.CheckBox ckISS;
        private System.Windows.Forms.CheckBox ckTax;
        private System.Windows.Forms.ImageList tvImages;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.TabPage tbAcountNotRegistered;
        private System.Windows.Forms.Button btRefreshAccountNotRegistered;
        private System.Windows.Forms.DataGridView dgAccountNotRegistered;
        private System.Windows.Forms.TabPage tbManagementAdjustment;
        private System.Windows.Forms.GroupBox grInvoice;
        private System.Windows.Forms.TextBox txtDocument;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAccount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.DateTimePicker dtInvoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.GroupBox grAdjustment;
        private System.Windows.Forms.Label lblAddAdjustments;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtValueAdjustment;
        private System.Windows.Forms.DateTimePicker dtAdjustment;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btCreateAdjustment;
        private System.Windows.Forms.ComboBox cbAdjustmentType;
        private System.Windows.Forms.Label label11;
    }
}

