namespace BusinessIntelligence.App.Marketing.CampaignMaker
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
            this.txtCampaign = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btBeginCreation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCampaign
            // 
            this.txtCampaign.Location = new System.Drawing.Point(121, 9);
            this.txtCampaign.Name = "txtCampaign";
            this.txtCampaign.Size = new System.Drawing.Size(171, 20);
            this.txtCampaign.TabIndex = 3;
            this.txtCampaign.Text = "CRMBRYYMMDDNNNN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome da campanha";
            // 
            // btBeginCreation
            // 
            this.btBeginCreation.Location = new System.Drawing.Point(121, 51);
            this.btBeginCreation.Name = "btBeginCreation";
            this.btBeginCreation.Size = new System.Drawing.Size(171, 23);
            this.btBeginCreation.TabIndex = 4;
            this.btBeginCreation.Text = "Iniciar criação";
            this.btBeginCreation.UseVisualStyleBackColor = true;
            this.btBeginCreation.Click += new System.EventHandler(this.btBeginCreation_Click);
            // 
            // FrmBegin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 98);
            this.Controls.Add(this.btBeginCreation);
            this.Controls.Add(this.txtCampaign);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBegin";
            this.Text = "Campaign Maker";
            this.Load += new System.EventHandler(this.FrmBegin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCampaign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btBeginCreation;
    }
}