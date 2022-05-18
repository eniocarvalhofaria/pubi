using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.ETL
{
    class ReturnCommand: IScriptCommand
    {
        public ReturnCommand(Script owner, string text)
        {
            this.owner = owner;
            _Text = text;
        }
        private string _Text = null;
        private bool _HasExecutionError = false;
        public  bool HasExecutionError
        {
            get { return _HasExecutionError; }
            set { _HasExecutionError = value; }
        }
        private bool _IsInTry = false;
        public  bool IsInTry
        {
            get { return _IsInTry; }
            set { _IsInTry = value; }
        }
        public int ReturnCode { get; set; }
        public void Execute()
        {
            Log.GetInstance().WriteLine("return(" + _Text + ")");
            var rc = owner.GetExpression(_Text);

            try
            {
                owner.ReturnCode = Convert.ToInt32(rc);
            }
            catch (Exception ex)
            {

            }
        }
        private Script owner;
    }
}
