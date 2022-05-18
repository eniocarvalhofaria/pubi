namespace BusinessIntelligence
{
    partial class TextContent
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
            this.grSeeded = new System.Windows.Forms.GroupBox();
            this.btSeededFile = new System.Windows.Forms.Button();
            this.txtSeeded = new System.Windows.Forms.RichTextBox();
            this.openSeededList = new System.Windows.Forms.OpenFileDialog();
            this.grSeeded.SuspendLayout();
            this.SuspendLayout();
            // 
            // grSeeded
            // 
            this.grSeeded.Controls.Add(this.btSeededFile);
            this.grSeeded.Controls.Add(this.txtSeeded);
            this.grSeeded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grSeeded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grSeeded.Location = new System.Drawing.Point(0, 0);
            this.grSeeded.Name = "grSeeded";
            this.grSeeded.Size = new System.Drawing.Size(383, 605);
            this.grSeeded.TabIndex = 2;
            this.grSeeded.TabStop = false;
            this.grSeeded.Text = "Lista de semeados";
            // 
            // btSeededFile
            // 
            this.btSeededFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSeededFile.Location = new System.Drawing.Point(6, 19);
            this.btSeededFile.Name = "btSeededFile";
            this.btSeededFile.Size = new System.Drawing.Size(110, 23);
            this.btSeededFile.TabIndex = 1;
            this.btSeededFile.Text = "Buscar em arquivo";
            this.btSeededFile.UseVisualStyleBackColor = true;
            this.btSeededFile.Click += new System.EventHandler(this.btSeededFile_Click);
            // 
            // txtSeeded
            // 
            this.txtSeeded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeeded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeeded.Location = new System.Drawing.Point(3, 48);
            this.txtSeeded.Name = "txtSeeded";
            this.txtSeeded.Size = new System.Drawing.Size(377, 551);
            this.txtSeeded.TabIndex = 0;
            this.txtSeeded.Text = "";
            // 
            // openSeededList
            // 
            this.openSeededList.Filter = "Arquivos txt|*.txt";
            this.openSeededList.FileOk += new System.ComponentModel.CancelEventHandler(this.openSeededList_FileOk);
            // 
            // TextContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grSeeded);
            this.Name = "TextContent";
            this.Size = new System.Drawing.Size(383, 605);
            this.grSeeded.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grSeeded;
        private System.Windows.Forms.Button btSeededFile;
        private System.Windows.Forms.RichTextBox txtSeeded;
        private System.Windows.Forms.OpenFileDialog openSeededList;
    }
}
