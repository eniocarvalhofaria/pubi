namespace BusinessIntelligence.Controls
{
    partial class FormDateSelector
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
            this.rdToday = new System.Windows.Forms.RadioButton();
            this.rdYesterday = new System.Windows.Forms.RadioButton();
            this.rdThisWeek = new System.Windows.Forms.RadioButton();
            this.rdThisMonth = new System.Windows.Forms.RadioButton();
            this.rdThisQuarter = new System.Windows.Forms.RadioButton();
            this.rdThisYear = new System.Windows.Forms.RadioButton();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.rd30Days = new System.Windows.Forms.RadioButton();
            this.rd60Days = new System.Windows.Forms.RadioButton();
            this.rd90Days = new System.Windows.Forms.RadioButton();
            this.rd120Days = new System.Windows.Forms.RadioButton();
            this.rd180Days = new System.Windows.Forms.RadioButton();
            this.rd365Days = new System.Windows.Forms.RadioButton();
            this.rdBetween = new System.Windows.Forms.RadioButton();
            this.lbStartDate = new System.Windows.Forms.Label();
            this.lbEndDate = new System.Windows.Forms.Label();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.rdPreviousWeek = new System.Windows.Forms.RadioButton();
            this.rdPreviousMonth = new System.Windows.Forms.RadioButton();
            this.rdPreviousQuarter = new System.Windows.Forms.RadioButton();
            this.rdPreviousYear = new System.Windows.Forms.RadioButton();
            this.btOk = new System.Windows.Forms.Button();
            this.rdNotBetween = new System.Windows.Forms.RadioButton();
            this.rd3160 = new System.Windows.Forms.RadioButton();
            this.rd6190 = new System.Windows.Forms.RadioButton();
            this.rdGT90Days = new System.Windows.Forms.RadioButton();
            this.grSpecificWeekDay = new System.Windows.Forms.GroupBox();
            this.ckSaturday = new System.Windows.Forms.CheckBox();
            this.ckFriday = new System.Windows.Forms.CheckBox();
            this.ckThursday = new System.Windows.Forms.CheckBox();
            this.ckWednesday = new System.Windows.Forms.CheckBox();
            this.ckTuesday = new System.Windows.Forms.CheckBox();
            this.ckMonday = new System.Windows.Forms.CheckBox();
            this.ckSunday = new System.Windows.Forms.CheckBox();
            this.ckSpecificWeekDay = new System.Windows.Forms.CheckBox();
            this.grSpecificTime = new System.Windows.Forms.GroupBox();
            this.tmEndSpecificTime = new System.Windows.Forms.DateTimePicker();
            this.tmStartSpecificTime = new System.Windows.Forms.DateTimePicker();
            this.ckSpecificTime = new System.Windows.Forms.CheckBox();
            this.grSpecificWeekDay.SuspendLayout();
            this.grSpecificTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdToday
            // 
            this.rdToday.AutoSize = true;
            this.rdToday.Checked = true;
            this.rdToday.Location = new System.Drawing.Point(12, 12);
            this.rdToday.Name = "rdToday";
            this.rdToday.Size = new System.Drawing.Size(47, 17);
            this.rdToday.TabIndex = 0;
            this.rdToday.TabStop = true;
            this.rdToday.Text = "Hoje";
            this.rdToday.UseVisualStyleBackColor = true;
            this.rdToday.CheckedChanged += new System.EventHandler(this.rdToday_CheckedChanged);
            // 
            // rdYesterday
            // 
            this.rdYesterday.AutoSize = true;
            this.rdYesterday.Location = new System.Drawing.Point(136, 12);
            this.rdYesterday.Name = "rdYesterday";
            this.rdYesterday.Size = new System.Drawing.Size(56, 17);
            this.rdYesterday.TabIndex = 1;
            this.rdYesterday.TabStop = true;
            this.rdYesterday.Text = "Ontem";
            this.rdYesterday.UseVisualStyleBackColor = true;
            this.rdYesterday.CheckedChanged += new System.EventHandler(this.rdYesterday_CheckedChanged);
            // 
            // rdThisWeek
            // 
            this.rdThisWeek.AutoSize = true;
            this.rdThisWeek.Location = new System.Drawing.Point(12, 36);
            this.rdThisWeek.Name = "rdThisWeek";
            this.rdThisWeek.Size = new System.Drawing.Size(86, 17);
            this.rdThisWeek.TabIndex = 2;
            this.rdThisWeek.TabStop = true;
            this.rdThisWeek.Text = "Esta semana";
            this.rdThisWeek.UseVisualStyleBackColor = true;
            // 
            // rdThisMonth
            // 
            this.rdThisMonth.AutoSize = true;
            this.rdThisMonth.Location = new System.Drawing.Point(12, 59);
            this.rdThisMonth.Name = "rdThisMonth";
            this.rdThisMonth.Size = new System.Drawing.Size(68, 17);
            this.rdThisMonth.TabIndex = 3;
            this.rdThisMonth.TabStop = true;
            this.rdThisMonth.Text = "Este mês";
            this.rdThisMonth.UseVisualStyleBackColor = true;
            // 
            // rdThisQuarter
            // 
            this.rdThisQuarter.AutoSize = true;
            this.rdThisQuarter.Location = new System.Drawing.Point(12, 82);
            this.rdThisQuarter.Name = "rdThisQuarter";
            this.rdThisQuarter.Size = new System.Drawing.Size(88, 17);
            this.rdThisQuarter.TabIndex = 4;
            this.rdThisQuarter.TabStop = true;
            this.rdThisQuarter.Text = "Este trimestre";
            this.rdThisQuarter.UseVisualStyleBackColor = true;
            // 
            // rdThisYear
            // 
            this.rdThisYear.AutoSize = true;
            this.rdThisYear.Location = new System.Drawing.Point(12, 105);
            this.rdThisYear.Name = "rdThisYear";
            this.rdThisYear.Size = new System.Drawing.Size(67, 17);
            this.rdThisYear.TabIndex = 5;
            this.rdThisYear.TabStop = true;
            this.rdThisYear.Text = "Este ano";
            this.rdThisYear.UseVisualStyleBackColor = true;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(437, 401);
            this.shapeContainer1.TabIndex = 6;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 9;
            this.lineShape2.X2 = 240;
            this.lineShape2.Y1 = 278;
            this.lineShape2.Y2 = 278;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 9;
            this.lineShape1.X2 = 240;
            this.lineShape1.Y1 = 136;
            this.lineShape1.Y2 = 136;
            // 
            // rd30Days
            // 
            this.rd30Days.AutoSize = true;
            this.rd30Days.Location = new System.Drawing.Point(12, 148);
            this.rd30Days.Name = "rd30Days";
            this.rd30Days.Size = new System.Drawing.Size(96, 17);
            this.rd30Days.TabIndex = 7;
            this.rd30Days.TabStop = true;
            this.rd30Days.Text = "Últimos 30 dias";
            this.rd30Days.UseVisualStyleBackColor = true;
            // 
            // rd60Days
            // 
            this.rd60Days.AutoSize = true;
            this.rd60Days.Location = new System.Drawing.Point(12, 171);
            this.rd60Days.Name = "rd60Days";
            this.rd60Days.Size = new System.Drawing.Size(96, 17);
            this.rd60Days.TabIndex = 8;
            this.rd60Days.TabStop = true;
            this.rd60Days.Text = "Últimos 60 dias";
            this.rd60Days.UseVisualStyleBackColor = true;
            // 
            // rd90Days
            // 
            this.rd90Days.AutoSize = true;
            this.rd90Days.Location = new System.Drawing.Point(12, 194);
            this.rd90Days.Name = "rd90Days";
            this.rd90Days.Size = new System.Drawing.Size(96, 17);
            this.rd90Days.TabIndex = 9;
            this.rd90Days.TabStop = true;
            this.rd90Days.Text = "Últimos 90 dias";
            this.rd90Days.UseVisualStyleBackColor = true;
            // 
            // rd120Days
            // 
            this.rd120Days.AutoSize = true;
            this.rd120Days.Location = new System.Drawing.Point(136, 148);
            this.rd120Days.Name = "rd120Days";
            this.rd120Days.Size = new System.Drawing.Size(102, 17);
            this.rd120Days.TabIndex = 10;
            this.rd120Days.TabStop = true;
            this.rd120Days.Text = "Últimos 120 dias";
            this.rd120Days.UseVisualStyleBackColor = true;
            // 
            // rd180Days
            // 
            this.rd180Days.AutoSize = true;
            this.rd180Days.Location = new System.Drawing.Point(136, 171);
            this.rd180Days.Name = "rd180Days";
            this.rd180Days.Size = new System.Drawing.Size(102, 17);
            this.rd180Days.TabIndex = 11;
            this.rd180Days.TabStop = true;
            this.rd180Days.Text = "Últimos 180 dias";
            this.rd180Days.UseVisualStyleBackColor = true;
            // 
            // rd365Days
            // 
            this.rd365Days.AutoSize = true;
            this.rd365Days.Location = new System.Drawing.Point(136, 194);
            this.rd365Days.Name = "rd365Days";
            this.rd365Days.Size = new System.Drawing.Size(102, 17);
            this.rd365Days.TabIndex = 12;
            this.rd365Days.TabStop = true;
            this.rd365Days.Text = "Últimos 365 dias";
            this.rd365Days.UseVisualStyleBackColor = true;
            // 
            // rdBetween
            // 
            this.rdBetween.AutoSize = true;
            this.rdBetween.Location = new System.Drawing.Point(12, 287);
            this.rdBetween.Name = "rdBetween";
            this.rdBetween.Size = new System.Drawing.Size(73, 17);
            this.rdBetween.TabIndex = 13;
            this.rdBetween.TabStop = true;
            this.rdBetween.Text = "Está entre";
            this.rdBetween.UseVisualStyleBackColor = true;
            this.rdBetween.CheckedChanged += new System.EventHandler(this.rdCustom_CheckedChanged);
            // 
            // lbStartDate
            // 
            this.lbStartDate.AutoSize = true;
            this.lbStartDate.Enabled = false;
            this.lbStartDate.Location = new System.Drawing.Point(12, 319);
            this.lbStartDate.Name = "lbStartDate";
            this.lbStartDate.Size = new System.Drawing.Size(60, 13);
            this.lbStartDate.TabIndex = 16;
            this.lbStartDate.Text = "Data Inicial";
            // 
            // lbEndDate
            // 
            this.lbEndDate.AutoSize = true;
            this.lbEndDate.Enabled = false;
            this.lbEndDate.Location = new System.Drawing.Point(133, 319);
            this.lbEndDate.Name = "lbEndDate";
            this.lbEndDate.Size = new System.Drawing.Size(55, 13);
            this.lbEndDate.TabIndex = 17;
            this.lbEndDate.Text = "Data Final";
            // 
            // dtStartDate
            // 
            this.dtStartDate.Enabled = false;
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStartDate.Location = new System.Drawing.Point(12, 335);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(100, 20);
            this.dtStartDate.TabIndex = 18;
            // 
            // dtEndDate
            // 
            this.dtEndDate.Enabled = false;
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtEndDate.Location = new System.Drawing.Point(136, 335);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(100, 20);
            this.dtEndDate.TabIndex = 19;
            // 
            // rdPreviousWeek
            // 
            this.rdPreviousWeek.AutoSize = true;
            this.rdPreviousWeek.Location = new System.Drawing.Point(136, 35);
            this.rdPreviousWeek.Name = "rdPreviousWeek";
            this.rdPreviousWeek.Size = new System.Drawing.Size(107, 17);
            this.rdPreviousWeek.TabIndex = 20;
            this.rdPreviousWeek.TabStop = true;
            this.rdPreviousWeek.Text = "Semana passada";
            this.rdPreviousWeek.UseVisualStyleBackColor = true;
            // 
            // rdPreviousMonth
            // 
            this.rdPreviousMonth.AutoSize = true;
            this.rdPreviousMonth.Location = new System.Drawing.Point(136, 59);
            this.rdPreviousMonth.Name = "rdPreviousMonth";
            this.rdPreviousMonth.Size = new System.Drawing.Size(88, 17);
            this.rdPreviousMonth.TabIndex = 21;
            this.rdPreviousMonth.TabStop = true;
            this.rdPreviousMonth.Text = "Mês passado";
            this.rdPreviousMonth.UseVisualStyleBackColor = true;
            // 
            // rdPreviousQuarter
            // 
            this.rdPreviousQuarter.AutoSize = true;
            this.rdPreviousQuarter.Location = new System.Drawing.Point(136, 82);
            this.rdPreviousQuarter.Name = "rdPreviousQuarter";
            this.rdPreviousQuarter.Size = new System.Drawing.Size(111, 17);
            this.rdPreviousQuarter.TabIndex = 22;
            this.rdPreviousQuarter.TabStop = true;
            this.rdPreviousQuarter.Text = "Trimestre passado";
            this.rdPreviousQuarter.UseVisualStyleBackColor = true;
            // 
            // rdPreviousYear
            // 
            this.rdPreviousYear.AutoSize = true;
            this.rdPreviousYear.Location = new System.Drawing.Point(136, 105);
            this.rdPreviousYear.Name = "rdPreviousYear";
            this.rdPreviousYear.Size = new System.Drawing.Size(87, 17);
            this.rdPreviousYear.TabIndex = 23;
            this.rdPreviousYear.TabStop = true;
            this.rdPreviousYear.Text = "Ano passado";
            this.rdPreviousYear.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(299, 366);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 24;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // rdNotBetween
            // 
            this.rdNotBetween.AutoSize = true;
            this.rdNotBetween.Location = new System.Drawing.Point(136, 287);
            this.rdNotBetween.Name = "rdNotBetween";
            this.rdNotBetween.Size = new System.Drawing.Size(95, 17);
            this.rdNotBetween.TabIndex = 25;
            this.rdNotBetween.TabStop = true;
            this.rdNotBetween.Text = "Não está entre";
            this.rdNotBetween.UseVisualStyleBackColor = true;
            this.rdNotBetween.CheckedChanged += new System.EventHandler(this.rdCustom_CheckedChanged);
            // 
            // rd3160
            // 
            this.rd3160.AutoSize = true;
            this.rd3160.Location = new System.Drawing.Point(12, 217);
            this.rd3160.Name = "rd3160";
            this.rd3160.Size = new System.Drawing.Size(111, 17);
            this.rd3160.TabIndex = 26;
            this.rd3160.TabStop = true;
            this.rd3160.Text = "Entre 31 e 60 dias";
            this.rd3160.UseVisualStyleBackColor = true;
            // 
            // rd6190
            // 
            this.rd6190.AutoSize = true;
            this.rd6190.Location = new System.Drawing.Point(136, 217);
            this.rd6190.Name = "rd6190";
            this.rd6190.Size = new System.Drawing.Size(111, 17);
            this.rd6190.TabIndex = 27;
            this.rd6190.TabStop = true;
            this.rd6190.Text = "Entre 61 e 90 dias";
            this.rd6190.UseVisualStyleBackColor = true;
            // 
            // rdGT90Days
            // 
            this.rdGT90Days.AutoSize = true;
            this.rdGT90Days.Location = new System.Drawing.Point(12, 240);
            this.rdGT90Days.Name = "rdGT90Days";
            this.rdGT90Days.Size = new System.Drawing.Size(99, 17);
            this.rdGT90Days.TabIndex = 28;
            this.rdGT90Days.TabStop = true;
            this.rdGT90Days.Text = "Mais de 90 dias";
            this.rdGT90Days.UseVisualStyleBackColor = true;
            // 
            // grSpecificWeekDay
            // 
            this.grSpecificWeekDay.Controls.Add(this.ckSaturday);
            this.grSpecificWeekDay.Controls.Add(this.ckFriday);
            this.grSpecificWeekDay.Controls.Add(this.ckThursday);
            this.grSpecificWeekDay.Controls.Add(this.ckWednesday);
            this.grSpecificWeekDay.Controls.Add(this.ckTuesday);
            this.grSpecificWeekDay.Controls.Add(this.ckMonday);
            this.grSpecificWeekDay.Controls.Add(this.ckSunday);
            this.grSpecificWeekDay.Enabled = false;
            this.grSpecificWeekDay.Location = new System.Drawing.Point(253, 12);
            this.grSpecificWeekDay.Name = "grSpecificWeekDay";
            this.grSpecificWeekDay.Size = new System.Drawing.Size(172, 199);
            this.grSpecificWeekDay.TabIndex = 29;
            this.grSpecificWeekDay.TabStop = false;
            // 
            // ckSaturday
            // 
            this.ckSaturday.AutoSize = true;
            this.ckSaturday.Location = new System.Drawing.Point(14, 163);
            this.ckSaturday.Name = "ckSaturday";
            this.ckSaturday.Size = new System.Drawing.Size(63, 17);
            this.ckSaturday.TabIndex = 6;
            this.ckSaturday.Text = "Sábado";
            this.ckSaturday.UseVisualStyleBackColor = true;
            // 
            // ckFriday
            // 
            this.ckFriday.AutoSize = true;
            this.ckFriday.Location = new System.Drawing.Point(14, 140);
            this.ckFriday.Name = "ckFriday";
            this.ckFriday.Size = new System.Drawing.Size(76, 17);
            this.ckFriday.TabIndex = 5;
            this.ckFriday.Text = "Sexta-feira";
            this.ckFriday.UseVisualStyleBackColor = true;
            // 
            // ckThursday
            // 
            this.ckThursday.AutoSize = true;
            this.ckThursday.Location = new System.Drawing.Point(14, 117);
            this.ckThursday.Name = "ckThursday";
            this.ckThursday.Size = new System.Drawing.Size(80, 17);
            this.ckThursday.TabIndex = 4;
            this.ckThursday.Text = "Quinta-feira";
            this.ckThursday.UseVisualStyleBackColor = true;
            // 
            // ckWednesday
            // 
            this.ckWednesday.AutoSize = true;
            this.ckWednesday.Location = new System.Drawing.Point(14, 94);
            this.ckWednesday.Name = "ckWednesday";
            this.ckWednesday.Size = new System.Drawing.Size(81, 17);
            this.ckWednesday.TabIndex = 3;
            this.ckWednesday.Text = "Quarta-feira";
            this.ckWednesday.UseVisualStyleBackColor = true;
            // 
            // ckTuesday
            // 
            this.ckTuesday.AutoSize = true;
            this.ckTuesday.Location = new System.Drawing.Point(14, 71);
            this.ckTuesday.Name = "ckTuesday";
            this.ckTuesday.Size = new System.Drawing.Size(77, 17);
            this.ckTuesday.TabIndex = 2;
            this.ckTuesday.Text = "Terça-feira";
            this.ckTuesday.UseVisualStyleBackColor = true;
            // 
            // ckMonday
            // 
            this.ckMonday.AutoSize = true;
            this.ckMonday.Location = new System.Drawing.Point(14, 48);
            this.ckMonday.Name = "ckMonday";
            this.ckMonday.Size = new System.Drawing.Size(92, 17);
            this.ckMonday.TabIndex = 1;
            this.ckMonday.Text = "Segunda-feira";
            this.ckMonday.UseVisualStyleBackColor = true;
            // 
            // ckSunday
            // 
            this.ckSunday.AutoSize = true;
            this.ckSunday.Location = new System.Drawing.Point(14, 24);
            this.ckSunday.Name = "ckSunday";
            this.ckSunday.Size = new System.Drawing.Size(68, 17);
            this.ckSunday.TabIndex = 0;
            this.ckSunday.Text = "Domingo";
            this.ckSunday.UseVisualStyleBackColor = true;
            // 
            // ckSpecificWeekDay
            // 
            this.ckSpecificWeekDay.AutoSize = true;
            this.ckSpecificWeekDay.Enabled = false;
            this.ckSpecificWeekDay.Location = new System.Drawing.Point(260, 10);
            this.ckSpecificWeekDay.Name = "ckSpecificWeekDay";
            this.ckSpecificWeekDay.Size = new System.Drawing.Size(150, 17);
            this.ckSpecificWeekDay.TabIndex = 7;
            this.ckSpecificWeekDay.Text = "Dia da semana específico";
            this.ckSpecificWeekDay.UseVisualStyleBackColor = true;
            this.ckSpecificWeekDay.CheckedChanged += new System.EventHandler(this.ckSpecificTime_CheckedChanged);
            // 
            // grSpecificTime
            // 
            this.grSpecificTime.Controls.Add(this.tmEndSpecificTime);
            this.grSpecificTime.Controls.Add(this.tmStartSpecificTime);
            this.grSpecificTime.Enabled = false;
            this.grSpecificTime.Location = new System.Drawing.Point(253, 220);
            this.grSpecificTime.Name = "grSpecificTime";
            this.grSpecificTime.Size = new System.Drawing.Size(172, 135);
            this.grSpecificTime.TabIndex = 30;
            this.grSpecificTime.TabStop = false;
            // 
            // tmEndSpecificTime
            // 
            this.tmEndSpecificTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmEndSpecificTime.Location = new System.Drawing.Point(62, 99);
            this.tmEndSpecificTime.Name = "tmEndSpecificTime";
            this.tmEndSpecificTime.Size = new System.Drawing.Size(94, 20);
            this.tmEndSpecificTime.TabIndex = 9;
            // 
            // tmStartSpecificTime
            // 
            this.tmStartSpecificTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmStartSpecificTime.Location = new System.Drawing.Point(62, 23);
            this.tmStartSpecificTime.Name = "tmStartSpecificTime";
            this.tmStartSpecificTime.Size = new System.Drawing.Size(94, 20);
            this.tmStartSpecificTime.TabIndex = 8;
            // 
            // ckSpecificTime
            // 
            this.ckSpecificTime.AutoSize = true;
            this.ckSpecificTime.Location = new System.Drawing.Point(260, 220);
            this.ckSpecificTime.Name = "ckSpecificTime";
            this.ckSpecificTime.Size = new System.Drawing.Size(102, 17);
            this.ckSpecificTime.TabIndex = 7;
            this.ckSpecificTime.Text = "Hora específica";
            this.ckSpecificTime.UseVisualStyleBackColor = true;
            this.ckSpecificTime.CheckedChanged += new System.EventHandler(this.ckSpecificTime_CheckedChanged_1);
            // 
            // FormDateSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 401);
            this.ControlBox = false;
            this.Controls.Add(this.ckSpecificWeekDay);
            this.Controls.Add(this.ckSpecificTime);
            this.Controls.Add(this.grSpecificTime);
            this.Controls.Add(this.grSpecificWeekDay);
            this.Controls.Add(this.rdGT90Days);
            this.Controls.Add(this.rd6190);
            this.Controls.Add(this.rd3160);
            this.Controls.Add(this.rdNotBetween);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.rdPreviousYear);
            this.Controls.Add(this.rdPreviousQuarter);
            this.Controls.Add(this.rdPreviousMonth);
            this.Controls.Add(this.rdPreviousWeek);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(this.lbEndDate);
            this.Controls.Add(this.lbStartDate);
            this.Controls.Add(this.rdBetween);
            this.Controls.Add(this.rd365Days);
            this.Controls.Add(this.rd180Days);
            this.Controls.Add(this.rd120Days);
            this.Controls.Add(this.rd90Days);
            this.Controls.Add(this.rd60Days);
            this.Controls.Add(this.rd30Days);
            this.Controls.Add(this.rdThisYear);
            this.Controls.Add(this.rdThisQuarter);
            this.Controls.Add(this.rdThisMonth);
            this.Controls.Add(this.rdThisWeek);
            this.Controls.Add(this.rdYesterday);
            this.Controls.Add(this.rdToday);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "FormDateSelector";
            this.Text = "Selecionador de período";
            this.grSpecificWeekDay.ResumeLayout(false);
            this.grSpecificWeekDay.PerformLayout();
            this.grSpecificTime.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdToday;
        private System.Windows.Forms.RadioButton rdYesterday;
        private System.Windows.Forms.RadioButton rdThisWeek;
        private System.Windows.Forms.RadioButton rdThisMonth;
        private System.Windows.Forms.RadioButton rdThisQuarter;
        private System.Windows.Forms.RadioButton rdThisYear;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.RadioButton rd30Days;
        private System.Windows.Forms.RadioButton rd60Days;
        private System.Windows.Forms.RadioButton rd90Days;
        private System.Windows.Forms.RadioButton rd120Days;
        private System.Windows.Forms.RadioButton rd180Days;
        private System.Windows.Forms.RadioButton rd365Days;
        private System.Windows.Forms.RadioButton rdBetween;
        private System.Windows.Forms.Label lbStartDate;
        private System.Windows.Forms.Label lbEndDate;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.RadioButton rdPreviousWeek;
        private System.Windows.Forms.RadioButton rdPreviousMonth;
        private System.Windows.Forms.RadioButton rdPreviousQuarter;
        private System.Windows.Forms.RadioButton rdPreviousYear;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.RadioButton rdNotBetween;
        private System.Windows.Forms.RadioButton rd3160;
        private System.Windows.Forms.RadioButton rd6190;
        private System.Windows.Forms.RadioButton rdGT90Days;
        private System.Windows.Forms.GroupBox grSpecificWeekDay;
        private System.Windows.Forms.CheckBox ckSpecificWeekDay;
        private System.Windows.Forms.CheckBox ckSaturday;
        private System.Windows.Forms.CheckBox ckFriday;
        private System.Windows.Forms.CheckBox ckThursday;
        private System.Windows.Forms.CheckBox ckWednesday;
        private System.Windows.Forms.CheckBox ckTuesday;
        private System.Windows.Forms.CheckBox ckMonday;
        private System.Windows.Forms.CheckBox ckSunday;
        private System.Windows.Forms.GroupBox grSpecificTime;
        private System.Windows.Forms.CheckBox ckSpecificTime;
        private System.Windows.Forms.DateTimePicker tmStartSpecificTime;
        private System.Windows.Forms.DateTimePicker tmEndSpecificTime;
    }
}