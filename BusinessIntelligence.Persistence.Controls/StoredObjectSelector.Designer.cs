namespace BusinessIntelligence.Persistence.Controls
{
    partial class StoredObjectSelector
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
            this.btClearItens = new System.Windows.Forms.Button();
            this.btAddItem = new System.Windows.Forms.Button();
            this.combo = new System.Windows.Forms.ComboBox();
            this.lbLists = new System.Windows.Forms.Label();
            this.btManageItens = new System.Windows.Forms.Button();
            this.grSelector = new System.Windows.Forms.GroupBox();
            this.ckEnable = new System.Windows.Forms.CheckBox();
            this.rdIn = new System.Windows.Forms.RadioButton();
            this.rdNotIn = new System.Windows.Forms.RadioButton();
            this.grSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // btClearItens
            // 
            this.btClearItens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearItens.BackColor = System.Drawing.Color.Teal;
            this.btClearItens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearItens.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btClearItens.ForeColor = System.Drawing.Color.White;
            this.btClearItens.Location = new System.Drawing.Point(99, 67);
            this.btClearItens.Name = "btClearItens";
            this.btClearItens.Size = new System.Drawing.Size(70, 23);
            this.btClearItens.TabIndex = 21;
            this.btClearItens.Text = "Limpar";
            this.btClearItens.UseVisualStyleBackColor = false;
            this.btClearItens.Click += new System.EventHandler(this.btClearList_Click);
            // 
            // btAddItem
            // 
            this.btAddItem.BackColor = System.Drawing.Color.Teal;
            this.btAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAddItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAddItem.ForeColor = System.Drawing.Color.White;
            this.btAddItem.Location = new System.Drawing.Point(16, 67);
            this.btAddItem.Name = "btAddItem";
            this.btAddItem.Size = new System.Drawing.Size(70, 23);
            this.btAddItem.TabIndex = 20;
            this.btAddItem.Text = "Adicionar";
            this.btAddItem.UseVisualStyleBackColor = false;
            this.btAddItem.Click += new System.EventHandler(this.btAddList_Click);
            // 
            // combo
            // 
            this.combo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combo.Location = new System.Drawing.Point(16, 40);
            this.combo.Name = "combo";
            this.combo.Size = new System.Drawing.Size(236, 21);
            this.combo.TabIndex = 18;
            // 
            // lbLists
            // 
            this.lbLists.AutoSize = true;
            this.lbLists.Location = new System.Drawing.Point(13, 69);
            this.lbLists.Name = "lbLists";
            this.lbLists.Size = new System.Drawing.Size(0, 13);
            this.lbLists.TabIndex = 22;
            // 
            // btManageItens
            // 
            this.btManageItens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btManageItens.BackColor = System.Drawing.Color.Teal;
            this.btManageItens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btManageItens.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btManageItens.ForeColor = System.Drawing.Color.White;
            this.btManageItens.Location = new System.Drawing.Point(182, 67);
            this.btManageItens.Name = "btManageItens";
            this.btManageItens.Size = new System.Drawing.Size(70, 23);
            this.btManageItens.TabIndex = 23;
            this.btManageItens.Text = "Gerenciar";
            this.btManageItens.UseVisualStyleBackColor = false;
            this.btManageItens.Click += new System.EventHandler(this.btManageItens_Click);
            // 
            // grSelector
            // 
            this.grSelector.Controls.Add(this.rdNotIn);
            this.grSelector.Controls.Add(this.rdIn);
            this.grSelector.Controls.Add(this.combo);
            this.grSelector.Controls.Add(this.btManageItens);
            this.grSelector.Controls.Add(this.btAddItem);
            this.grSelector.Controls.Add(this.btClearItens);
            this.grSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grSelector.Enabled = false;
            this.grSelector.Location = new System.Drawing.Point(0, 0);
            this.grSelector.Name = "grSelector";
            this.grSelector.Size = new System.Drawing.Size(271, 97);
            this.grSelector.TabIndex = 24;
            this.grSelector.TabStop = false;
            // 
            // ckEnable
            // 
            this.ckEnable.AutoSize = true;
            this.ckEnable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckEnable.Location = new System.Drawing.Point(6, 0);
            this.ckEnable.Name = "ckEnable";
            this.ckEnable.Size = new System.Drawing.Size(15, 14);
            this.ckEnable.TabIndex = 24;
            this.ckEnable.UseVisualStyleBackColor = true;
            this.ckEnable.CheckedChanged += new System.EventHandler(this.ckEnable_CheckedChanged);
            // 
            // rdIn
            // 
            this.rdIn.AutoSize = true;
            this.rdIn.Checked = true;
            this.rdIn.Location = new System.Drawing.Point(16, 20);
            this.rdIn.Name = "rdIn";
            this.rdIn.Size = new System.Drawing.Size(101, 17);
            this.rdIn.TabIndex = 24;
            this.rdIn.Text = "Está na seleção";
            this.rdIn.UseVisualStyleBackColor = true;
            // 
            // rdNotIn
            // 
            this.rdNotIn.AutoSize = true;
            this.rdNotIn.Location = new System.Drawing.Point(129, 19);
            this.rdNotIn.Name = "rdNotIn";
            this.rdNotIn.Size = new System.Drawing.Size(123, 17);
            this.rdNotIn.TabIndex = 25;
            this.rdNotIn.Text = "Não está na seleção";
            this.rdNotIn.UseVisualStyleBackColor = true;
            // 
            // StoredObjectSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ckEnable);
            this.Controls.Add(this.grSelector);
            this.Controls.Add(this.lbLists);
            this.Name = "StoredObjectSelector";
            this.Size = new System.Drawing.Size(271, 97);
            this.Load += new System.EventHandler(this.StoredObjectSelector_Load);
            this.grSelector.ResumeLayout(false);
            this.grSelector.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClearItens;
        private System.Windows.Forms.Button btAddItem;
        private System.Windows.Forms.ComboBox combo;
     //   private BusinessIntelligence.Controls.CheckedComboBox combo;
        private System.Windows.Forms.Label lbLists;
        private System.Windows.Forms.Button btManageItens;
        private System.Windows.Forms.GroupBox grSelector;
        private System.Windows.Forms.CheckBox ckEnable;
        private System.Windows.Forms.RadioButton rdNotIn;
        private System.Windows.Forms.RadioButton rdIn;
    }
}
