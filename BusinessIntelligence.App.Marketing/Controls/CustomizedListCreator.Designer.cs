namespace BusinessIntelligence.App.Marketing
{
    partial class CustomizedListCreator
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbMain = new System.Windows.Forms.TabPage();
            this.btSelect = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btInsert = new System.Windows.Forms.Button();
            this.tbListType = new System.Windows.Forms.TabPage();
            this.btNext1 = new System.Windows.Forms.Button();
            this.rdSql = new System.Windows.Forms.RadioButton();
            this.rdFile = new System.Windows.Forms.RadioButton();
            this.tbSql = new System.Windows.Forms.TabPage();
            this.btReturn2 = new System.Windows.Forms.Button();
            this.btCreate = new System.Windows.Forms.Button();
            this.txtSql = new System.Windows.Forms.RichTextBox();
            this.tbFile = new System.Windows.Forms.TabPage();
            this.lbFileName = new System.Windows.Forms.Label();
            this.btSelectFile = new System.Windows.Forms.Button();
            this.tbSelect = new System.Windows.Forms.TabPage();
            this.btReturrn = new System.Windows.Forms.Button();
            this.btNext2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.btLoadFile = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.tbListType.SuspendLayout();
            this.tbSql.SuspendLayout();
            this.tbFile.SuspendLayout();
            this.tbSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(216, 20);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(32, 43);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(71, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Nome da lista";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbMain);
            this.tabControl1.Controls.Add(this.tbListType);
            this.tabControl1.Controls.Add(this.tbSql);
            this.tabControl1.Controls.Add(this.tbFile);
            this.tabControl1.Controls.Add(this.tbSelect);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 325);
            this.tabControl1.TabIndex = 2;
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.btSelect);
            this.tbMain.Controls.Add(this.btDelete);
            this.tbMain.Controls.Add(this.btInsert);
            this.tbMain.Location = new System.Drawing.Point(4, 22);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(356, 299);
            this.tbMain.TabIndex = 3;
            this.tbMain.Text = "Início";
            this.tbMain.UseVisualStyleBackColor = true;
            // 
            // btSelect
            // 
            this.btSelect.Location = new System.Drawing.Point(84, 47);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(188, 23);
            this.btSelect.TabIndex = 3;
            this.btSelect.Text = "Visualizar";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(84, 143);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(188, 23);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "Apagar";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btInsert
            // 
            this.btInsert.Location = new System.Drawing.Point(84, 94);
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(188, 23);
            this.btInsert.TabIndex = 0;
            this.btInsert.Text = "Criar";
            this.btInsert.UseVisualStyleBackColor = true;
            this.btInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // tbListType
            // 
            this.tbListType.Controls.Add(this.btNext1);
            this.tbListType.Controls.Add(this.rdSql);
            this.tbListType.Controls.Add(this.rdFile);
            this.tbListType.Controls.Add(this.txtName);
            this.tbListType.Controls.Add(this.lblName);
            this.tbListType.Location = new System.Drawing.Point(4, 22);
            this.tbListType.Name = "tbListType";
            this.tbListType.Padding = new System.Windows.Forms.Padding(3);
            this.tbListType.Size = new System.Drawing.Size(356, 299);
            this.tbListType.TabIndex = 0;
            this.tbListType.Text = "Tipo de Lista";
            this.tbListType.UseVisualStyleBackColor = true;
            // 
            // btNext1
            // 
            this.btNext1.Location = new System.Drawing.Point(275, 259);
            this.btNext1.Name = "btNext1";
            this.btNext1.Size = new System.Drawing.Size(75, 23);
            this.btNext1.TabIndex = 4;
            this.btNext1.Text = "Próximo";
            this.btNext1.UseVisualStyleBackColor = true;
            this.btNext1.Click += new System.EventHandler(this.btNext1_Click);
            // 
            // rdSql
            // 
            this.rdSql.AutoSize = true;
            this.rdSql.Checked = true;
            this.rdSql.Location = new System.Drawing.Point(35, 85);
            this.rdSql.Name = "rdSql";
            this.rdSql.Size = new System.Drawing.Size(93, 17);
            this.rdSql.TabIndex = 3;
            this.rdSql.TabStop = true;
            this.rdSql.Text = "Lista em query";
            this.rdSql.UseVisualStyleBackColor = true;
            // 
            // rdFile
            // 
            this.rdFile.AutoSize = true;
            this.rdFile.Location = new System.Drawing.Point(233, 85);
            this.rdFile.Name = "rdFile";
            this.rdFile.Size = new System.Drawing.Size(102, 17);
            this.rdFile.TabIndex = 2;
            this.rdFile.Text = "Lista em arquivo";
            this.rdFile.UseVisualStyleBackColor = true;
            // 
            // tbSql
            // 
            this.tbSql.Controls.Add(this.btReturn2);
            this.tbSql.Controls.Add(this.btCreate);
            this.tbSql.Controls.Add(this.txtSql);
            this.tbSql.Location = new System.Drawing.Point(4, 22);
            this.tbSql.Name = "tbSql";
            this.tbSql.Padding = new System.Windows.Forms.Padding(3);
            this.tbSql.Size = new System.Drawing.Size(356, 299);
            this.tbSql.TabIndex = 1;
            this.tbSql.Text = "Sql";
            this.tbSql.UseVisualStyleBackColor = true;
            // 
            // btReturn2
            // 
            this.btReturn2.Location = new System.Drawing.Point(194, 259);
            this.btReturn2.Name = "btReturn2";
            this.btReturn2.Size = new System.Drawing.Size(75, 23);
            this.btReturn2.TabIndex = 7;
            this.btReturn2.Text = "Voltar";
            this.btReturn2.UseVisualStyleBackColor = true;
            this.btReturn2.Visible = false;
            this.btReturn2.Click += new System.EventHandler(this.btReturn2_Click);
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(275, 259);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(75, 23);
            this.btCreate.TabIndex = 1;
            this.btCreate.Text = "Criar";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // txtSql
            // 
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSql.Location = new System.Drawing.Point(3, 3);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(350, 250);
            this.txtSql.TabIndex = 0;
            this.txtSql.Text = "";
            // 
            // tbFile
            // 
            this.tbFile.Controls.Add(this.button1);
            this.tbFile.Controls.Add(this.btLoadFile);
            this.tbFile.Controls.Add(this.lbFileName);
            this.tbFile.Controls.Add(this.btSelectFile);
            this.tbFile.Location = new System.Drawing.Point(4, 22);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(356, 299);
            this.tbFile.TabIndex = 2;
            this.tbFile.Text = "Arquivo";
            this.tbFile.UseVisualStyleBackColor = true;
            // 
            // lbFileName
            // 
            this.lbFileName.Location = new System.Drawing.Point(19, 29);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(317, 22);
            this.lbFileName.TabIndex = 1;
            // 
            // btSelectFile
            // 
            this.btSelectFile.Location = new System.Drawing.Point(214, 69);
            this.btSelectFile.Name = "btSelectFile";
            this.btSelectFile.Size = new System.Drawing.Size(122, 23);
            this.btSelectFile.TabIndex = 0;
            this.btSelectFile.Text = "Escolher Arquivo";
            this.btSelectFile.UseVisualStyleBackColor = true;
            this.btSelectFile.Click += new System.EventHandler(this.btSelectFile_Click);
            // 
            // tbSelect
            // 
            this.tbSelect.Controls.Add(this.btReturrn);
            this.tbSelect.Controls.Add(this.btNext2);
            this.tbSelect.Controls.Add(this.label1);
            this.tbSelect.Controls.Add(this.comboBox1);
            this.tbSelect.Location = new System.Drawing.Point(4, 22);
            this.tbSelect.Name = "tbSelect";
            this.tbSelect.Size = new System.Drawing.Size(356, 299);
            this.tbSelect.TabIndex = 4;
            this.tbSelect.Text = "Seleção";
            this.tbSelect.UseVisualStyleBackColor = true;
            // 
            // btReturrn
            // 
            this.btReturrn.Location = new System.Drawing.Point(194, 259);
            this.btReturrn.Name = "btReturrn";
            this.btReturrn.Size = new System.Drawing.Size(75, 23);
            this.btReturrn.TabIndex = 6;
            this.btReturrn.Text = "Voltar";
            this.btReturrn.UseVisualStyleBackColor = true;
            this.btReturrn.Click += new System.EventHandler(this.btReturrn_Click);
            // 
            // btNext2
            // 
            this.btNext2.Location = new System.Drawing.Point(275, 259);
            this.btNext2.Name = "btNext2";
            this.btNext2.Size = new System.Drawing.Size(75, 23);
            this.btNext2.TabIndex = 5;
            this.btNext2.Text = "Próximo";
            this.btNext2.UseVisualStyleBackColor = true;
            this.btNext2.Click += new System.EventHandler(this.btNext2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selectione a lista";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(22, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(314, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Voltar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btLoadFile
            // 
            this.btLoadFile.Location = new System.Drawing.Point(275, 259);
            this.btLoadFile.Name = "btLoadFile";
            this.btLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btLoadFile.TabIndex = 8;
            this.btLoadFile.Text = "Criar";
            this.btLoadFile.UseVisualStyleBackColor = true;
            this.btLoadFile.Click += new System.EventHandler(this.btLoadFile_Click);
            // 
            // CustomizedListCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "CustomizedListCreator";
            this.Size = new System.Drawing.Size(364, 325);
            this.Load += new System.EventHandler(this.CustomizedListCreator_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbMain.ResumeLayout(false);
            this.tbListType.ResumeLayout(false);
            this.tbListType.PerformLayout();
            this.tbSql.ResumeLayout(false);
            this.tbFile.ResumeLayout(false);
            this.tbSelect.ResumeLayout(false);
            this.tbSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbListType;
        private System.Windows.Forms.Button btNext1;
        private System.Windows.Forms.RadioButton rdSql;
        private System.Windows.Forms.RadioButton rdFile;
        private System.Windows.Forms.TabPage tbSql;
        private System.Windows.Forms.TabPage tbFile;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.RichTextBox txtSql;
        private System.Windows.Forms.TabPage tbMain;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btInsert;
        private System.Windows.Forms.TabPage tbSelect;
        private System.Windows.Forms.Button btNext2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.Button btSelectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btReturrn;
        private System.Windows.Forms.Button btReturn2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btLoadFile;
    }
}
