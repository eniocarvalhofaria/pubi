namespace BusinessIntelligence.App.Marketing
{
    partial class FormDeals
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mostrarTodasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostrarSelecionadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marcarTodasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desmarcarTodasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.pesquisarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btClose = new System.Windows.Forms.Button();
            this.btNextPage = new System.Windows.Forms.Button();
            this.btPreviousPage = new System.Windows.Forms.Button();
            this.btFirstPage = new System.Windows.Forms.Button();
            this.btLastPage = new System.Windows.Forms.Button();
            this.lbCurrentPage = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostrarTodasToolStripMenuItem,
            this.mostrarSelecionadasToolStripMenuItem,
            this.marcarTodasToolStripMenuItem,
            this.desmarcarTodasToolStripMenuItem,
            this.txtFind,
            this.pesquisarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 435);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(618, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mostrarTodasToolStripMenuItem
            // 
            this.mostrarTodasToolStripMenuItem.Name = "mostrarTodasToolStripMenuItem";
            this.mostrarTodasToolStripMenuItem.Size = new System.Drawing.Size(95, 23);
            this.mostrarTodasToolStripMenuItem.Text = "Mostrar Todas";
            this.mostrarTodasToolStripMenuItem.Click += new System.EventHandler(this.mostrarTodasToolStripMenuItem_Click);
            // 
            // mostrarSelecionadasToolStripMenuItem
            // 
            this.mostrarSelecionadasToolStripMenuItem.Name = "mostrarSelecionadasToolStripMenuItem";
            this.mostrarSelecionadasToolStripMenuItem.Size = new System.Drawing.Size(130, 23);
            this.mostrarSelecionadasToolStripMenuItem.Text = "Mostrar selecionadas";
            this.mostrarSelecionadasToolStripMenuItem.Click += new System.EventHandler(this.mostrarSelecionadasToolStripMenuItem_Click);
            // 
            // marcarTodasToolStripMenuItem
            // 
            this.marcarTodasToolStripMenuItem.Name = "marcarTodasToolStripMenuItem";
            this.marcarTodasToolStripMenuItem.Size = new System.Drawing.Size(88, 23);
            this.marcarTodasToolStripMenuItem.Text = "Marcar todas";
            this.marcarTodasToolStripMenuItem.Click += new System.EventHandler(this.marcarTodasToolStripMenuItem_Click);
            // 
            // desmarcarTodasToolStripMenuItem
            // 
            this.desmarcarTodasToolStripMenuItem.Name = "desmarcarTodasToolStripMenuItem";
            this.desmarcarTodasToolStripMenuItem.Size = new System.Drawing.Size(107, 23);
            this.desmarcarTodasToolStripMenuItem.Text = "Desmarcar todas";
            this.desmarcarTodasToolStripMenuItem.Click += new System.EventHandler(this.desmarcarTodasToolStripMenuItem_Click);
            // 
            // txtFind
            // 
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 23);
            // 
            // pesquisarToolStripMenuItem
            // 
            this.pesquisarToolStripMenuItem.Name = "pesquisarToolStripMenuItem";
            this.pesquisarToolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.pesquisarToolStripMenuItem.Text = "Pesquisar";
            this.pesquisarToolStripMenuItem.Click += new System.EventHandler(this.pesquisarToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 435);
            this.panel1.TabIndex = 0;
            this.panel1.Resize += new System.EventHandler(this.OnResize);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(531, 509);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Fechar";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.OnBtCloseClick);
            // 
            // btNextPage
            // 
            this.btNextPage.Location = new System.Drawing.Point(204, 509);
            this.btNextPage.Name = "btNextPage";
            this.btNextPage.Size = new System.Drawing.Size(90, 23);
            this.btNextPage.TabIndex = 2;
            this.btNextPage.Text = "Próxima página";
            this.btNextPage.UseVisualStyleBackColor = true;
            this.btNextPage.Click += new System.EventHandler(this.btNextPage_Click);
            // 
            // btPreviousPage
            // 
            this.btPreviousPage.Location = new System.Drawing.Point(108, 509);
            this.btPreviousPage.Name = "btPreviousPage";
            this.btPreviousPage.Size = new System.Drawing.Size(90, 23);
            this.btPreviousPage.TabIndex = 3;
            this.btPreviousPage.Text = "Página anterior";
            this.btPreviousPage.UseVisualStyleBackColor = true;
            this.btPreviousPage.Click += new System.EventHandler(this.btPreviousPage_Click);
            // 
            // btFirstPage
            // 
            this.btFirstPage.Location = new System.Drawing.Point(12, 509);
            this.btFirstPage.Name = "btFirstPage";
            this.btFirstPage.Size = new System.Drawing.Size(90, 23);
            this.btFirstPage.TabIndex = 4;
            this.btFirstPage.Text = "Primeira página";
            this.btFirstPage.UseVisualStyleBackColor = true;
            this.btFirstPage.Click += new System.EventHandler(this.btFirstPage_Click);
            // 
            // btLastPage
            // 
            this.btLastPage.Location = new System.Drawing.Point(300, 509);
            this.btLastPage.Name = "btLastPage";
            this.btLastPage.Size = new System.Drawing.Size(90, 23);
            this.btLastPage.TabIndex = 5;
            this.btLastPage.Text = "Última página";
            this.btLastPage.UseVisualStyleBackColor = true;
            this.btLastPage.Click += new System.EventHandler(this.btLastPage_Click);
            // 
            // lbCurrentPage
            // 
            this.lbCurrentPage.Location = new System.Drawing.Point(396, 514);
            this.lbCurrentPage.Name = "lbCurrentPage";
            this.lbCurrentPage.Size = new System.Drawing.Size(129, 18);
            this.lbCurrentPage.TabIndex = 6;
            this.lbCurrentPage.Text = "2021 a 2019 de 2029";
            this.lbCurrentPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormDeals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(618, 544);
            this.ControlBox = false;
            this.Controls.Add(this.lbCurrentPage);
            this.Controls.Add(this.btLastPage);
            this.Controls.Add(this.btFirstPage);
            this.Controls.Add(this.btPreviousPage);
            this.Controls.Add(this.btNextPage);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormDeals";
            this.Text = "Ofertas";
            this.Load += new System.EventHandler(this.OnLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mostrarTodasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marcarTodasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desmarcarTodasToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripMenuItem pesquisarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostrarSelecionadasToolStripMenuItem;
        private System.Windows.Forms.Button btNextPage;
        private System.Windows.Forms.Button btPreviousPage;
        private System.Windows.Forms.Button btFirstPage;
        private System.Windows.Forms.Button btLastPage;
        private System.Windows.Forms.Label lbCurrentPage;
    }
}