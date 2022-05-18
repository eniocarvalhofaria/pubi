namespace BusinessIntelligence.App.Marketing
{
    partial class DealItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DealItem));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbName = new System.Windows.Forms.Label();
            this.imUp = new System.Windows.Forms.PictureBox();
            this.imDown = new System.Windows.Forms.PictureBox();
            this.imTop = new System.Windows.Forms.PictureBox();
            this.lbNeighborhood = new System.Windows.Forms.Label();
            this.imMarker = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imMarker)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(476, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lbName
            // 
            this.lbName.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.ForeColor = System.Drawing.Color.Gray;
            this.lbName.Location = new System.Drawing.Point(107, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(363, 47);
            this.lbName.TabIndex = 2;
            // 
            // imUp
            // 
            this.imUp.Image = ((System.Drawing.Image)(resources.GetObject("imUp.Image")));
            this.imUp.Location = new System.Drawing.Point(44, 53);
            this.imUp.Name = "imUp";
            this.imUp.Size = new System.Drawing.Size(35, 24);
            this.imUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imUp.TabIndex = 3;
            this.imUp.TabStop = false;
            this.imUp.Click += new System.EventHandler(this.imUp_Click);
            // 
            // imDown
            // 
            this.imDown.Image = ((System.Drawing.Image)(resources.GetObject("imDown.Image")));
            this.imDown.Location = new System.Drawing.Point(3, 53);
            this.imDown.Name = "imDown";
            this.imDown.Size = new System.Drawing.Size(35, 24);
            this.imDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imDown.TabIndex = 4;
            this.imDown.TabStop = false;
            this.imDown.Click += new System.EventHandler(this.imDown_Click);
            // 
            // imTop
            // 
            this.imTop.Image = ((System.Drawing.Image)(resources.GetObject("imTop.Image")));
            this.imTop.Location = new System.Drawing.Point(4, 0);
            this.imTop.Name = "imTop";
            this.imTop.Size = new System.Drawing.Size(76, 47);
            this.imTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imTop.TabIndex = 5;
            this.imTop.TabStop = false;
            this.imTop.Click += new System.EventHandler(this.imTop_Click);
            // 
            // lbNeighborhood
            // 
            this.lbNeighborhood.AutoSize = true;
            this.lbNeighborhood.ForeColor = System.Drawing.Color.Gray;
            this.lbNeighborhood.Location = new System.Drawing.Point(107, 53);
            this.lbNeighborhood.Name = "lbNeighborhood";
            this.lbNeighborhood.Size = new System.Drawing.Size(0, 13);
            this.lbNeighborhood.TabIndex = 6;
            // 
            // imMarker
            // 
            this.imMarker.Image = ((System.Drawing.Image)(resources.GetObject("imMarker.Image")));
            this.imMarker.Location = new System.Drawing.Point(85, 45);
            this.imMarker.Name = "imMarker";
            this.imMarker.Size = new System.Drawing.Size(24, 28);
            this.imMarker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imMarker.TabIndex = 7;
            this.imMarker.TabStop = false;
            // 
            // DealItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.imMarker);
            this.Controls.Add(this.lbNeighborhood);
            this.Controls.Add(this.imTop);
            this.Controls.Add(this.imDown);
            this.Controls.Add(this.imUp);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DealItem";
            this.Size = new System.Drawing.Size(600, 80);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imMarker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.PictureBox imUp;
        private System.Windows.Forms.PictureBox imDown;
        private System.Windows.Forms.PictureBox imTop;
        private System.Windows.Forms.Label lbNeighborhood;
        private System.Windows.Forms.PictureBox imMarker;
    }
}
