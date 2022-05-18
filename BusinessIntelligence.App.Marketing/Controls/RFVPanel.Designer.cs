namespace BusinessIntelligence.App.Marketing.Controls
{
    partial class RFVPanel
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
            this.grFrequency = new System.Windows.Forms.GroupBox();
            this.ckMoreQtyEvent = new System.Windows.Forms.CheckBox();
            this.rdFLast12Months = new System.Windows.Forms.RadioButton();
            this.rdFLast6Months = new System.Windows.Forms.RadioButton();
            this.rdFAllTime = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.QtyOfEvent = new BusinessIntelligence.Controls.Selector();
            this.grRecency = new System.Windows.Forms.GroupBox();
            this.ckLastEvent = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeOfEvent = new BusinessIntelligence.Controls.Selector();
            this.grInteractionType = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdPurchase = new System.Windows.Forms.RadioButton();
            this.rdOrder = new System.Windows.Forms.RadioButton();
            this.grValue = new System.Windows.Forms.GroupBox();
            this.rdVLast12Months = new System.Windows.Forms.RadioButton();
            this.rdVLast6Months = new System.Windows.Forms.RadioButton();
            this.rdVAllTime = new System.Windows.Forms.RadioButton();
            this.ckMoreValueEvent = new System.Windows.Forms.CheckBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.ValueEvents = new BusinessIntelligence.Controls.Selector();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdAdvanced = new System.Windows.Forms.RadioButton();
            this.rdBasic = new System.Windows.Forms.RadioButton();
            this.grFrequency.SuspendLayout();
            this.grRecency.SuspendLayout();
            this.grInteractionType.SuspendLayout();
            this.grValue.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grFrequency
            // 
            this.grFrequency.Controls.Add(this.ckMoreQtyEvent);
            this.grFrequency.Controls.Add(this.rdFLast12Months);
            this.grFrequency.Controls.Add(this.rdFLast6Months);
            this.grFrequency.Controls.Add(this.rdFAllTime);
            this.grFrequency.Controls.Add(this.label1);
            this.grFrequency.Controls.Add(this.QtyOfEvent);
            this.grFrequency.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grFrequency.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.grFrequency.Location = new System.Drawing.Point(0, 224);
            this.grFrequency.Name = "grFrequency";
            this.grFrequency.Size = new System.Drawing.Size(895, 110);
            this.grFrequency.TabIndex = 1;
            this.grFrequency.TabStop = false;
            // 
            // ckMoreQtyEvent
            // 
            this.ckMoreQtyEvent.AutoSize = true;
            this.ckMoreQtyEvent.Enabled = false;
            this.ckMoreQtyEvent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ckMoreQtyEvent.Location = new System.Drawing.Point(27, 43);
            this.ckMoreQtyEvent.Name = "ckMoreQtyEvent";
            this.ckMoreQtyEvent.Size = new System.Drawing.Size(196, 23);
            this.ckMoreQtyEvent.TabIndex = 7;
            this.ckMoreQtyEvent.Text = "Tem maior qtde de pedidos";
            this.ckMoreQtyEvent.UseVisualStyleBackColor = true;
            this.ckMoreQtyEvent.CheckedChanged += new System.EventHandler(this.ckMoreQtyEvent_CheckedChanged);
            // 
            // rdFLast12Months
            // 
            this.rdFLast12Months.AutoSize = true;
            this.rdFLast12Months.Enabled = false;
            this.rdFLast12Months.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdFLast12Months.Location = new System.Drawing.Point(674, 43);
            this.rdFLast12Months.Name = "rdFLast12Months";
            this.rdFLast12Months.Size = new System.Drawing.Size(136, 23);
            this.rdFLast12Months.TabIndex = 6;
            this.rdFLast12Months.Text = "Últimos 12 meses";
            this.rdFLast12Months.UseVisualStyleBackColor = true;
            // 
            // rdFLast6Months
            // 
            this.rdFLast6Months.AutoSize = true;
            this.rdFLast6Months.Enabled = false;
            this.rdFLast6Months.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdFLast6Months.Location = new System.Drawing.Point(523, 43);
            this.rdFLast6Months.Name = "rdFLast6Months";
            this.rdFLast6Months.Size = new System.Drawing.Size(128, 23);
            this.rdFLast6Months.TabIndex = 5;
            this.rdFLast6Months.Text = "Últimos 6 meses";
            this.rdFLast6Months.UseVisualStyleBackColor = true;
            // 
            // rdFAllTime
            // 
            this.rdFAllTime.AutoSize = true;
            this.rdFAllTime.Checked = true;
            this.rdFAllTime.Enabled = false;
            this.rdFAllTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdFAllTime.Location = new System.Drawing.Point(365, 43);
            this.rdFAllTime.Name = "rdFAllTime";
            this.rdFAllTime.Size = new System.Drawing.Size(121, 23);
            this.rdFAllTime.TabIndex = 4;
            this.rdFAllTime.TabStop = true;
            this.rdFAllTime.Text = "Todo o período";
            this.rdFAllTime.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frequência";
            // 
            // QtyOfEvent
            // 
            this.QtyOfEvent.Checked = false;
            this.QtyOfEvent.Criterial = null;
            this.QtyOfEvent.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.QtyOfEvent.FormSelector = null;
            this.QtyOfEvent.Location = new System.Drawing.Point(20, 25);
            this.QtyOfEvent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.QtyOfEvent.Name = "QtyOfEvent";
            this.QtyOfEvent.SelectionInfo = null;
            this.QtyOfEvent.Size = new System.Drawing.Size(279, 65);
            this.QtyOfEvent.TabIndex = 0;
            this.QtyOfEvent.Text = "Qtde de pedidos";
            this.QtyOfEvent.CheckedChange += new System.EventHandler(this.QtyOfEvent_CheckedChange);
            // 
            // grRecency
            // 
            this.grRecency.Controls.Add(this.ckLastEvent);
            this.grRecency.Controls.Add(this.label2);
            this.grRecency.Controls.Add(this.TimeOfEvent);
            this.grRecency.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grRecency.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.grRecency.Location = new System.Drawing.Point(0, 334);
            this.grRecency.Name = "grRecency";
            this.grRecency.Size = new System.Drawing.Size(895, 110);
            this.grRecency.TabIndex = 0;
            this.grRecency.TabStop = false;
            // 
            // ckLastEvent
            // 
            this.ckLastEvent.AutoSize = true;
            this.ckLastEvent.Enabled = false;
            this.ckLastEvent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ckLastEvent.Location = new System.Drawing.Point(27, 43);
            this.ckLastEvent.Name = "ckLastEvent";
            this.ckLastEvent.Size = new System.Drawing.Size(136, 23);
            this.ckLastEvent.TabIndex = 2;
            this.ckLastEvent.Text = "É o último pedido";
            this.ckLastEvent.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Recência";
            // 
            // TimeOfEvent
            // 
            this.TimeOfEvent.Checked = false;
            this.TimeOfEvent.Criterial = null;
            this.TimeOfEvent.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Date;
            this.TimeOfEvent.FormSelector = null;
            this.TimeOfEvent.Location = new System.Drawing.Point(20, 24);
            this.TimeOfEvent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TimeOfEvent.Name = "TimeOfEvent";
            this.TimeOfEvent.SelectionInfo = null;
            this.TimeOfEvent.Size = new System.Drawing.Size(279, 65);
            this.TimeOfEvent.TabIndex = 0;
            this.TimeOfEvent.Text = "Período do último pedido";
            this.TimeOfEvent.CheckedChange += new System.EventHandler(this.TimeOfEvent_CheckedChange);
            // 
            // grInteractionType
            // 
            this.grInteractionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grInteractionType.Controls.Add(this.label3);
            this.grInteractionType.Controls.Add(this.rdPurchase);
            this.grInteractionType.Controls.Add(this.rdOrder);
            this.grInteractionType.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grInteractionType.Location = new System.Drawing.Point(280, 3);
            this.grInteractionType.Name = "grInteractionType";
            this.grInteractionType.Size = new System.Drawing.Size(615, 110);
            this.grInteractionType.TabIndex = 3;
            this.grInteractionType.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo de interação";
            // 
            // rdPurchase
            // 
            this.rdPurchase.AutoSize = true;
            this.rdPurchase.Location = new System.Drawing.Point(338, 44);
            this.rdPurchase.Name = "rdPurchase";
            this.rdPurchase.Size = new System.Drawing.Size(136, 23);
            this.rdPurchase.TabIndex = 1;
            this.rdPurchase.Text = "Compra aprovada";
            this.rdPurchase.UseVisualStyleBackColor = true;
            this.rdPurchase.CheckedChanged += new System.EventHandler(this.rdCheckedChanged);
            // 
            // rdOrder
            // 
            this.rdOrder.AutoSize = true;
            this.rdOrder.Checked = true;
            this.rdOrder.Location = new System.Drawing.Point(43, 44);
            this.rdOrder.Name = "rdOrder";
            this.rdOrder.Size = new System.Drawing.Size(69, 23);
            this.rdOrder.TabIndex = 0;
            this.rdOrder.TabStop = true;
            this.rdOrder.Text = "Pedido";
            this.rdOrder.UseVisualStyleBackColor = true;
            this.rdOrder.CheckedChanged += new System.EventHandler(this.rdCheckedChanged);
            // 
            // grValue
            // 
            this.grValue.Controls.Add(this.rdVLast12Months);
            this.grValue.Controls.Add(this.rdVLast6Months);
            this.grValue.Controls.Add(this.rdVAllTime);
            this.grValue.Controls.Add(this.ckMoreValueEvent);
            this.grValue.Controls.Add(this.lbl1);
            this.grValue.Controls.Add(this.ValueEvents);
            this.grValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grValue.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.grValue.Location = new System.Drawing.Point(0, 114);
            this.grValue.Name = "grValue";
            this.grValue.Size = new System.Drawing.Size(895, 110);
            this.grValue.TabIndex = 2;
            this.grValue.TabStop = false;
            // 
            // rdVLast12Months
            // 
            this.rdVLast12Months.AutoSize = true;
            this.rdVLast12Months.Enabled = false;
            this.rdVLast12Months.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdVLast12Months.Location = new System.Drawing.Point(674, 43);
            this.rdVLast12Months.Name = "rdVLast12Months";
            this.rdVLast12Months.Size = new System.Drawing.Size(136, 23);
            this.rdVLast12Months.TabIndex = 11;
            this.rdVLast12Months.Text = "Últimos 12 meses";
            this.rdVLast12Months.UseVisualStyleBackColor = true;
            // 
            // rdVLast6Months
            // 
            this.rdVLast6Months.AutoSize = true;
            this.rdVLast6Months.Enabled = false;
            this.rdVLast6Months.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdVLast6Months.Location = new System.Drawing.Point(523, 43);
            this.rdVLast6Months.Name = "rdVLast6Months";
            this.rdVLast6Months.Size = new System.Drawing.Size(128, 23);
            this.rdVLast6Months.TabIndex = 10;
            this.rdVLast6Months.Text = "Últimos 6 meses";
            this.rdVLast6Months.UseVisualStyleBackColor = true;
            // 
            // rdVAllTime
            // 
            this.rdVAllTime.AutoSize = true;
            this.rdVAllTime.Checked = true;
            this.rdVAllTime.Enabled = false;
            this.rdVAllTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdVAllTime.Location = new System.Drawing.Point(365, 43);
            this.rdVAllTime.Name = "rdVAllTime";
            this.rdVAllTime.Size = new System.Drawing.Size(121, 23);
            this.rdVAllTime.TabIndex = 9;
            this.rdVAllTime.TabStop = true;
            this.rdVAllTime.Text = "Todo o período";
            this.rdVAllTime.UseVisualStyleBackColor = true;
            // 
            // ckMoreValueEvent
            // 
            this.ckMoreValueEvent.AutoSize = true;
            this.ckMoreValueEvent.Enabled = false;
            this.ckMoreValueEvent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ckMoreValueEvent.Location = new System.Drawing.Point(27, 43);
            this.ckMoreValueEvent.Name = "ckMoreValueEvent";
            this.ckMoreValueEvent.Size = new System.Drawing.Size(173, 23);
            this.ckMoreValueEvent.TabIndex = 8;
            this.ckMoreValueEvent.Text = "Tem maior valor pedido";
            this.ckMoreValueEvent.UseVisualStyleBackColor = true;
            this.ckMoreValueEvent.CheckedChanged += new System.EventHandler(this.ckMoreValueEvent_CheckedChanged);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(17, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(41, 17);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Valor";
            // 
            // ValueEvents
            // 
            this.ValueEvents.Checked = false;
            this.ValueEvents.Criterial = null;
            this.ValueEvents.DataTypeCriterial = BusinessIntelligence.DataTypeCriterial.Number;
            this.ValueEvents.FormSelector = null;
            this.ValueEvents.Location = new System.Drawing.Point(20, 25);
            this.ValueEvents.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ValueEvents.Name = "ValueEvents";
            this.ValueEvents.SelectionInfo = null;
            this.ValueEvents.Size = new System.Drawing.Size(279, 65);
            this.ValueEvents.TabIndex = 0;
            this.ValueEvents.Text = "Valor dos pedidos";
            this.ValueEvents.CheckedChange += new System.EventHandler(this.ValueEvents_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rdAdvanced);
            this.groupBox1.Controls.Add(this.rdBasic);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 110);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Modo de filtros";
            // 
            // rdAdvanced
            // 
            this.rdAdvanced.AutoSize = true;
            this.rdAdvanced.Location = new System.Drawing.Point(135, 45);
            this.rdAdvanced.Name = "rdAdvanced";
            this.rdAdvanced.Size = new System.Drawing.Size(85, 23);
            this.rdAdvanced.TabIndex = 1;
            this.rdAdvanced.Text = "Avançado";
            this.rdAdvanced.UseVisualStyleBackColor = true;
            this.rdAdvanced.CheckedChanged += new System.EventHandler(this.rdBasic_CheckedChanged);
            // 
            // rdBasic
            // 
            this.rdBasic.AutoSize = true;
            this.rdBasic.Checked = true;
            this.rdBasic.Location = new System.Drawing.Point(43, 44);
            this.rdBasic.Name = "rdBasic";
            this.rdBasic.Size = new System.Drawing.Size(64, 23);
            this.rdBasic.TabIndex = 0;
            this.rdBasic.TabStop = true;
            this.rdBasic.Text = "Básico";
            this.rdBasic.UseVisualStyleBackColor = true;
            this.rdBasic.CheckedChanged += new System.EventHandler(this.rdBasic_CheckedChanged);
            // 
            // RFVPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grValue);
            this.Controls.Add(this.grInteractionType);
            this.Controls.Add(this.grFrequency);
            this.Controls.Add(this.grRecency);
            this.Name = "RFVPanel";
            this.Size = new System.Drawing.Size(895, 444);
            this.Load += new System.EventHandler(this.RFVPanel_Load);
            this.grFrequency.ResumeLayout(false);
            this.grFrequency.PerformLayout();
            this.grRecency.ResumeLayout(false);
            this.grRecency.PerformLayout();
            this.grInteractionType.ResumeLayout(false);
            this.grInteractionType.PerformLayout();
            this.grValue.ResumeLayout(false);
            this.grValue.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grFrequency;
        private BusinessIntelligence.Controls.Selector QtyOfEvent;
        private System.Windows.Forms.GroupBox grRecency;
        private BusinessIntelligence.Controls.Selector TimeOfEvent;
        private System.Windows.Forms.GroupBox grInteractionType;
        private System.Windows.Forms.RadioButton rdPurchase;
        private System.Windows.Forms.RadioButton rdOrder;
        private System.Windows.Forms.GroupBox grValue;
        private BusinessIntelligence.Controls.Selector ValueEvents;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckLastEvent;
        private System.Windows.Forms.CheckBox ckMoreQtyEvent;
        private System.Windows.Forms.RadioButton rdFLast12Months;
        private System.Windows.Forms.RadioButton rdFLast6Months;
        private System.Windows.Forms.RadioButton rdFAllTime;
        private System.Windows.Forms.CheckBox ckMoreValueEvent;
        private System.Windows.Forms.RadioButton rdVLast12Months;
        private System.Windows.Forms.RadioButton rdVLast6Months;
        private System.Windows.Forms.RadioButton rdVAllTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdAdvanced;
        private System.Windows.Forms.RadioButton rdBasic;
    }
}
