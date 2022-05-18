using Microsoft.VisualBasic;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Xml;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BusinessIntelligence.Util;


namespace BusinessIntelligence.Data
{
    public class QueryExecutor
    {


        #region "Variaveis"

        System.Data.IDbCommand Comando;
        //     Dim _ConnectionString As String
        int _CommandTimeout = 0;
        DateTime _StartTime;
        DateTime _EndTime;
        bool _IsRunning;
        int _ReturnCode;
        int _RecordsAffected;
        string _DatabaseMessage = "";
        bool _StopReturn;
        //     int _CurrentRowIndex = null;
        System.Data.DataTable _DataTable1 = new System.Data.DataTable("aaaa");
        //    ConnectionType _ConnectionType;

  
        #endregion

        System.Data.IDbConnection _Connection;
        public IDbConnection Connection
        {
            get { return _Connection; }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Objeto que contem os dados solicitados atraves do comando ReturnData
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[84933]	03/08/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------

        private string _QueryName;
        public string QueryName
        {
            get { return _QueryName; }
        }
        string DataString = "";
        System.Data.Common.DbDataReader Leitor;
        private DataTable ReturnData1(bool WriteSqlCommand, bool ReturnDataString = false)
        {
            switch (Connection.State)
            {
                case ConnectionState.Open:

                    break;
                case ConnectionState.Closed:
                    Connection.Open();
                    break;
                default:
                    Connection.Close();
                    Connection.Open();
                    break;
            }
            this._StopReturn = false;
            _StartTime = DateTime.Now;
            _IsRunning = true;
            foreach (KeyValuePair<string, string> item in Parameters)
            {
                _CurrentRequest = _CurrentRequest.Replace("<@" + item.Key + "@>", item.Value).Trim();
                _CurrentRequest = _CurrentRequest.Replace("/@" + item.Key + "@/", item.Value).Trim();
            }
            if (WriteSqlCommand)
            {
                Log.GetInstance().WriteLine("\r\n" + this._CurrentRequest);
            }
            Comando.CommandText = this._CurrentRequest;
            Comando.CommandTimeout = this.CommandTimeout;
            _EndTime = DateTime.MinValue;
            _RecordsAffected = -1;
            _DatabaseMessage = null;
            _ReturnCode = int.MinValue;
            if (BeforeSendCommand != null)
            {
                BeforeSendCommand(this);
            }
            try
            {
                try
                {
                    try
                    {
                        if (WriteSqlCommand)
                        {
                            Log.GetInstance().WriteLine(DrawLine(79, '='));
                            Log.GetInstance().WriteLine(this.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "    SQL ENVIADO");
                        }

                        if (Leitor != null && !Leitor.IsClosed)
                        {
                            Leitor.Close();
                        }
                        Leitor = (System.Data.Common.DbDataReader)Comando.ExecuteReader();
                        DataTable dt = null;
                        if (ReturnDataString)
                        {
                            DataString = RetornaDataString(Leitor);
                        }
                        else
                        {
                            dt = GetDataTable(Leitor);
                        }

                        _RecordsAffected = Leitor.RecordsAffected;
                        Leitor.Close();
                        if (Connection.ConnectionString.Contains("redshift") && !isInTransaction)
                        {
                            Comando.CommandText = "commit";
                            Comando.ExecuteNonQuery();
                        }
                        this._ReturnCode = 0;
                        _EndTime = DateTime.Now;
                        _IsRunning = false;
                        return dt;
                    }
                    catch (OleDbException ex)
                    {
                        _EndTime = DateTime.Now;
                        _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
                        _ReturnCode = (ex.Errors[ex.Errors.Count - 1].NativeError) * -1;
                        _IsRunning = false;
                    }
                }
                catch (System.Data.Odbc.OdbcException ex)
                {
                    _EndTime = DateTime.Now;
                    _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
                    _ReturnCode = (ex.Errors[ex.Errors.Count - 1].NativeError) * -1;
                    _IsRunning = false;
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                _EndTime = DateTime.Now;

                _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
                _ReturnCode = ex.ErrorCode;
                _IsRunning = false;
            }
            Comando.Dispose();

            if (WriteSqlCommand)
            {

                if (this.RecordsAffected > -1)
                {
                    Log.GetInstance().WriteLine(this.EndTime + "    PROCESSO EXECUTADO  REGISTROS PROCESSADOS : " + this.RecordsAffected);
                }
                else
                {
                    Log.GetInstance().WriteLine(this.EndTime + "    PROCESSO EXECUTADO");
                }
                if (this.DatabaseMessage != null)
                {
                    Log.GetInstance().WriteLine(this.DatabaseMessage);
                }
                Log.GetInstance().WriteLine("     A DURACAO DO PROCESSO FOI " + DurationString(this.StartTime, this.EndTime) + " RETURN CODE = " + this.ReturnCode);
                Log.GetInstance().WriteLine(DrawLine(79, '='));
            }
            if (EndQuery != null)
            {
                EndQuery(this);
            }

            if (_CheckError)
            {
                if (EmErro)
                {
                    return null;
                }
                EmErro = true;
                if (OnError != null)
                {
                    OnError(this.ReturnCode, this.DatabaseMessage);
                }
                EmErro = false;
            }
            return null;

        }
        bool _CheckError;
        bool EmErro;
        /// <summary>
        /// Aborta um Sql
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[84933]	03/08/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public void Abort()
        {
            try
            {
                this._StopReturn = true;
                Comando.Cancel();
            }
            catch
            {
            }
            _RecordsAffected = -1;
            _EndTime = DateTime.Now;
            _DatabaseMessage = "Processo Cancelado";
            _ReturnCode = -1;
            _IsRunning = false;
        }
        public System.Data.Common.DbDataReader ReturnDataReader(bool WriteSqlCommand = true, bool CheckError = true)
        {
            _CheckError = CheckError;
            _CurrentRequest = _CommandText;
            return ReturnDataReader1(WriteSqlCommand);
        }
        public System.Data.Common.DbDataReader ReturnDataReader(string CommandText, bool WriteSqlCommand = true, bool CheckError = true)
        {
            _CheckError = CheckError;
            if (CommandText.Substring(0, 1) == ".")
            {
                this._CurrentRequest = Util.EmbeddedResource.TextResource(this.DataAssembly, CommandText);
            }
            else
            {
                this._CurrentRequest = CommandText;
            }
            return ReturnDataReader1(WriteSqlCommand);
        }
        private System.Data.Common.DbDataReader ReturnDataReader1(bool WriteSqlCommand)
        {
            switch (Connection.State)
            {
                case ConnectionState.Open:

                    break;
                case ConnectionState.Closed:
                    Connection.Open();
                    break;
                default:
                    Connection.Close();
                    Connection.Open();
                    break;
            }
            this._StopReturn = false;
            _StartTime = DateTime.Now;
            _IsRunning = true;
            foreach (KeyValuePair<string, string> item in Parameters)
            {
                _CurrentRequest = _CurrentRequest.Replace("<@" + item.Key + "@>", item.Value).Trim();
                _CurrentRequest = _CurrentRequest.Replace("/@" + item.Key + "@/", item.Value).Trim();

            }
            if (WriteSqlCommand)
            {
                Log.GetInstance().WriteLine(this._CurrentRequest);
            }
            Comando.CommandText = this._CurrentRequest;
            Comando.CommandTimeout = this.CommandTimeout;

            _EndTime = DateTime.MinValue;
            _RecordsAffected = -1;
            _DatabaseMessage = null;
            _ReturnCode = int.MinValue;
            if (BeforeSendCommand != null)
            {
                BeforeSendCommand(this);
            }
            try
            {
                try
                {
                    try
                    {
                        if (Leitor != null && !Leitor.IsClosed)
                        {
                            Leitor.Close();
                        }
                        Leitor = (System.Data.Common.DbDataReader)Comando.ExecuteReader();
                        _RecordsAffected = Leitor.RecordsAffected;

                        this._ReturnCode = 0;
                        _EndTime = DateTime.Now;
                        _IsRunning = false;
                        return Leitor;
                    }
                    catch (OleDbException ex)
                    {
                        TrataErro(ex, (ex.Errors[ex.Errors.Count - 1].NativeError) * -1);

                    }
                }
                catch (System.Data.Odbc.OdbcException ex)
                {
                    TrataErro(ex, (ex.Errors[ex.Errors.Count - 1].NativeError) * -1);
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                TrataErro(ex, ex.ErrorCode);

            }
            if (EndQuery != null)
            {
                EndQuery(this);
            }


            if (EmErro)
            {
                return null;
            }
            EmErro = true;
            if (_CheckError)
            {
                if (OnError != null)
                {
                    OnError(this.ReturnCode, this.DatabaseMessage);
                }
                EmErro = false;
            }
            return null;

        }
        string DrawLine(int Width, Char c)
        {
            StringBuilder a = new StringBuilder("");
            for (int i = 0; i < Width; i++)
            {
                a.Append(c);
            }
            return a.ToString();
        }
        private void TrataErro(Exception ex, int errorCode)
        {
            _EndTime = DateTime.Now;
            _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
            _ReturnCode = errorCode;
            _IsRunning = false;

        }
        ///  -----------------------------------------------------------------------------
        /// <summary>
        /// Executa um Sql e armazena os dados retornados na propriedade DataReturned
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[84933]	03/08/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public System.Data.DataTable ReturnData(bool WriteSqlCommand = true, bool CheckError = true, int RowsLimit = 0)
        {
            _CurrentRequest = CommandText;
            _CheckError = CheckError;
            _OldRowsLimit = _RowsLimit;
            _RowsLimit = RowsLimit;
            DataTable d = ReturnData1(WriteSqlCommand);
            _RowsLimit = _OldRowsLimit;
            return d;
        }

        int _OldRowsLimit;
        int _RowsLimit = 0;
        public virtual int RowsLimit
        {
            get { return _RowsLimit; }
            set { _RowsLimit = value; }
        }

        public System.Data.DataTable ReturnData(string CommandText, bool WriteSqlCommand = true, bool CheckError = true, int RowsLimit = 0)
        {
            _CheckError = CheckError;
            if (CommandText.Substring(0, 1) == ".")
            {
                this._CurrentRequest = Util.EmbeddedResource.TextResource(this.DataAssembly, CommandText);
            }
            else
            {
                this._CurrentRequest = CommandText;
            }
            _OldRowsLimit = _RowsLimit;
            _RowsLimit = RowsLimit;
            DataTable d = ReturnData1(WriteSqlCommand);
            _RowsLimit = _OldRowsLimit;
            try
            {
                return d;
            }
            catch
            {
                return null;
            }
        }

        public System.String ReturnDataString(string commandText, string ColumnDelimiter = "\t", string RowDelimiter = "\r\n", bool WriteSqlCommand = true, bool CheckError = true, int RowsLimit = 0)
        {
            _CheckError = CheckError;
            if (CommandText.Substring(0, 1) == ".")
            {
                this._CurrentRequest = Util.EmbeddedResource.TextResource(this.DataAssembly, commandText);
            }
            else
            {
                this._CurrentRequest = CommandText;
            }
            _OldRowsLimit = _RowsLimit;
            _RowsLimit = RowsLimit;
            ReturnData1(WriteSqlCommand);
            _RowsLimit = _OldRowsLimit;
            try
            {
                return this.DataString;
            }
            catch
            {
                return null;
            }
        }
        ///  -----------------------------------------------------------------------------
        /// <summary>
        /// Executa um Sql e armazena os dados retornados na propriedade DataReturned 
        /// </summary>
        /// <remarks>
        /// Se o parametro Message for = True as mensagens de retorno serao aguardadas 
        /// </remarks>
        /// <history>
        /// 	[84933]	03/08/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        ///  
        public System.Data.DataTable GetDataTable(System.Data.Common.DbDataReader DR)
        {
            System.Data.DataTable Retorno = new System.Data.DataTable();
            DataTable Schema = DR.GetSchemaTable();
            System.Collections.Generic.List<int> CasasDecimais = new System.Collections.Generic.List<int>();
            bool SeparadorIgual = true;

            foreach (DataRow Campo in Schema.Rows)
            {
                Type DataType = null;
                try
                {
                   
                    DataType = Type.GetType(Campo["DataType"].ToString());
                }
                catch (Exception ex)
                {
                }
                DataColumn A = null;
                if (DataType == null)
                {
                    A = new DataColumn(Campo[0].ToString());
                }
                else
                {
                    A = new DataColumn(Campo[0].ToString(), DataType);
                }

                if (Campo["DataType"].ToString() == "System.Decimal")
                {
                    CasasDecimais.Add(Convert.ToInt32(Campo[4]));
                }
                else
                {
                    CasasDecimais.Add(0);
                }
                Retorno.Columns.Add(A);
                if (Campo["DataType"].ToString() == "System.Decimal" && SeparadorIgual)
                {
                    if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator != ".")
                    {
                        SeparadorIgual = false;
                    }
                }
            }
            if (DR.HasRows)
            {
                if (!SeparadorIgual && _Connection is OdbcConnection && _Connection.ConnectionString.ToLower().Contains("teradata"))
                {
                    object[] Linha = new object[DR.FieldCount + 1];
                    System.Data.DataRow Row = null;
                    int e = 0;
                    while (DR.Read() && _StopReturn == false)
                    {
                        Row = Retorno.NewRow();

                        for (Int32 i = 0; i <= DR.FieldCount - 1; i++)
                        {
                            if (CasasDecimais[i] > 0 & (!object.ReferenceEquals(DR[i], DBNull.Value)))
                            {
                                Row[i] = (int)(DR[i]) / (Math.Pow(10, CasasDecimais[i]));
                            }
                            else
                            {
                                Row[i] = DR[i];
                            }

                        }


                        Retorno.Rows.Add(Row);
                        e += 1;
                        if (e == this._RowsLimit)
                        {
                            break; // TODO: might not be correct. Was : Exit While
                        }
                    }

                }
                else
                {
                    object[] Linha = new object[DR.FieldCount + 1];
                    System.Data.DataRow Row = null;
                    int e = 0;
                    while (DR.Read() && _StopReturn == false)
                    {
                        Row = Retorno.NewRow();
                        for (Int32 i = 0; i <= DR.FieldCount - 1; i++)
                        {
                            Row[i] = DR[i];
                        }
                        Retorno.Rows.Add(Row);
                        e += 1;
                        if (e == this._RowsLimit)
                        {
                            break; // TODO: might not be correct. Was : Exit While
                        }
                    }
                }
            }

            DR.Close();
            return Retorno;
        }
        public int ExportFile(System.Data.Common.DbDataReader DR, string fileName, bool hasHeader, string RowDelimiter = "\r\n", string ColumnDelimiter = "\t")
        {
            int ret = 0;
            if (DR != null && DR.HasRows)
            {

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false, Encoding.Default))
                {
                    if (hasHeader)
                    {
                        bool isFirst = true;
                        foreach (DataRow c in DR.GetSchemaTable().Rows)
                        {
                            if (!isFirst)
                            {
                                sw.Write(ColumnDelimiter + c["ColumnName"].ToString());
                            }
                            else
                            {
                                sw.Write(c["ColumnName"].ToString());
                            }

                            isFirst = false;
                        }
                        sw.Write(RowDelimiter);
                    }
                    object[] Linha = new object[DR.FieldCount + 1];

                    while (DR.Read() && _StopReturn == false)
                    {
                        ret++;
                        for (Int32 i = 0; i <= DR.FieldCount - 1; i++)
                        {
                            if (i == 0)
                            {
                                if ((!object.ReferenceEquals(DR[i], DBNull.Value)))
                                {
                                    sw.Write(DR[i]);
                                }

                            }
                            else
                            {
                                if ((!object.ReferenceEquals(DR[i], DBNull.Value)))
                                {
                                    sw.Write(ColumnDelimiter);
                                    sw.Write(DR[i]);
                                }
                                else
                                {
                                    sw.Write(ColumnDelimiter);
                                }

                            }
                        }
                        sw.Write(RowDelimiter);
                    }
                    sw.Close();
                    sw.Dispose();
                }
            }
            DR.Close();
            return ret;
        }
        private string RetornaDataString(System.Data.Common.DbDataReader DR, string RowDelimiter = "\r\n", string ColumnDelimiter = "\t")
        {
            System.Text.StringBuilder Saida = new System.Text.StringBuilder("");
            if (DR.HasRows)
            {
                object[] Linha = new object[DR.FieldCount + 1];

                while (DR.Read() && _StopReturn == false)
                {
                    for (Int32 i = 0; i <= DR.FieldCount - 1; i++)
                    {
                        if (i == 0)
                        {
                            if ((!object.ReferenceEquals(DR[i], DBNull.Value)))
                            {
                                Saida.Append(DR[i]);
                            }

                        }
                        else
                        {
                            if ((!object.ReferenceEquals(DR[i], DBNull.Value)))
                            {
                                Saida.Append(ColumnDelimiter);
                                Saida.Append(DR[i]);
                            }
                            else
                            {
                                Saida.Append(ColumnDelimiter);
                            }

                        }
                    }
                    Saida.Append(RowDelimiter);
                }
            }
            return Saida.ToString();
        }
        public void StopReturn()
        {
            _StopReturn = true;
        }
        public event EndQueryEventHandler EndQuery;
        public delegate void EndQueryEventHandler(QueryExecutor sender);
        public event BeforeSendCommandEventHandler BeforeSendCommand;
        public delegate void BeforeSendCommandEventHandler(QueryExecutor sender);
        private Dictionary<string, string> commandParameters = new Dictionary<string, string>();
        public System.Data.IDbCommand DBCommand()
        {
            return Comando;
        }
        
        System.Collections.Generic.Dictionary<string, string> _Parameters = new System.Collections.Generic.Dictionary<string, string>();
        public void AddTextParameter(string ParameterName, string Value)
        {
            ParameterName = ParameterName.Replace("<@", "").Replace("@>", "");
            ParameterName = ParameterName.Replace("/@", "").Replace("@/", "");
            if (Parameters.ContainsKey(ParameterName))
            {
                Parameters.Remove(ParameterName);
            }
            this.Parameters.Add(ParameterName, Value);
        }
        bool _IsCancelled;
        public void CancelExecution()
        {
            this._IsCancelled = true;
        }
        public void Execute()
        {
            Execute(true, true);
        }
        public void Execute(bool WriteSqlCommand, bool CheckError, int commandTimeout = 0)
        {
            _CheckError = CheckError;
            CommandTimeout = commandTimeout;
            for (int i = 0; i <= this._CommandText.Split(';').Length - 1; i++)
            {
                this._CurrentRequest = _CommandText.Split(';')[i];
                if (!string.IsNullOrEmpty(this._CurrentRequest.Replace("\r\n", "").Trim()))
                {
                    Execute1(ref WriteSqlCommand);
                }
                if (_IsCancelled)
                {
                    return;
                }
            }
        }

        public void Execute(string CommandText, bool WriteSqlCommand = true, bool CheckError = true,int commandTimeout = 0)
        {
            this.CommandText = CommandText;
            CommandTimeout = commandTimeout;
            Execute(WriteSqlCommand, CheckError);
        }
        public void ExecuteTransaction(bool WriteSqlCommand = true, bool CheckError = true)
        {
            _CheckError = CheckError;
            this.BeginTransaction();
            for (int i = 0; i <= this._CommandText.Split(';').Length - 1; i++)
            {
                this._CurrentRequest = _CommandText.Split(';')[i];
                if (!string.IsNullOrEmpty(this._CurrentRequest.Replace("\r\n", "").Trim()))
                {
                    Execute1(ref WriteSqlCommand);
                    if (this._ReturnCode != 0)
                    {
                        this.RollBack();
                        return;
                    }
                }
            }
            this.Commit();
        }
        public void ExecuteTransaction(ref string CommandText, bool WriteSqlCommand = true, bool CheckError = true)
        {
            this.CommandText = CommandText;
            ExecuteTransaction(WriteSqlCommand, CheckError);
        }
        bool isInTransaction = false;
        IDbTransaction _Transaction;
        public void BeginTransaction()
        {
            _Transaction = Connection.BeginTransaction();
            this.Comando.Transaction = _Transaction;
            Comando.Connection = Connection;
            isInTransaction = true;
        }
        public void Commit()
        {
            _Transaction.Commit();
            isInTransaction = false;
        }
        public void RollBack()
        {
            _Transaction.Rollback();
            isInTransaction = false;
        }
        private List<string> _Tables = new List<string>();
        public List<string> Tables
        {
            get
            {
                if (_Tables.Count == 0)
                {
                    _Tables.AddRange(this.GetTabelas(this.CurrentRequest));
                }
                return _Tables;
            }
        }

        private void Execute1(ref bool WriteSqlCommand)
        {
            _StartTime = DateTime.Now;
            _IsRunning = true;
            foreach (KeyValuePair<string, string> item in Parameters)
            {
                _CurrentRequest = _CurrentRequest.Replace("<@" + item.Key + "@>", item.Value).Trim();
                _CurrentRequest = _CurrentRequest.Replace("/@" + item.Key + "@/", item.Value).Trim();
            }
            if (WriteSqlCommand)
            {
                Log.GetInstance().WriteLine("\r\n" + this._CurrentRequest);
            }
            Comando.CommandText = _CurrentRequest;
            Comando.CommandTimeout = CommandTimeout;
            _EndTime = DateTime.MinValue;
            _RecordsAffected = -1;
            _DatabaseMessage = null;
            _ReturnCode = int.MinValue;
            if (BeforeSendCommand != null)
            {
                BeforeSendCommand(this);
            }
            switch (Connection.State)
            {
                case ConnectionState.Open:

                    break;
                case ConnectionState.Closed:
                    Connection.Open();
                    break;
                default:
                    Connection.Close();
                    Connection.Open();
                    break;
            }
            try
            {
                try
                {
                    try
                    {
                        if (WriteSqlCommand)
                        {
                            Log.GetInstance().WriteLine(DrawLine(79, '=') + "\r\n" + this.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "    SQL ENVIADO");
                        }
                        _RecordsAffected = Comando.ExecuteNonQuery();
                        if (Connection.ConnectionString.Contains("redshift") && !isInTransaction)
                        {
                            Comando.CommandText = "commit";
                            Comando.ExecuteNonQuery();
                        }
                        _EndTime = DateTime.Now;
                        _ReturnCode = 0;
                        _IsRunning = false;
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        _EndTime = DateTime.Now;
                        _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
                        _ReturnCode = (ex.Errors[ex.Errors.Count - 1].NativeError) * -1;
                        _IsRunning = false;
                    }
                }
                catch (System.Data.Odbc.OdbcException ex)
                {
                    _EndTime = DateTime.Now;
                    if (ex.Message.Contains("RedShift ODBC"))
                    {
                        _DatabaseMessage = ex.Message.Substring(ex.Message.LastIndexOf("RedShift ODBC") + 15);
                    }
                    else
                    {
                        _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
                    }
                    if (ex.Errors[ex.Errors.Count - 1].NativeError > 0)
                    {
                        _ReturnCode = (ex.Errors[ex.Errors.Count - 1].NativeError);
                    }
                    else
                    {
                        _ReturnCode = (ex.Errors[ex.Errors.Count - 1].NativeError) * -1;
                    }
                    _IsRunning = false;
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                _EndTime = DateTime.Now;

                _DatabaseMessage = ex.Message.ToUpper().Split(']')[ex.Message.ToUpper().Split(']').Length - 1];
                _ReturnCode = ex.ErrorCode;
                _IsRunning = false;
            }
            if (WriteSqlCommand)
            {
                if (this.RecordsAffected > -1)
                {
                    Log.GetInstance().WriteLine(this.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "    PROCESSO EXECUTADO  REGISTROS PROCESSADOS : " + this.RecordsAffected);
                }
                else
                {
                    Log.GetInstance().WriteLine(this.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "    PROCESSO EXECUTADO");
                }
                if (this.DatabaseMessage != null)
                {
                    Log.GetInstance().WriteLine(this.DatabaseMessage);
                }
                Log.GetInstance().WriteLine("     A DURACAO DO PROCESSO FOI " + DurationString(this.StartTime, this.EndTime) + " RETURN CODE = " + this.ReturnCode + "\r\n" + DrawLine(80, '='));
            }
            if (EndQuery != null)
            {
                EndQuery(this);
            }

            if (this.ReturnCode > 0)
            {

                if (_CheckError)
                {
                    if (EmErro)
                    {
                        return;
                    }
                    EmErro = true;
                    if (OnError != null)
                    {
                        OnError(this.ReturnCode, this.DatabaseMessage);
                    }
                    EmErro = false;
                }

            }
        }
        public event OnErrorEventHandler OnError;
        public delegate void OnErrorEventHandler(int ErrorCode, string ErrorMessage);
        public int RecordsAffected
        {
            get { return this._RecordsAffected; }
        }
        public bool IsRunning
        {
            get
            {
                return this._IsRunning;
            }
        }
        public DateTime StartTime
        {
            get { return this._StartTime; }
        }
        public DateTime EndTime
        {
            get { return this._EndTime; }
        }
        string _CommandText;
        public virtual string CommandText
        {
            get { return this._CommandText; }
            set
            {
                _Tables.Clear();
                if (value.Substring(0, 1) == ".")
                {
                    _QueryName = value.Substring(1);

                    this._CommandText = Util.EmbeddedResource.TextResource(this.DataAssembly, value);
                }
                else
                {
                    this._CommandText = value;
                }
            }
        }
        string _CurrentRequest;
        public string CurrentRequest
        {
            get { return this._CurrentRequest; }
        }
        public virtual int CommandTimeout
        {
            get { return this._CommandTimeout; }
            set
            {
                this._CommandTimeout = value;
                Comando.CommandTimeout = value;
            }
        }
        public int ReturnCode
        {
            get { return _ReturnCode; }
        }
        public string DatabaseMessage
        {
            get { return _DatabaseMessage; }
        }
        Assembly _DataAssembly;
        public virtual Assembly DataAssembly
        {
            get
            {
                if (_DataAssembly == null)
                {
                    _DataAssembly = System.Reflection.Assembly.GetEntryAssembly();
                }
                return _DataAssembly;
            }
            set { _DataAssembly = value; }
        }

        public Dictionary<string, string> Parameters
        {
            get
            {
                return _Parameters;
            }

            set
            {
                _Parameters = value;
            }
        }

        public QueryExecutor(System.Data.Common.DbConnection connection)
            : this(connection, "", 0)
        {
        }
        public QueryExecutor(System.Data.Common.DbConnection connection, string CurrentRequest)
            : this(connection, CurrentRequest, 0)
        {
        }
        public QueryExecutor(System.Data.Common.DbConnection connection, string CurrentRequest, int CommandTimeOut)
        {
            this._Connection = connection;
            Comando = Connection.CreateCommand();
            this._CurrentRequest = CurrentRequest;
            Comando.CommandText = CurrentRequest;
            Comando.CommandTimeout = CommandTimeOut;
            _CommandTimeout = CommandTimeOut;

        }
        private List<string> GetTabelas(string text)
        {
            List<string> Ret = new List<string>();
            string up = text.ToUpper();
            int currentPoint = 0;
            int pointBeginTable = 0;
            bool inTable = false;
            bool inAfterTable = false;
            bool inKeyWord = false;
            do
            {
                int[] array = {
                    up.IndexOf("JOIN", currentPoint),
                    up.IndexOf("FROM", currentPoint),
                    up.IndexOf("INTO", currentPoint),
                    up.IndexOf("UPDATE", currentPoint)
                };
                currentPoint = MenorPositivo(array);
                inKeyWord = true;

                if (currentPoint > -1)
                {
                    do
                    {

                        if (!inKeyWord)
                        {

                            if (inTable)
                            {
                                if (CheckEndWord(text, currentPoint))
                                {
                                    Ret.Add(text.Substring(pointBeginTable, currentPoint + 1 - pointBeginTable).Replace("\"", ""));
                                    inTable = false;
                                    inAfterTable = true;
                                }

                            }
                            else
                            {
                                if (CheckChar(up.Substring(currentPoint, 1)))
                                {
                                    if (inAfterTable)
                                    {
                                        if (up.Substring(currentPoint, 1) == ",")
                                        {
                                            inAfterTable = false;
                                        }
                                        else
                                        {
                                            inAfterTable = false;
                                            break; // TODO: might not be correct. Was : Exit Do
                                        }
                                    }
                                    else if (up.Substring(currentPoint, 1) == "(")
                                    {
                                        break; // TODO: might not be correct. Was : Exit Do
                                    }
                                    else
                                    {
                                        pointBeginTable = currentPoint;
                                        inTable = true;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (!CheckChar(up.Substring(currentPoint, 1)))
                            {
                                inKeyWord = false;
                            }
                        }
                        currentPoint += 1;
                    } while (currentPoint < text.Length);

                }

            } while (currentPoint > -1 & currentPoint < text.Length);
            return Ret;
        }
        public bool CheckChar(string value)
        {
            return !(value == " " || value == "\t" || value == "\r" || value == "\n");
        }
        public bool CheckEndWord(string text, int point)
        {
            if (point == text.Length - 1)
            {
                return true;
            }
            else if (text.Substring(point + 1, 1) == " ")
            {
                return true;
            }
            else if (text.Substring(point + 1, 1) == "\t")
            {
                return true;
            }
            else if (text.Substring(point + 1, 1) == ",")
            {
                return true;
            }
            else if (text.Substring(point + 1, 1) == "\r")
            {
                return true;
            }
            else if (text.Substring(point + 1, 1) == "\n")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public int MenorPositivo(int[] values)
        {
            int m = int.MaxValue;
            foreach (int item in values)
            {
                if (item > -1 && item < m)
                {
                    m = item;
                }
            }
            if (m == int.MaxValue)
            {
                return -1;
            }
            else
            {
                return m;
            }
        }
        public void Close()
        {
            try
            {
                Connection.Close();

            }
            catch (Exception ex)
            {
            }

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

        public List<string> GetSchemasNames()
        {
            var ret = new List<string>();
            var dt = ReturnData("select schema_name from information_schema.schemata");
            foreach (DataRow dr in dt.Rows)
            {
                ret.Add(dr[0].ToString());

            }
            return ret;

        }
        static void DataTableToCsv(DataTable dt, string csvFileName)
        {
            using (var sw = new System.IO.StreamWriter(csvFileName, true, Encoding.Default))
            {
                bool isFirst = true;
                StringBuilder sb = new StringBuilder();
                foreach (var c in dt.Columns)
                {
                    if (!isFirst)
                    {
                        sb.Append(",");
                    }
                    sb.Append(c.ToString().Replace(",", ";").Replace("\r", " ").Replace("\n", " ").Replace("\t", " "));
                    isFirst = false;
                }
                sw.WriteLine(sb.ToString());
                sb.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    isFirst = true;
                    foreach (var c in r.ItemArray)
                    {

                        if (!isFirst)
                        {
                            sb.Append("\t");
                        }
                        sb.Append(c.ToString().Replace("\t", " ").Replace("\r", " ").Replace("\n", " "));
                        isFirst = false;
                    }
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }
                sw.Close();
                sw.Dispose();
            }
        }

    }
}



