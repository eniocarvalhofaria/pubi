namespace BusinessIntelligence.App.Marketing
{
    partial class DealsSelector
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
            this.grDeals = new System.Windows.Forms.GroupBox();
            this.grPage = new System.Windows.Forms.GroupBox();
            this.lbPages = new System.Windows.Forms.Label();
            this.btClearPage = new System.Windows.Forms.Button();
            this.btAddPage = new System.Windows.Forms.Button();
            this.cbPages = new System.Windows.Forms.ComboBox();
            this.btSortDeals = new System.Windows.Forms.Button();
            this.btClearDeals = new System.Windows.Forms.Button();
            this.lbOfertas = new System.Windows.Forms.Label();
            this.btDeals = new System.Windows.Forms.Button();
            this.grDeals.SuspendLayout();
            this.grPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // grDeals
            // 
            this.grDeals.Controls.Add(this.grPage);
            this.grDeals.Controls.Add(this.btSortDeals);
            this.grDeals.Controls.Add(this.btClearDeals);
            this.grDeals.Controls.Add(this.lbOfertas);
            this.grDeals.Controls.Add(this.btDeals);
            this.grDeals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grDeals.Location = new System.Drawing.Point(0, 0);
            this.grDeals.Name = "grDeals";
            this.grDeals.Size = new System.Drawing.Size(401, 150);
            this.grDeals.TabIndex = 17;
            this.grDeals.TabStop = false;
            this.grDeals.Text = "Seleção de Ofertas";
            // 
            // grPage
            // 
            this.grPage.Controls.Add(this.lbPages);
            this.grPage.Controls.Add(this.btClearPage);
            this.grPage.Controls.Add(this.btAddPage);
            this.grPage.Controls.Add(this.cbPages);
            this.grPage.Location = new System.Drawing.Point(6, 19);
            this.grPage.Name = "grPage";
            this.grPage.Size = new System.Drawing.Size(240, 123);
            this.grPage.TabIndex = 21;
            this.grPage.TabStop = false;
            this.grPage.Text = "Páginas";
            // 
            // lbPages
            // 
            this.lbPages.AutoSize = true;
            this.lbPages.Location = new System.Drawing.Point(21, 81);
            this.lbPages.Name = "lbPages";
            this.lbPages.Size = new System.Drawing.Size(0, 13);
            this.lbPages.TabIndex = 22;
            // 
            // btClearPage
            // 
            this.btClearPage.Location = new System.Drawing.Point(139, 46);
            this.btClearPage.Name = "btClearPage";
            this.btClearPage.Size = new System.Drawing.Size(95, 23);
            this.btClearPage.TabIndex = 21;
            this.btClearPage.Text = "Limpar páginas";
            this.btClearPage.UseVisualStyleBackColor = true;
            this.btClearPage.Click += new System.EventHandler(this.btClearPage_Click);
            // 
            // btAddPage
            // 
            this.btAddPage.Location = new System.Drawing.Point(21, 46);
            this.btAddPage.Name = "btAddPage";
            this.btAddPage.Size = new System.Drawing.Size(94, 23);
            this.btAddPage.TabIndex = 20;
            this.btAddPage.Text = "Adicionar página";
            this.btAddPage.UseVisualStyleBackColor = true;
            this.btAddPage.Click += new System.EventHandler(this.btAddPage_Click);
            // 
            // cbPages
            // 
            this.cbPages.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbPages.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPages.Location = new System.Drawing.Point(21, 19);
            this.cbPages.Name = "cbPages";
            this.cbPages.Size = new System.Drawing.Size(213, 21);
            this.cbPages.TabIndex = 18;
            // 
            // btSortDeals
            // 
            this.btSortDeals.Enabled = false;
            this.btSortDeals.Location = new System.Drawing.Point(277, 119);
            this.btSortDeals.Name = "btSortDeals";
            this.btSortDeals.Size = new System.Drawing.Size(101, 23);
            this.btSortDeals.TabIndex = 17;
            this.btSortDeals.Text = "Ordenar ofertas";
            this.btSortDeals.UseVisualStyleBackColor = true;
            this.btSortDeals.Click += new System.EventHandler(this.btSortDeals_Click);
            // 
            // btClearDeals
            // 
            this.btClearDeals.Enabled = false;
            this.btClearDeals.Location = new System.Drawing.Point(277, 91);
            this.btClearDeals.Name = "btClearDeals";
            this.btClearDeals.Size = new System.Drawing.Size(101, 23);
            this.btClearDeals.TabIndex = 16;
            this.btClearDeals.Text = "Limpar ofertas";
            this.btClearDeals.UseVisualStyleBackColor = true;
            this.btClearDeals.Click += new System.EventHandler(this.OnBtClearDealsClick);
            // 
            // lbOfertas
            // 
            this.lbOfertas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOfertas.Location = new System.Drawing.Point(265, 16);
            this.lbOfertas.Name = "lbOfertas";
            this.lbOfertas.Size = new System.Drawing.Size(119, 40);
            this.lbOfertas.TabIndex = 14;
            this.lbOfertas.Text = "0 oferta\r\nselecionada";
            this.lbOfertas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btDeals
            // 
            this.btDeals.Location = new System.Drawing.Point(277, 62);
            this.btDeals.Name = "btDeals";
            this.btDeals.Size = new System.Drawing.Size(101, 23);
            this.btDeals.TabIndex = 0;
            this.btDeals.Text = "Escolher ofertas";
            this.btDeals.UseVisualStyleBackColor = true;
            this.btDeals.Click += new System.EventHandler(this.OnBtOfertasClick);
            // 
            // DealsSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grDeals);
            this.Name = "DealsSelector";
            this.Size = new System.Drawing.Size(401, 150);
            this.Load += new System.EventHandler(this.OLoad);
            this.grDeals.ResumeLayout(false);
            this.grPage.ResumeLayout(false);
            this.grPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grDeals;
        private System.Windows.Forms.Button btSortDeals;
        private System.Windows.Forms.Button btClearDeals;
        private System.Windows.Forms.Label lbOfertas;
        private System.Windows.Forms.Button btDeals;
        private System.Windows.Forms.ComboBox cbPages;
        private System.Windows.Forms.Button btAddPage;
        private System.Windows.Forms.GroupBox grPage;
        private System.Windows.Forms.Label lbPages;
        private System.Windows.Forms.Button btClearPage;
    }
}
