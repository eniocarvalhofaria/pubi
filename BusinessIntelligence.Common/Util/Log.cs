using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Util
{
    public class Log
    {
        private Log()
        {

        }
        public static Log GetInstance()
        {
            return GetInstance(".");

        }
        static Dictionary<string, Log> instances = new Dictionary<string, Log>();
        public static Log GetInstance(string name)
        {
            Log instance;
            if (!instances.TryGetValue(name, out instance))
            {
                instance = new Log();
                instances.Add(name, instance);
            }
            return instance;
        }
         private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set
            {
                this.WriteLine("Changing Log file name to:" + value);

                _FileName = value;


            }
        }
        private bool isWriting = false;
        private bool updateVerified = false;
        private ControlledExecution ce = null;
        private void updateExecutionControl(string text)
        {
            if(ce == null && !updateVerified)
            {
                var fi = new System.IO.FileInfo(FileName);

                ce = ControlledExecution.GetInstance(fi.Name.Split('.')[0]);
                updateVerified = true;
            }

            if (ce != null)
            {
                ce.UpdateLog(sb.ToString());
            }
        }
        StringBuilder sb = new StringBuilder();
        public void Write(string text)
        {
            Console.Write(text);
            sb.Append(text);
            try
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    if (!isWriting)
                    {
                        isWriting = true;
                        using (var sw = new System.IO.StreamWriter(FileName, true))
                        {
                            sw.Write(text);
                            sw.Close();
                            sw.Dispose();
                        }
                     updateExecutionControl(text);
                        isWriting = false;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        Write(text);
                    }
                }
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace);
            }
        }
        public void Write(Exception ex)
        {
            string toWrite = "Erro: " + ex.Message + "\r\n";
            toWrite += "Rastreamento: " + ex.StackTrace + "\r\n";
            Console.Write(toWrite);
            if (!string.IsNullOrEmpty(FileName))
            {
                using (var sw = new System.IO.StreamWriter(FileName, true))
                {
                    sw.Write(toWrite);
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        public void WriteLine(string text)
        {
            Write(text + "\r\n");
        }
        public void WriteLine()
        {
            Write("\r\n");
        }
    }
}
