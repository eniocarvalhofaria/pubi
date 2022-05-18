namespace BusinessIntelligence.App.Marketing.MailingSelectorContainer
{
    partial class FrmBegin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBegin));
            this.btBeginCreation = new System.Windows.Forms.Button();
            this.txtCampaign = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSlices = new System.Windows.Forms.Label();
            this.qtySlices = new System.Windows.Forms.NumericUpDown();
            this.btVerify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.qtySlices)).BeginInit();
            this.SuspendLayout();
            // 
            // btBeginCreation
            // 
            this.btBeginCreation.Enabled = false;
            this.btBeginCreation.Location = new System.Drawing.Point(276, 40);
            this.btBeginCreation.Name = "btBeginCreation";
            this.btBeginCreation.Size = new System.Drawing.Size(96, 23);
            this.btBeginCreation.TabIndex = 7;
            this.btBeginCreation.Text = "Iniciar criação";
            this.btBeginCreation.UseVisualStyleBackColor = true;
            this.btBeginCreation.Click += new System.EventHandler(this.btBeginCreation_Click);
            // 
            // txtCampaign
            // 
            this.txtCampaign.Location = new System.Drawing.Point(124, 12);
            this.txtCampaign.Name = "txtCampaign";
            this.txtCampaign.Size = new System.Drawing.Size(146, 20);
            this.txtCampaign.TabIndex = 6;
            this.txtCampaign.Text = "CRMBRYYMMDDNNNN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome da campanha";
            // 
            // lbSlices
            // 
            this.lbSlices.AutoSize = true;
            this.lbSlices.Enabled = false;
            this.lbSlices.Location = new System.Drawing.Point(15, 40);
            this.lbSlices.Name = "lbSlices";
            this.lbSlices.Size = new System.Drawing.Size(76, 13);
            this.lbSlices.TabIndex = 20;
            this.lbSlices.Text = "Fatias de teste";
            // 
            // qtySlices
            // 
            this.qtySlices.Enabled = false;
            this.qtySlices.Location = new System.Drawing.Point(124, 38);
            this.qtySlices.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.qtySlices.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.qtySlices.Name = "qtySlices";
            this.qtySlices.Size = new System.Drawing.Size(146, 20);
            this.qtySlices.TabIndex = 19;
            this.qtySlices.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btVerify
            // 
            this.btVerify.Location = new System.Drawing.Point(276, 7);
            this.btVerify.Name = "btVerify";
            this.btVerify.Size = new System.Drawing.Size(96, 23);
            this.btVerify.TabIndex = 21;
            this.btVerify.Text = "Verificar";
            this.btVerify.UseVisualStyleBackColor = true;
            this.btVerify.Click += new System.EventHandler(this.btVerify_Click);
            // 
            // FrmBegin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 70);
            this.Controls.Add(this.btVerify);
            this.Controls.Add(this.lbSlices);
            this.Controls.Add(this.qtySlices);
            this.Controls.Add(this.btBeginCreation);
            this.Controls.Add(this.txtCampaign);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBegin";
            this.Text = "Mailing Selector";
            this.Load += new System.EventHandler(this.FrmBegin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.qtySlices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btBeginCreation;
        private System.Windows.Forms.TextBox txtCampaign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSlices;
        private System.Windows.Forms.NumericUpDown qtySlices;
        private System.Windows.Forms.Button btVerify;
    }
}