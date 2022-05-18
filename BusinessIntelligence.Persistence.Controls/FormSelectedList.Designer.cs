namespace BusinessIntelligence.Persistence.Controls
{
    partial class FormSelectedList
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
            this.cklItens = new System.Windows.Forms.CheckedListBox();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cklItens
            // 
            this.cklItens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cklItens.FormattingEnabled = true;
            this.cklItens.Location = new System.Drawing.Point(0, 0);
            this.cklItens.Name = "cklItens";
            this.cklItens.Size = new System.Drawing.Size(190, 244);
            this.cklItens.TabIndex = 0;
            this.cklItens.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cklItens_ItemCheck);
            this.cklItens.SelectedIndexChanged += new System.EventHandler(this.cklItens_SelectedIndexChanged);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(103, 262);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // FormSelectedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 297);
            this.ControlBox = false;
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.cklItens);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectedList";
            this.Text = "Itens";
            this.Load += new System.EventHandler(this.FormSelectedList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cklItens;
        private System.Windows.Forms.Button btOk;

    }
}