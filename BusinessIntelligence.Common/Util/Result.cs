using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Util
{
    public class Result
    {
        public Result(bool sucess,string message)
        {
            _Sucess = sucess;
            _Message = message;
        }
        public Result(bool sucess)
        {
            _Sucess = sucess;
            if (sucess)
            {
                _Message = "Comando executado com êxito!";
            }
        }
        private bool _Sucess;
        private string _Message;

        public bool Sucess
        {
            get
            {
                return _Sucess;
            }


        }

        public string Message
        {
            get
            {
                return _Message;
            }

        }
    }
}
