namespace BusinessIntelligence.App.RedshiftLoader
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tb1 = new System.Windows.Forms.TabPage();
            this.rdDataSource = new System.Windows.Forms.RadioButton();
            this.rdExcelFile = new System.Windows.Forms.RadioButton();
            this.rdTextFileDelimited = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tb2 = new System.Windows.Forms.TabPage();
            this.ckHasColumnsNames = new System.Windows.Forms.CheckBox();
            this.pnDelimiterOptions = new System.Windows.Forms.Panel();
            this.Delimitador = new System.Windows.Forms.Label();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.btVerifyDelimiter = new System.Windows.Forms.Button();
            this.rdMoreColumns = new System.Windows.Forms.RadioButton();
            this.rd1Column = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tb3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSchemas = new System.Windows.Forms.ComboBox();
            this.tb4 = new System.Windows.Forms.TabPage();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevious = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tb1.SuspendLayout();
            this.tb2.SuspendLayout();
            this.pnDelimiterOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tb3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.btNext);
            this.splitContainer1.Panel2.Controls.Add(this.btPrevious);
            this.splitContainer1.Size = new System.Drawing.Size(911, 431);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tb1);
            this.tabControl1.Controls.Add(this.tb2);
            this.tabControl1.Controls.Add(this.tb3);
            this.tabControl1.Controls.Add(this.tb4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(911, 362);
            this.tabControl1.TabIndex = 0;
            // 
            // tb1
            // 
            this.tb1.Controls.Add(this.rdDataSource);
            this.tb1.Controls.Add(this.rdExcelFile);
            this.tb1.Controls.Add(this.rdTextFileDelimited);
            this.tb1.Controls.Add(this.label1);
            this.tb1.Location = new System.Drawing.Point(4, 22);
            this.tb1.Name = "tb1";
            this.tb1.Padding = new System.Windows.Forms.Padding(3);
            this.tb1.Size = new System.Drawing.Size(903, 336);
            this.tb1.TabIndex = 0;
            this.tb1.UseVisualStyleBackColor = true;
            // 
            // rdDataSource
            // 
            this.rdDataSource.AutoSize = true;
            this.rdDataSource.Enabled = false;
            this.rdDataSource.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDataSource.Location = new System.Drawing.Point(85, 136);
            this.rdDataSource.Name = "rdDataSource";
            this.rdDataSource.Size = new System.Drawing.Size(391, 25);
            this.rdDataSource.TabIndex = 3;
            this.rdDataSource.Text = "Transferir dados de um outro database para o Redshift";
            this.rdDataSource.UseVisualStyleBackColor = true;
            this.rdDataSource.CheckedChanged += new System.EventHandler(this.rdDataSource_CheckedChanged);
            // 
            // rdExcelFile
            // 
            this.rdExcelFile.AutoSize = true;
            this.rdExcelFile.Enabled = false;
            this.rdExcelFile.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdExcelFile.Location = new System.Drawing.Point(85, 105);
            this.rdExcelFile.Name = "rdExcelFile";
            this.rdExcelFile.Size = new System.Drawing.Size(341, 25);
            this.rdExcelFile.TabIndex = 2;
            this.rdExcelFile.Text = "Carregar dados a partir de um arquivo de excel";
            this.rdExcelFile.UseVisualStyleBackColor = true;
            // 
            // rdTextFileDelimited
            // 
            this.rdTextFileDelimited.AutoSize = true;
            this.rdTextFileDelimited.Checked = true;
            this.rdTextFileDelimited.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTextFileDelimited.Location = new System.Drawing.Point(85, 74);
            this.rdTextFileDelimited.Name = "rdTextFileDelimited";
            this.rdTextFileDelimited.Size = new System.Drawing.Size(395, 25);
            this.rdTextFileDelimited.TabIndex = 1;
            this.rdTextFileDelimited.TabStop = true;
            this.rdTextFileDelimited.Text = "Carregar dados a partir de um arquivo texto delimitado";
            this.rdTextFileDelimited.UseVisualStyleBackColor = true;
            this.rdTextFileDelimited.CheckedChanged += new System.EventHandler(this.rdTextFile_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "O que você gostaria de fazer";
            // 
            // tb2
            // 
            this.tb2.Controls.Add(this.ckHasColumnsNames);
            this.tb2.Controls.Add(this.pnDelimiterOptions);
            this.tb2.Controls.Add(this.rdMoreColumns);
            this.tb2.Controls.Add(this.rd1Column);
            this.tb2.Controls.Add(this.dataGridView1);
            this.tb2.Location = new System.Drawing.Point(4, 22);
            this.tb2.Name = "tb2";
            this.tb2.Padding = new System.Windows.Forms.Padding(3);
            this.tb2.Size = new System.Drawing.Size(903, 336);
            this.tb2.TabIndex = 1;
            this.tb2.UseVisualStyleBackColor = true;
            // 
            // ckHasColumnsNames
            // 
            this.ckHasColumnsNames.AutoSize = true;
            this.ckHasColumnsNames.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckHasColumnsNames.Location = new System.Drawing.Point(466, 9);
            this.ckHasColumnsNames.Name = "ckHasColumnsNames";
            this.ckHasColumnsNames.Size = new System.Drawing.Size(269, 25);
            this.ckHasColumnsNames.TabIndex = 8;
            this.ckHasColumnsNames.Text = "1a linha contém nomes dos campos";
            this.ckHasColumnsNames.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ckHasColumnsNames.UseVisualStyleBackColor = true;
            this.ckHasColumnsNames.CheckedChanged += new System.EventHandler(this.ckHasColumnsNames_CheckedChanged);
            // 
            // pnDelimiterOptions
            // 
            this.pnDelimiterOptions.Controls.Add(this.Delimitador);
            this.pnDelimiterOptions.Controls.Add(this.txtDelimiter);
            this.pnDelimiterOptions.Controls.Add(this.btVerifyDelimiter);
            this.pnDelimiterOptions.Location = new System.Drawing.Point(162, 6);
            this.pnDelimiterOptions.Name = "pnDelimiterOptions";
            this.pnDelimiterOptions.Size = new System.Drawing.Size(268, 65);
            this.pnDelimiterOptions.TabIndex = 6;
            // 
            // Delimitador
            // 
            this.Delimitador.AutoSize = true;
            this.Delimitador.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delimitador.Location = new System.Drawing.Point(17, 10);
            this.Delimitador.Name = "Delimitador";
            this.Delimitador.Size = new System.Drawing.Size(87, 21);
            this.Delimitador.TabIndex = 2;
            this.Delimitador.Text = "Delimitador";
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Location = new System.Drawing.Point(110, 11);
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size(23, 20);
            this.txtDelimiter.TabIndex = 0;
            this.txtDelimiter.TextChanged += new System.EventHandler(this.txtDelimiter_TextChanged);
            this.txtDelimiter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDelimiter_KeyDown);
            // 
            // btVerifyDelimiter
            // 
            this.btVerifyDelimiter.Location = new System.Drawing.Point(153, 10);
            this.btVerifyDelimiter.Name = "btVerifyDelimiter";
            this.btVerifyDelimiter.Size = new System.Drawing.Size(112, 23);
            this.btVerifyDelimiter.TabIndex = 1;
            this.btVerifyDelimiter.Text = "Verificar delimitador";
            this.btVerifyDelimiter.UseVisualStyleBackColor = true;
            this.btVerifyDelimiter.Click += new System.EventHandler(this.btVerifyDelimiter_Click);
            // 
            // rdMoreColumns
            // 
            this.rdMoreColumns.AutoSize = true;
            this.rdMoreColumns.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdMoreColumns.Location = new System.Drawing.Point(17, 40);
            this.rdMoreColumns.Name = "rdMoreColumns";
            this.rdMoreColumns.Size = new System.Drawing.Size(123, 25);
            this.rdMoreColumns.TabIndex = 5;
            this.rdMoreColumns.Text = "Várias colunas";
            this.rdMoreColumns.UseVisualStyleBackColor = true;
            this.rdMoreColumns.CheckedChanged += new System.EventHandler(this.rdMoreColumns_CheckedChanged);
            // 
            // rd1Column
            // 
            this.rd1Column.AutoSize = true;
            this.rd1Column.Checked = true;
            this.rd1Column.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1Column.Location = new System.Drawing.Point(17, 9);
            this.rd1Column.Name = "rd1Column";
            this.rd1Column.Size = new System.Drawing.Size(139, 25);
            this.rd1Column.TabIndex = 4;
            this.rd1Column.TabStop = true;
            this.rd1Column.Text = "Apenas 1 coluna";
            this.rd1Column.UseVisualStyleBackColor = true;
            this.rd1Column.CheckedChanged += new System.EventHandler(this.rd1Column_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(3, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(897, 222);
            this.dataGridView1.TabIndex = 3;
            // 
            // tb3
            // 
            this.tb3.Controls.Add(this.label3);
            this.tb3.Controls.Add(this.txtTableName);
            this.tb3.Controls.Add(this.label2);
            this.tb3.Controls.Add(this.cbSchemas);
            this.tb3.Location = new System.Drawing.Point(4, 22);
            this.tb3.Name = "tb3";
            this.tb3.Size = new System.Drawing.Size(903, 336);
            this.tb3.TabIndex = 2;
            this.tb3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(309, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Selecione a tabela";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(313, 53);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(376, 20);
            this.txtTableName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selecione o banco";
            // 
            // cbSchemas
            // 
            this.cbSchemas.FormattingEnabled = true;
            this.cbSchemas.Location = new System.Drawing.Point(62, 53);
            this.cbSchemas.Name = "cbSchemas";
            this.cbSchemas.Size = new System.Drawing.Size(202, 21);
            this.cbSchemas.TabIndex = 0;
            // 
            // tb4
            // 
            this.tb4.Location = new System.Drawing.Point(4, 22);
            this.tb4.Name = "tb4";
            this.tb4.Size = new System.Drawing.Size(903, 336);
            this.tb4.TabIndex = 3;
            this.tb4.UseVisualStyleBackColor = true;
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(645, 16);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(94, 23);
            this.btNext.TabIndex = 1;
            this.btNext.Text = "Próximo";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btPrevious
            // 
            this.btPrevious.Location = new System.Drawing.Point(530, 16);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(94, 23);
            this.btPrevious.TabIndex = 0;
            this.btPrevious.Text = "Anterior";
            this.btPrevious.UseVisualStyleBackColor = true;
            this.btPrevious.Visible = false;
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 431);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Redshift Loader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tb1.ResumeLayout(false);
            this.tb1.PerformLayout();
            this.tb2.ResumeLayout(false);
            this.tb2.PerformLayout();
            this.pnDelimiterOptions.ResumeLayout(false);
            this.pnDelimiterOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tb3.ResumeLayout(false);
            this.tb3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tb1;
        private System.Windows.Forms.RadioButton rdExcelFile;
        private System.Windows.Forms.RadioButton rdTextFileDelimited;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tb2;
        private System.Windows.Forms.RadioButton rdDataSource;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label Delimitador;
        private System.Windows.Forms.Button btVerifyDelimiter;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel pnDelimiterOptions;
        private System.Windows.Forms.RadioButton rdMoreColumns;
        private System.Windows.Forms.RadioButton rd1Column;
        private System.Windows.Forms.CheckBox ckHasColumnsNames;
        private System.Windows.Forms.TabPage tb3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSchemas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.TabPage tb4;
    }
}

