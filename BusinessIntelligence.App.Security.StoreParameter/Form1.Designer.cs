namespace BusinessIntelligence.App.Security.StoreParameter
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
            this.lbChave = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOriginalValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ckEncrypt = new System.Windows.Forms.CheckBox();
            this.btStore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbChave
            // 
            this.lbChave.AutoSize = true;
            this.lbChave.Location = new System.Drawing.Point(76, 126);
            this.lbChave.Name = "lbChave";
            this.lbChave.Size = new System.Drawing.Size(38, 13);
            this.lbChave.TabIndex = 11;
            this.lbChave.Text = "Chave";
            // 
            // txtKey
            // 
            this.txtKey.Enabled = false;
            this.txtKey.Location = new System.Drawing.Point(180, 119);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(272, 20);
            this.txtKey.TabIndex = 10;
            this.txtKey.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Valor";
            // 
            // txtOriginalValue
            // 
            this.txtOriginalValue.Location = new System.Drawing.Point(180, 51);
            this.txtOriginalValue.Name = "txtOriginalValue";
            this.txtOriginalValue.Size = new System.Drawing.Size(272, 20);
            this.txtOriginalValue.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nome";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(180, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(272, 20);
            this.txtName.TabIndex = 12;
            // 
            // ckEncrypt
            // 
            this.ckEncrypt.AutoSize = true;
            this.ckEncrypt.Location = new System.Drawing.Point(372, 86);
            this.ckEncrypt.Name = "ckEncrypt";
            this.ckEncrypt.Size = new System.Drawing.Size(80, 17);
            this.ckEncrypt.TabIndex = 14;
            this.ckEncrypt.Text = "Criptografar";
            this.ckEncrypt.UseVisualStyleBackColor = true;
            this.ckEncrypt.CheckedChanged += new System.EventHandler(this.ckEncrypt_CheckedChanged);
            // 
            // btStore
            // 
            this.btStore.Location = new System.Drawing.Point(372, 160);
            this.btStore.Name = "btStore";
            this.btStore.Size = new System.Drawing.Size(75, 23);
            this.btStore.TabIndex = 15;
            this.btStore.Text = "Armazenar";
            this.btStore.UseVisualStyleBackColor = true;
            this.btStore.Click += new System.EventHandler(this.btStore_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 199);
            this.Controls.Add(this.btStore);
            this.Controls.Add(this.ckEncrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbChave);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOriginalValue);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbChave;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOriginalValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox ckEncrypt;
        private System.Windows.Forms.Button btStore;
    }
}

