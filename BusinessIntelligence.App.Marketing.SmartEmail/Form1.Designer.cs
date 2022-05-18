namespace BusinessIntelligence.App.Marketing.SmartEmail
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lbSubject = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtHtml = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtSql = new System.Windows.Forms.RichTextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.destaqueToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.destaquesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.semDestaqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.destaqueToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.destaqueToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.destaquesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.semDestaqueToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Enabled = false;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel2.Controls.Add(this.menuStrip2);
            this.splitContainer2.Size = new System.Drawing.Size(981, 357);
            this.splitContainer2.SplitterDistance = 393;
            this.splitContainer2.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Enabled = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(393, 357);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 333);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Controls.Add(this.lbSubject);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pré visualização";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 31);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(570, 273);
            this.webBrowser1.TabIndex = 1;
            // 
            // lbSubject
            // 
            this.lbSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubject.Location = new System.Drawing.Point(3, 3);
            this.lbSubject.Name = "lbSubject";
            this.lbSubject.Size = new System.Drawing.Size(570, 28);
            this.lbSubject.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtHtml);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(576, 307);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Html";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtHtml
            // 
            this.txtHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHtml.Location = new System.Drawing.Point(3, 3);
            this.txtHtml.Name = "txtHtml";
            this.txtHtml.Size = new System.Drawing.Size(783, 301);
            this.txtHtml.TabIndex = 0;
            this.txtHtml.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtSql);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(576, 307);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sql Usuários Afetados";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtSql
            // 
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Location = new System.Drawing.Point(0, 0);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(576, 307);
            this.txtSql.TabIndex = 0;
            this.txtSql.Text = "";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.destaqueToolStripMenuItem1,
            this.destaquesToolStripMenuItem1,
            this.semDestaqueToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(584, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // destaqueToolStripMenuItem1
            // 
            this.destaqueToolStripMenuItem1.Name = "destaqueToolStripMenuItem1";
            this.destaqueToolStripMenuItem1.Size = new System.Drawing.Size(76, 20);
            this.destaqueToolStripMenuItem1.Text = "1 destaque";
            this.destaqueToolStripMenuItem1.Click += new System.EventHandler(this.destaqueToolStripMenuItem1_Click);
            // 
            // destaquesToolStripMenuItem1
            // 
            this.destaquesToolStripMenuItem1.Name = "destaquesToolStripMenuItem1";
            this.destaquesToolStripMenuItem1.Size = new System.Drawing.Size(81, 20);
            this.destaquesToolStripMenuItem1.Text = "2 destaques";
            this.destaquesToolStripMenuItem1.Click += new System.EventHandler(this.destaquesToolStripMenuItem1_Click);
            // 
            // semDestaqueToolStripMenuItem
            // 
            this.semDestaqueToolStripMenuItem.Name = "semDestaqueToolStripMenuItem";
            this.semDestaqueToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.semDestaqueToolStripMenuItem.Text = "Sem destaque";
            this.semDestaqueToolStripMenuItem.Click += new System.EventHandler(this.semDestaqueToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.layoutsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(981, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(64, 20);
            this.toolStripMenuItem1.Text = "Carregar";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // layoutsToolStripMenuItem
            // 
            this.layoutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.destaqueToolStripMenuItem2});
            this.layoutsToolStripMenuItem.Enabled = false;
            this.layoutsToolStripMenuItem.Name = "layoutsToolStripMenuItem";
            this.layoutsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.layoutsToolStripMenuItem.Text = "Layouts";
            // 
            // destaqueToolStripMenuItem2
            // 
            this.destaqueToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.destaqueToolStripMenuItem3,
            this.destaquesToolStripMenuItem3,
            this.semDestaqueToolStripMenuItem1});
            this.destaqueToolStripMenuItem2.Name = "destaqueToolStripMenuItem2";
            this.destaqueToolStripMenuItem2.Size = new System.Drawing.Size(123, 22);
            this.destaqueToolStripMenuItem2.Text = "Destaque";
            // 
            // destaqueToolStripMenuItem3
            // 
            this.destaqueToolStripMenuItem3.Name = "destaqueToolStripMenuItem3";
            this.destaqueToolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.destaqueToolStripMenuItem3.Text = "1 destaque";
            this.destaqueToolStripMenuItem3.Click += new System.EventHandler(this.destaqueToolStripMenuItem3_Click);
            // 
            // destaquesToolStripMenuItem3
            // 
            this.destaquesToolStripMenuItem3.Name = "destaquesToolStripMenuItem3";
            this.destaquesToolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.destaquesToolStripMenuItem3.Text = "2 destaques";
            this.destaquesToolStripMenuItem3.Click += new System.EventHandler(this.destaquesToolStripMenuItem3_Click);
            // 
            // semDestaqueToolStripMenuItem1
            // 
            this.semDestaqueToolStripMenuItem1.Name = "semDestaqueToolStripMenuItem1";
            this.semDestaqueToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.semDestaqueToolStripMenuItem1.Text = "Sem destaque";
            this.semDestaqueToolStripMenuItem1.Click += new System.EventHandler(this.semDestaqueToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 381);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form1";
            this.Text = "Smart Email";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox txtHtml;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem destaqueToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem destaquesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem semDestaqueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutsToolStripMenuItem;
        private System.Windows.Forms.Label lbSubject;
        private System.Windows.Forms.ToolStripMenuItem destaqueToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem destaqueToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem destaquesToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem semDestaqueToolStripMenuItem1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox txtSql;

    }
}

