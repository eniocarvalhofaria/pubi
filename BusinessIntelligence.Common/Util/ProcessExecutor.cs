using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;
namespace BusinessIntelligence.Util
{
    public class ProcessExecutor
    {
        DateTime _StartTime;
        DateTime _EndTime;
        bool _IsRunning;
        int _ReturnCode;
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        string _Arguments;
        System.IO.StringWriter H = new System.IO.StringWriter();
        public void Execute()
        {
            WaitForExit();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;

            _StartTime = DateTime.Now;
            _IsRunning = true;
            if (_FileName.EndsWith(".bat"))
            {
                p.StartInfo.FileName = "cmd";
                p.Start();
                p.StandardInput.WriteLine(_FileName + " " + _Arguments);
                p.StandardInput.WriteLine("exit %errorlevel%");
            }
            else
            {

                p.StartInfo.FileName = _FileName;
                p.StartInfo.Arguments = _Arguments;
                p.Start();
            }



            new Thread(this.Retorna).Start();
            new Thread(this.RetornaErro).Start();

            Log.GetInstance().WriteLine(DrawLine(79, '=') + "\r\n" + this.StartTime + "     PROCESSO INICIADO" + "\r\n");
            this.WaitForExit();
            Log.GetInstance().WriteLine(this.EndTime + "    PROCESSO TERMINADO" + "\r\n" + "     A DURACAO DO PROCESSO FOI " + DurationString(this.StartTime, this.EndTime) + " RETURN CODE = " + this.ReturnCode + "\r\n" + DrawLine(79, '='));
        }
        public string GetAllOutputFromApplication()
        {
            PararLeitura = true;
            string a = "";
            int count = 0;
            const int ErrorLimit = 60;
            do
            {
                try
                {
                    a = p.StandardOutput.ReadToEnd();
                    break;
                }
                catch (Exception ex)
                {
                    if (count >= ErrorLimit)
                    {
                        throw new System.Exception("Limite de erros excedido na tentaiva de leitura da saida do console.");
                    }
                    count += 1;
                    //Erro de leitura do buffer
                    Thread.Sleep(1000);
                }
            } while (true);
            return H.ToString() + a;
        }
        public string GetOutputFromApplication()
        {
            return H.ToString();
        }
        public void SendCommandToApplication(string CommandText)
        {
            p.StandardInput.WriteLine(CommandText);
        }
        private bool PararLeitura;
        private void RetornaErro()
        {

            var sb = new StringBuilder();
            //      while (p.StandardError.Peek() != -1 && !p.StandardError.EndOfStream)
            while (!p.StandardError.EndOfStream)
            {

                if (p.StandardError.Peek() != -1)
                {
                    sb.Append((char)p.StandardError.Read());
                    if (IsRunning && sb.ToString().Contains("Unhandled Exception") && sb.ToString().Contains(".cs:line ") && sb.ToString().Substring(sb.Length - 1, 1) == "\n")
                    {
                        Thread.Sleep(1000);
                        p.Kill();
                        _IsRunning = false;
                        break;
                    }

                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            H.WriteLine(sb.ToString());
        }
        private void Retorna()
        {
            string g = null;
            int cont = 0;
            do
            {
                try
                {
                    g = p.StandardOutput.ReadLine();
                    if (string.IsNullOrEmpty(g))
                    {
                        if (cont == 0)
                        {
                            if (this._MessageEnabled)
                            {
                                Log.GetInstance().WriteLine(g);
                            }
                            H.WriteLine(g);
                            cont = 1;
                        }
                        else
                        {
                            cont += 1;
                        }
                    }
                    else
                    {
                        cont = 0;
                        if (this._MessageEnabled)
                        {
                            Log.GetInstance().WriteLine(g);
                        }

                        H.WriteLine(g);
                    }
                    if (!(this._IsRunning) || PararLeitura)
                    {
                        return;
                    }
                }
                catch
                {
                }
            } while (true);
        }

        public void WaitForExit()
        {
            try
            {
                p.WaitForExit();
                _EndTime = p.ExitTime;
                _ReturnCode = p.ExitCode;
                _IsRunning = false;
            }
            catch
            {
            }
        }
        public void Kill()
        {
            p.Kill();
        }
        public bool IsRunning
        {
            get { return this._IsRunning; }
        }
        public DateTime StartTime
        {
            get { return this._StartTime; }
        }
        public DateTime EndTime
        {
            get { return this._EndTime; }
        }
        public int Id
        {
            get { return p.Id; }
        }
        public Process Process
        {
            get { return p; }
        }
        public int ReturnCode
        {
            get { return _ReturnCode; }
        }
        string _FileName;
        public virtual string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public virtual string Arguments
        {
            get { return _Arguments; }
            set { _Arguments = value; }
        }
        public ProcessExecutor(bool MessageEnabled = true)
        {
            this._MessageEnabled = MessageEnabled;
        }
        public ProcessExecutor(string Path, bool MessageEnabled = true)
        {
            this._MessageEnabled = MessageEnabled;
            _FileName = Path;
        }
        bool _MessageEnabled;
        public virtual bool MessageEnabled
        {
            get { return this._MessageEnabled; }
            set { this._MessageEnabled = value; }
        }
        public virtual string WorkingDirectory
        {
            get { return p.StartInfo.WorkingDirectory; }
            set { p.StartInfo.WorkingDirectory = value; }
        }
        public ProcessExecutor(string Path, string Arguments, bool MessageEnabled = true)
        {
            this._MessageEnabled = MessageEnabled;
            _Arguments = Arguments;
            _FileName = Path;
        }
        static string DrawLine(int Width, Char c)
        {
            StringBuilder a = new StringBuilder("");
            for (int i = 0; i < Width; i++)
            {
                a.Append(c);
            }
            return a.ToString();
        }
        public static string DurationString(DateTime StartTime, DateTime EndTime)
        {
            string DuracaoHora = null;
            string DuracaoMinuto = null;
            string DuracaoSegundo = null;

            if (DateAndTime.DateDiff(DateInterval.Hour, StartTime, EndTime) > 1)
            {
                DuracaoHora = DateAndTime.DateDiff(DateInterval.Hour, StartTime, EndTime) + " HORAS ,";
            }
            else if (DateAndTime.DateDiff(DateInterval.Hour, StartTime, EndTime) == 1)
            {
                DuracaoHora = DateAndTime.DateDiff(DateInterval.Hour, StartTime, EndTime) + " HORA ,";
            }
            else
            {
                DuracaoHora = "";
            }
            if (DateAndTime.DateDiff(DateInterval.Minute, StartTime, EndTime) - (Conversion.Val(DuracaoHora) * 60) > 1)
            {
                DuracaoMinuto = DateAndTime.DateDiff(DateInterval.Minute, StartTime, EndTime) - Conversion.Val(DuracaoHora) * 60 + " MINUTOS ,";
            }
            else if (DateAndTime.DateDiff(DateInterval.Minute, StartTime, EndTime) == 1)
            {
                DuracaoMinuto = DateAndTime.DateDiff(DateInterval.Minute, StartTime, EndTime) - Conversion.Val(DuracaoHora) * 60 + " MINUTO ,";
            }
            else
            {
                DuracaoMinuto = "";
            }
            if (DateAndTime.DateDiff(DateInterval.Second, StartTime, EndTime) - (Conversion.Val(DuracaoMinuto) * 60) - (Conversion.Val(DuracaoHora) * 3600) > 1)
            {
                DuracaoSegundo = DateAndTime.DateDiff(DateInterval.Second, StartTime, EndTime) - (Conversion.Val(DuracaoMinuto) * 60) - (Conversion.Val(DuracaoHora) * 3600) + " SEGUNDOS.";
            }
            else
            {
                DuracaoSegundo = DateAndTime.DateDiff(DateInterval.Second, StartTime, EndTime) - (Conversion.Val(DuracaoMinuto) * 60) - (Conversion.Val(DuracaoHora) * 3600) + " SEGUNDO.";
            }
            if (DateAndTime.DateDiff(DateInterval.Second, StartTime, EndTime) == 0)
            {
                return "MENOS DE 1 SEGUNDO";
            }
            else
            {
                return DuracaoHora + DuracaoMinuto + DuracaoSegundo;
            }
        }

    }
}
