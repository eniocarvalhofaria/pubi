﻿namespace BusinessIntelligence.Controls
{
    partial class BigListSelector
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
            this.grSelector = new System.Windows.Forms.GroupBox();
            this.rdNo = new System.Windows.Forms.RadioButton();
            this.btSelector = new System.Windows.Forms.Button();
            this.rdYes = new System.Windows.Forms.RadioButton();
            this.lbValueSelected = new System.Windows.Forms.Label();
            this.ckEnable = new System.Windows.Forms.CheckBox();
            this.grSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // grSelector
            // 
            this.grSelector.BackColor = System.Drawing.Color.White;
            this.grSelector.Controls.Add(this.rdNo);
            this.grSelector.Controls.Add(this.btSelector);
            this.grSelector.Controls.Add(this.rdYes);
            this.grSelector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grSelector.Enabled = false;
            this.grSelector.Location = new System.Drawing.Point(0, 3);
            this.grSelector.Name = "grSelector";
            this.grSelector.Size = new System.Drawing.Size(299, 74);
            this.grSelector.TabIndex = 0;
            this.grSelector.TabStop = false;
            // 
            // rdNo
            // 
            this.rdNo.AutoSize = true;
            this.rdNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNo.Location = new System.Drawing.Point(176, 19);
            this.rdNo.Name = "rdNo";
            this.rdNo.Size = new System.Drawing.Size(47, 19);
            this.rdNo.TabIndex = 3;
            this.rdNo.TabStop = true;
            this.rdNo.Text = "Não";
            this.rdNo.UseVisualStyleBackColor = true;
            // 
            // btSelector
            // 
            this.btSelector.BackColor = System.Drawing.Color.Teal;
            this.btSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSelector.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSelector.ForeColor = System.Drawing.Color.White;
            this.btSelector.Location = new System.Drawing.Point(32, 25);
            this.btSelector.Name = "btSelector";
            this.btSelector.Size = new System.Drawing.Size(126, 23);
            this.btSelector.TabIndex = 1;
            this.btSelector.Text = "Selecionar filtro";
            this.btSelector.UseVisualStyleBackColor = false;
            this.btSelector.Click += new System.EventHandler(this.btSelector_Click);
            // 
            // rdYes
            // 
            this.rdYes.AutoSize = true;
            this.rdYes.Checked = true;
            this.rdYes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdYes.Location = new System.Drawing.Point(176, 42);
            this.rdYes.Name = "rdYes";
            this.rdYes.Size = new System.Drawing.Size(45, 19);
            this.rdYes.TabIndex = 2;
            this.rdYes.TabStop = true;
            this.rdYes.Text = "Sim";
            this.rdYes.UseVisualStyleBackColor = true;
            // 
            // lbValueSelected
            // 
            this.lbValueSelected.AutoSize = true;
            this.lbValueSelected.Location = new System.Drawing.Point(170, 13);
            this.lbValueSelected.Name = "lbValueSelected";
            this.lbValueSelected.Size = new System.Drawing.Size(0, 13);
            this.lbValueSelected.TabIndex = 0;
            // 
            // ckEnable
            // 
            this.ckEnable.AutoSize = true;
            this.ckEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckEnable.Location = new System.Drawing.Point(6, 5);
            this.ckEnable.Name = "ckEnable";
            this.ckEnable.Size = new System.Drawing.Size(15, 14);
            this.ckEnable.TabIndex = 5;
            this.ckEnable.UseVisualStyleBackColor = true;
            this.ckEnable.CheckedChanged += new System.EventHandler(this.ckEnable_CheckedChanged);
            // 
            // BigListSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckEnable);
            this.Controls.Add(this.grSelector);
            this.Name = "BigListSelector";
            this.Size = new System.Drawing.Size(299, 77);
            this.grSelector.ResumeLayout(false);
            this.grSelector.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grSelector;
        private System.Windows.Forms.CheckBox ckEnable;
        private System.Windows.Forms.Label lbValueSelected;
        private System.Windows.Forms.Button btSelector;
        private System.Windows.Forms.RadioButton rdNo;
        private System.Windows.Forms.RadioButton rdYes;
    }
}
