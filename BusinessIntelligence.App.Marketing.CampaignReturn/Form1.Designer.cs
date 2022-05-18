namespace BusinessIntelligence.App.Marketing.CampaignReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btRegisterDeals = new System.Windows.Forms.Button();
            this.dealsSelector1 = new BusinessIntelligence.App.Marketing.DealsSelector();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDealsCount = new System.Windows.Forms.TextBox();
            this.txtCountControl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCountMailing = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btClear = new System.Windows.Forms.Button();
            this.txtPromocode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btCreateCampaign = new System.Windows.Forms.Button();
            this.txtCampaign = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btMailingId = new System.Windows.Forms.Button();
            this.txtPartnerDescription = new System.Windows.Forms.TextBox();
            this.lblMailingId = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtPublic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grMailing = new System.Windows.Forms.GroupBox();
            this.btLoadFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btSelectFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grMailing.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(751, 354);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btRegisterDeals);
            this.tabPage1.Controls.Add(this.dealsSelector1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.grMailing);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(743, 328);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastro de campanha";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btRegisterDeals
            // 
            this.btRegisterDeals.Enabled = false;
            this.btRegisterDeals.Location = new System.Drawing.Point(628, 291);
            this.btRegisterDeals.Name = "btRegisterDeals";
            this.btRegisterDeals.Size = new System.Drawing.Size(101, 23);
            this.btRegisterDeals.TabIndex = 17;
            this.btRegisterDeals.Text = "Cadastrar ofertas";
            this.btRegisterDeals.UseVisualStyleBackColor = true;
            this.btRegisterDeals.Click += new System.EventHandler(this.OnUpdateOfertas);
            // 
            // dealsSelector1
            // 
            this.dealsSelector1.Date = new System.DateTime(((long)(0)));
            this.dealsSelector1.Enabled = false;
            this.dealsSelector1.Location = new System.Drawing.Point(351, 171);
            this.dealsSelector1.Name = "dealsSelector1";
            this.dealsSelector1.Size = new System.Drawing.Size(384, 150);
            this.dealsSelector1.TabIndex = 16;

            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDealsCount);
            this.groupBox1.Controls.Add(this.txtCountControl);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCountMailing);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.btClear);
            this.groupBox1.Controls.Add(this.txtPromocode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btCreateCampaign);
            this.groupBox1.Controls.Add(this.txtCampaign);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btMailingId);
            this.groupBox1.Controls.Add(this.txtPartnerDescription);
            this.groupBox1.Controls.Add(this.lblMailingId);
            this.groupBox1.Controls.Add(this.txtCategory);
            this.groupBox1.Controls.Add(this.txtPublic);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 159);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Campanha";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(540, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Qt Ofertas associadas";
            // 
            // txtDealsCount
            // 
            this.txtDealsCount.BackColor = System.Drawing.Color.White;
            this.txtDealsCount.Location = new System.Drawing.Point(653, 97);
            this.txtDealsCount.Name = "txtDealsCount";
            this.txtDealsCount.ReadOnly = true;
            this.txtDealsCount.Size = new System.Drawing.Size(68, 20);
            this.txtDealsCount.TabIndex = 21;
            // 
            // txtCountControl
            // 
            this.txtCountControl.BackColor = System.Drawing.Color.White;
            this.txtCountControl.Location = new System.Drawing.Point(653, 71);
            this.txtCountControl.Name = "txtCountControl";
            this.txtCountControl.ReadOnly = true;
            this.txtCountControl.Size = new System.Drawing.Size(68, 20);
            this.txtCountControl.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(540, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Qt Usuários controle";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Qt Usuários mailing";
            // 
            // txtCountMailing
            // 
            this.txtCountMailing.BackColor = System.Drawing.Color.White;
            this.txtCountMailing.Location = new System.Drawing.Point(443, 71);
            this.txtCountMailing.Name = "txtCountMailing";
            this.txtCountMailing.ReadOnly = true;
            this.txtCountMailing.Size = new System.Drawing.Size(68, 20);
            this.txtCountMailing.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Descrição";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(134, 120);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(312, 20);
            this.txtDescription.TabIndex = 15;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(471, 130);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(122, 23);
            this.btClear.TabIndex = 14;
            this.btClear.Text = "Limpar";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // txtPromocode
            // 
            this.txtPromocode.Location = new System.Drawing.Point(508, 45);
            this.txtPromocode.Name = "txtPromocode";
            this.txtPromocode.Size = new System.Drawing.Size(85, 20);
            this.txtPromocode.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(422, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Vale presente";
            // 
            // btCreateCampaign
            // 
            this.btCreateCampaign.Location = new System.Drawing.Point(599, 130);
            this.btCreateCampaign.Name = "btCreateCampaign";
            this.btCreateCampaign.Size = new System.Drawing.Size(122, 23);
            this.btCreateCampaign.TabIndex = 11;
            this.btCreateCampaign.Text = "Cadastrar campanha";
            this.btCreateCampaign.UseVisualStyleBackColor = true;
            this.btCreateCampaign.Click += new System.EventHandler(this.OnbtnCreateCampaignClick);
            // 
            // txtCampaign
            // 
            this.txtCampaign.Location = new System.Drawing.Point(134, 19);
            this.txtCampaign.Name = "txtCampaign";
            this.txtCampaign.Size = new System.Drawing.Size(171, 20);
            this.txtCampaign.TabIndex = 1;
            this.txtCampaign.Text = "CRMBRYYMMDDNNNN";
            this.txtCampaign.Leave += new System.EventHandler(this.OntxtCampaignLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome da campanha";
            // 
            // btMailingId
            // 
            this.btMailingId.Location = new System.Drawing.Point(412, 11);
            this.btMailingId.Name = "btMailingId";
            this.btMailingId.Size = new System.Drawing.Size(108, 23);
            this.btMailingId.TabIndex = 10;
            this.btMailingId.Text = "Buscar mailing id";
            this.btMailingId.UseVisualStyleBackColor = true;
            this.btMailingId.Click += new System.EventHandler(this.OnbtGetMailingIdClick);
            // 
            // txtPartnerDescription
            // 
            this.txtPartnerDescription.Location = new System.Drawing.Point(134, 45);
            this.txtPartnerDescription.Name = "txtPartnerDescription";
            this.txtPartnerDescription.Size = new System.Drawing.Size(282, 20);
            this.txtPartnerDescription.TabIndex = 2;
            // 
            // lblMailingId
            // 
            this.lblMailingId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMailingId.Location = new System.Drawing.Point(543, 11);
            this.lblMailingId.Name = "lblMailingId";
            this.lblMailingId.Size = new System.Drawing.Size(100, 23);
            this.lblMailingId.TabIndex = 8;
            this.lblMailingId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(134, 71);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(191, 20);
            this.txtCategory.TabIndex = 3;
            // 
            // txtPublic
            // 
            this.txtPublic.Location = new System.Drawing.Point(134, 94);
            this.txtPublic.Name = "txtPublic";
            this.txtPublic.Size = new System.Drawing.Size(378, 20);
            this.txtPublic.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Parceiro";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Público";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Categoria";
            // 
            // grMailing
            // 
            this.grMailing.Controls.Add(this.btLoadFile);
            this.grMailing.Controls.Add(this.txtFile);
            this.grMailing.Controls.Add(this.btSelectFile);
            this.grMailing.Enabled = false;
            this.grMailing.Location = new System.Drawing.Point(6, 200);
            this.grMailing.Name = "grMailing";
            this.grMailing.Size = new System.Drawing.Size(339, 120);
            this.grMailing.TabIndex = 14;
            this.grMailing.TabStop = false;
            this.grMailing.Text = "Mailing";
            // 
            // btLoadFile
            // 
            this.btLoadFile.Location = new System.Drawing.Point(6, 71);
            this.btLoadFile.Name = "btLoadFile";
            this.btLoadFile.Size = new System.Drawing.Size(116, 23);
            this.btLoadFile.TabIndex = 14;
            this.btLoadFile.Text = "Carregar";
            this.btLoadFile.UseVisualStyleBackColor = true;
            this.btLoadFile.Click += new System.EventHandler(this.OnBtLoadFileClick);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(6, 45);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(321, 20);
            this.txtFile.TabIndex = 13;
            // 
            // btSelectFile
            // 
            this.btSelectFile.Location = new System.Drawing.Point(6, 19);
            this.btSelectFile.Name = "btSelectFile";
            this.btSelectFile.Size = new System.Drawing.Size(116, 23);
            this.btSelectFile.TabIndex = 12;
            this.btSelectFile.Text = "Escolher arquivo";
            this.btSelectFile.UseVisualStyleBackColor = true;
            this.btSelectFile.Click += new System.EventHandler(this.btSelectFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 354);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Retorno de campanhas";
            this.Load += new System.EventHandler(this.OnLoad);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grMailing.ResumeLayout(false);
            this.grMailing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtCampaign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtPartnerDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPublic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMailingId;
        private System.Windows.Forms.Button btMailingId;
        private System.Windows.Forms.Button btSelectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox grMailing;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCreateCampaign;
        private System.Windows.Forms.Button btLoadFile;
        private System.Windows.Forms.TextBox txtPromocode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescription;
        private DealsSelector dealsSelector1;
        private System.Windows.Forms.Button btRegisterDeals;
        private System.Windows.Forms.TextBox txtCountControl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCountMailing;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDealsCount;
    }
}

