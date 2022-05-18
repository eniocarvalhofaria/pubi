using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.Controls
{
    public partial class FormDateSelector : FormSelector
    {
        public FormDateSelector()
        {

            InitializeComponent();
        }
        private void rdCustom_CheckedChanged(object sender, EventArgs e)
        {
            lbStartDate.Enabled = rdBetween.Checked || rdNotBetween.Checked;
            lbEndDate.Enabled = lbStartDate.Enabled;
            dtStartDate.Enabled = lbStartDate.Enabled;
            dtEndDate.Enabled = lbStartDate.Enabled;

        }


        public DateTime StartDate
        {
            get { return dtStartDate.Value; }
            set { dtStartDate.Value = value; }
        }
        public DateTime EndDate
        {
            get { return dtEndDate.Value; }
            set { dtEndDate.Value = value; }
        }
        DateCriterialType _DateTypeSelected;
        public DateCriterialType DateTypeSelected
        {
            get
            {
                return _DateTypeSelected;


            }
        }
        string _TemplateExpression;
        public string TemplateExpression
        {
            get { return _TemplateExpression; }
            set { _TemplateExpression = value; }
        }
        public DateTime StartOfWeek(DateTime dt)
        {
            return dt.AddDays(-1 * (int)dt.DayOfWeek).Date;
        }
        public DateTime EndOfWeek(DateTime dt)
        {
            return StartOfWeek(dt).AddDays(6);
        }
        public string GetExpression(string fieldName, DateTime dateReference)
        {
            DateCriterial dc = new DateCriterial();
            dc.DateTypeSelected = DateTypeSelected;

            switch (DateTypeSelected)
            {
                case DateCriterialType.Today:
                    {
                        TemplateExpression = "{0} = cast('" + dateReference.ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Hoje";
                        break;
                    }
                case DateCriterialType.Yesterday:
                    {
                        TemplateExpression = "{0} = cast('" + dateReference.AddDays(-1).ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Ontem";
                        break;
                    }
                case DateCriterialType.ThisWeek:
                    {
                        TemplateExpression = "{0} between cast('" + StartOfWeek(dateReference).ToString("yyyy-MM-dd") + "' as date) and cast('" +  EndOfWeek(dateReference).ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Esta semana";
                        break;
                    }
                case DateCriterialType.PreviousWeek:
                    {
                        TemplateExpression = "{0} between cast('" + StartOfWeek(dateReference).AddDays(-7).ToString("yyyy-MM-dd") + "' as date) and cast('" + EndOfWeek(dateReference).AddDays(-7).ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Semana anterior";
                        break;
                    }
                case DateCriterialType.ThisMonth:
                    {
                        TemplateExpression = "EXTRACT(YEAR FROM {0}) * 12  + EXTRACT(MONTH FROM {0}) = EXTRACT(YEAR FROM {1}) * 12 +  EXTRACT(MONTH FROM {1})";
                        Selector.SelectionInfo = "Este mês";
                        break;
                    }
                case DateCriterialType.PreviousMonth:
                    {
                        TemplateExpression = "EXTRACT(YEAR FROM {0}) * 12  + EXTRACT(MONTH FROM {0}) = EXTRACT(YEAR FROM {1}) * 12 +  EXTRACT(MONTH FROM {1}) - 1";
                        Selector.SelectionInfo = "Mês anterior";
                        break;
                    }
                case DateCriterialType.ThisQuarter:
                    {
                        TemplateExpression = "EXTRACT(YEAR FROM {0}) * 4  + EXTRACT(MONTH FROM {0}) = EXTRACT(YEAR FROM {1}) * 4 +  (((EXTRACT(MONTH FROM {1}) - 1)/ 3) + 1)";
                        Selector.SelectionInfo = "Este trimestre";
                        break;
                    }
                case DateCriterialType.PreviousQuarter:
                    {
                        TemplateExpression = "EXTRACT(YEAR FROM {0}) * 4  + EXTRACT(MONTH FROM {0}) = EXTRACT(YEAR FROM {1}) * 4 +  (((EXTRACT(MONTH FROM {1}) - 1)/ 3) + 1) - 1";
                        Selector.SelectionInfo = "Trimestre anterior";
                        break;
                    }
                case DateCriterialType.ThisYear :
                    {
                        TemplateExpression = "EXTRACT(YEAR FROM {0}) = EXTRACT(YEAR FROM {1})";
                        Selector.SelectionInfo = "Este ano";
                        break;
                    }
                case DateCriterialType.PreviousYear:
                    {
                        TemplateExpression = "EXTRACT(YEAR FROM {0}) = EXTRACT(YEAR FROM {1}) -1";
                        Selector.SelectionInfo = "Ano anterior";
                        break;
                    }
                case DateCriterialType.Last30Days:
                    {
                        TemplateExpression = "{0} between dateadd(day,-30,cast({1} as date)) and cast({1} as date)";
                        Selector.SelectionInfo = "Últimos 30 dias";
                        break;
                    }
                case DateCriterialType.Last60Days:
                    {
                        TemplateExpression = "{0} between dateadd(day,-60,cast({1} as date)) and cast({1} as date)";
                        Selector.SelectionInfo = "Últimos 60 dias";
                        break;
                    }
                case DateCriterialType.Last90Days:
                    {
                        TemplateExpression = "{0} between dateadd(day,-90,cast({1} as date)) and cast({1} as date)";
                        Selector.SelectionInfo = "Últimos 90 dias";
                        break;
                    }
                case DateCriterialType.Last120Days:
                    {
                        TemplateExpression = "{0} between dateadd(day,-120,cast({1} as date)) and cast({1} as date)";
                        Selector.SelectionInfo = "Últimos 120 dias";
                        break;
                    }
                case DateCriterialType.Last180Days:
                    {
                        TemplateExpression = "{0} between dateadd(day,-180,cast({1} as date)) and cast({1} as date)";
                        Selector.SelectionInfo = "Últimos 180 dias";
                        break;
                    }
                case DateCriterialType.Last365Days:
                    {
                        TemplateExpression = "{0} between dateadd(day,-365,cast({1} as date)) and cast({1} as date)";
                        Selector.SelectionInfo = "Últimos 365 dias";
                        break;
                    }
                case DateCriterialType.Between:
                    {
                        TemplateExpression = "{0} between '" + StartDate.ToString("yyyy-MM-dd") + "' as date) and cast('" + EndDate.ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Está entre \r\n" + StartDate.ToString("yyyy-MM-dd") + " e \r\n" + EndDate.ToString("yyyy-MM-dd");

                        dc.StartDate = StartDate;
                        dc.EndDate = EndDate;
                        break;
                    }
                case DateCriterialType.NotBetween:
                    {
                        TemplateExpression = "{0} not between  cast('" + StartDate.ToString("yyyy-MM-dd") + "' as date) and  cast('" + EndDate.ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Não está entre \r\n" + StartDate.ToString("yyyy-MM-dd") + " e \r\n" + EndDate.ToString("yyyy-MM-dd");
                        dc.StartDate = StartDate;
                        dc.EndDate = EndDate;
                        break;
                    }
                case DateCriterialType.Between31And60:
                    {

                        TemplateExpression = "{0} between  cast('" + dateReference.AddDays(-60).ToString("yyyy-MM-dd") + "' as date) and  cast('" + dateReference.AddDays(-31).ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Está entre \r\n" + dateReference.AddDays(-60).ToString("yyyy-MM-dd") + " e \r\n" + dateReference.AddDays(-31).ToString("yyyy-MM-dd");
                        dc.StartDate = dateReference.AddDays(-60);
                        dc.EndDate = dateReference.AddDays(-31);
                        break;
                    }
                case DateCriterialType.Between61And90:
                    {

                        TemplateExpression = "{0} between  cast('" + dateReference.AddDays(-90).ToString("yyyy-MM-dd") + "' as date) and  cast('" + dateReference.AddDays(-61).ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Está entre \r\n" + dateReference.AddDays(-90).ToString("yyyy-MM-dd") + " e \r\n" + dateReference.AddDays(-61).ToString("yyyy-MM-dd");
                        dc.StartDate = dateReference.AddDays(-90);
                        dc.EndDate = dateReference.AddDays(-61);
                        break;
                    }
                case DateCriterialType.Before90:
                    {

                        TemplateExpression = "{0}  < cast('" + dateReference.AddDays(-90).ToString("yyyy-MM-dd") + "' as date)";
                        Selector.SelectionInfo = "Anterior 90 dias" ;
                        dc.StartDate = dateReference.AddDays(-90);
                        dc.EndDate = dateReference.AddDays(-61);
                        break;
                    }

            }
            Selector.Criterial = dc;
            return string.Format(TemplateExpression + "\r\n", fieldName, "cast('" + dateReference.ToString("yyyy-MM-dd") + "' as date)");
        }
        private void btOk_Click(object sender, EventArgs e)
        {

            if (rdToday.Checked)
            {


                _DateTypeSelected = DateCriterialType.Today;
            }
            if (rdYesterday.Checked)
            {
                _DateTypeSelected = DateCriterialType.Yesterday;
            }
            if (rdThisWeek.Checked)
            {
                _DateTypeSelected = DateCriterialType.ThisWeek;
            }
            if (rdPreviousWeek.Checked)
            {
                _DateTypeSelected = DateCriterialType.PreviousWeek;
            }
            if (rdPreviousMonth.Checked)
            {
                _DateTypeSelected = DateCriterialType.PreviousMonth;
            }
            if (rdPreviousQuarter.Checked)
            {
                _DateTypeSelected = DateCriterialType.PreviousQuarter;
            }
            if (rdPreviousYear.Checked)
            {
                _DateTypeSelected = DateCriterialType.PreviousYear ;
            }
            if (rdThisMonth.Checked)
            {
                _DateTypeSelected = DateCriterialType.ThisMonth;
            }
            if (rdThisQuarter.Checked)
            {
                _DateTypeSelected = DateCriterialType.ThisQuarter;
            }
            if (rdThisYear .Checked)
            {
                _DateTypeSelected = DateCriterialType.ThisYear ;
            }
            if (rd30Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Last30Days;
            }
            if (rd60Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Last60Days;
            }
            if (rd90Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Last90Days;
            }
            if (rd120Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Last120Days;
            }
            if (rd180Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Last180Days;
            }
            if (rd365Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Last365Days;
            }
            if (rdBetween.Checked)
            {
                _DateTypeSelected = DateCriterialType.Between;
            }
            if (rd3160.Checked)
            {
                _DateTypeSelected = DateCriterialType.Between31And60;
            }
            if (rd6190.Checked)
            {
                _DateTypeSelected = DateCriterialType.Between61And90;
            }
            if (rdGT90Days.Checked)
            {
                _DateTypeSelected = DateCriterialType.Before90;
            }
            if (rdNotBetween.Checked)
            {
                _DateTypeSelected = DateCriterialType.NotBetween;
            }
          
            GetExpression("", DateTime.Now);
            this.Close();
        }

        private void ckSpecificTime_CheckedChanged(object sender, EventArgs e)
        {
            grSpecificWeekDay.Enabled = ckSpecificWeekDay.Checked;
        }

        private void rdToday_CheckedChanged(object sender, EventArgs e)
        {
            disableSpecificTime();
        }

        private void rdYesterday_CheckedChanged(object sender, EventArgs e)
        {
            disableSpecificTime();
        }
        
        private void disableSpecificTime()
        {
            bool disable = rdToday.Checked || rdToday.Checked;
            ckSpecificWeekDay.Enabled = !disable;
            grSpecificWeekDay.Enabled = ckSpecificWeekDay.Checked && !disable;
            
        }

        private void ckSpecificTime_CheckedChanged_1(object sender, EventArgs e)
        {
            grSpecificTime.Enabled = ckSpecificTime.Checked;
        }

    }

}
