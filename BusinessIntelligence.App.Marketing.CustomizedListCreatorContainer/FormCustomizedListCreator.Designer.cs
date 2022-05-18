using BusinessIntelligence.App.Marketing;
namespace BusinessIntelligence.App.Marketing
{
    partial class FormCustomizedListCreator
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
            this.customizedListCreator1 = new BusinessIntelligence.App.Marketing.CustomizedListCreator();
            this.SuspendLayout();
            // 
            // customizedListCreator1
            // 
            this.customizedListCreator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customizedListCreator1.Location = new System.Drawing.Point(0, 0);
            this.customizedListCreator1.Name = "customizedListCreator1";
            this.customizedListCreator1.Size = new System.Drawing.Size(374, 332);
            this.customizedListCreator1.TabIndex = 0;
            // 
            // FormCustomizedListCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 332);
            this.Controls.Add(this.customizedListCreator1);
            this.Name = "FormCustomizedListCreator";
            this.Text = "Criador de listas customizadas";
            this.ResumeLayout(false);

        }

        #endregion

        private BusinessIntelligence.App.Marketing.CustomizedListCreator customizedListCreator1;
    }
}

