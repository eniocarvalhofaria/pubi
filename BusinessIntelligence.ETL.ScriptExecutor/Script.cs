using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessIntelligence.Data;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.ETL
{
    public class Script : IScriptCommand
    {
        public Script(QueryExecutor ex, string text)
        {
            this.QueryExecutor = ex;
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("script nulo");
            }
            else
            {
                _Text = Format(text);
                _Text = RemoveLineComments(RemoveComments(_Text));
                var commands = SplitScript(_Text);
                foreach (var item in commands)
                {
                    var c = CreateCommand(item);
                    if (c != null)
                    {
                        _Statements.Add(c);
                    }
                }
            }
        }
        private static bool _LogEnabled = true;

        public static bool LogEnabled
        {
            get { return _LogEnabled; }
            set { _LogEnabled = value; }
        }


        IScriptCommand CreateCommand(CommandStructure value)
        {
            switch (value.Keyword)
            {
                case null:
                    {
                        return new SqlCommand(this, value.SqlCommand, value.QueryExecutor);

                    }
                case "try":
                    {
                        return new TryCommand(this, value.InnerScript);
                    }
                case "catch":
                    {
                        if (_Statements[_Statements.Count - 1] is TryCommand)
                        {
                            ((TryCommand)(_Statements[_Statements.Count - 1])).CatchScript = new Script(value.QueryExecutor, value.InnerScript);
                        }
                        return null;
                    }
                case "if":
                    {
                        return new IfCommand(this, value.Expression, value.InnerScript, value.QueryExecutor);

                    }
                case "else":
                    {
                        if (_Statements[_Statements.Count - 1] is IfCommand)
                        {
                            ((IfCommand)(_Statements[_Statements.Count - 1])).ElseScript = new Script(value.QueryExecutor, value.InnerScript);
                        }
                        return null;
                    }
                case "cursor":
                    {
                        return new Cursor(this, value.Name, value.SqlCommand, value.InnerScript);

                    }
                case "set":
                    {
                        return new SetVariables(this, value.SqlCommand, value.QueryExecutor);

                    }
                case "print":
                    {
                        return new PrintCommand(this, value.Expression);

                    }
                case "while":
                    {
                        return new WhileCommand(this, value.Expression, value.InnerScript, value.QueryExecutor);

                    }
                case "break":
                    {
                        return new BreakCommand();

                    }
                case "return":
                    {
                        return new ReturnCommand(this, value.Expression);

                    }
                case "showlog":
                    {
                        return new SetLog(true);

                    }
                case "hidelog":
                    {
                        return new SetLog(false);

                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public bool TestLogicalExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return true;
            }
            else
            {
                var a = "select case when ";
                var b = " then 1 else 0 end";
                string CommandText = a + expression + b;
           
                DataTable dt = this.QueryExecutor.ReturnData(CommandText, false);
                var c = this.QueryExecutor.CurrentRequest;
                var exp = c.Substring(a.Length, c.Length - (b.Length + a.Length));
                var ToLog = "A EXPRESSÃO " + exp + " RETORNOU ";
            
                ReturnCode = QueryExecutor.ReturnCode;
                if (QueryExecutor.ReturnCode != 0)
                {
                    Log.GetInstance().WriteLine(ToLog + "ERRO");
                    if (this.IsInTry)
                    {
                    
                        this.HasExecutionError = true;
                        QueryExecutor.AddTextParameter("errorcode", QueryExecutor.ReturnCode.ToString());
                        QueryExecutor.AddTextParameter("errormessage", QueryExecutor.DatabaseMessage.ToString());
                    }
                    else
                    {
                        Environment.Exit(QueryExecutor.ReturnCode);
                    }

                }
                if ((int)(dt.Rows[0][0]) == 1)
                {
                    Log.GetInstance().WriteLine(ToLog + "VERDADEIRO" );
                    return true;
                }
                else
                {
                    Log.GetInstance().WriteLine(ToLog + "FALSO");
                    return false;
                }
            }


        }
        public string GetExpression(string expression)
        {

            string CommandText = "select " + expression;

            DataTable dt = this.QueryExecutor.ReturnData(CommandText, false);
            return dt.Rows[0][0].ToString();
        }

        string Trim(string text)
        {
            string oldText = null;

            while (oldText != text && text.Length > 0)
            {
                oldText = text;
                if ("\r\n\t ".IndexOf(text.Substring(0, 1)) > -1)
                {
                    text = text.Substring(1);
                }
            }

            return text;
        }
        int MinIndex(int value1, int value2)
        {
            if (value1 > value2 && value2 > -1)
            {
                return value2;
            }
            else
            { return value1; }
        }
        int MinIndex(int[] values)
        {
            int min = int.MaxValue;
            if (values.Length > 1)
            {

                foreach (var i in values)
                {
                    if (i < min && i > -1)
                    {
                        min = i;
                    }
                }
            }
            if (min == int.MaxValue)
            {
                min = -1;
            }
            return min;
        }
        string FirstWord(string text)
        {
            int iTab = text.IndexOf("\t");
            int iSpace = text.IndexOf(" ");
            int iCr = text.IndexOf("\r");
            int iLf = text.IndexOf("\n");
            int iEndWord = MinIndex(new int[] { iTab, iSpace, iCr, iLf });
            if (iEndWord == -1)
            {
                return text;
            }
            else
            {
                return text.Substring(0, iEndWord);
            }

        }
        CommandStructure SplitCommand(string text)
        {
            CommandStructure ret = new CommandStructure();

            text = Trim(text);

            string[] keywords = new string[] { "if", "cursor", "set", "while", "break", "return", "print", "else", "showlog", "hidelog", "try", "catch" };
            string[] keywordsNamedCommand = new string[] { "cursor" };
            string[] keywordsExpressionCommand = new string[] { "if", "while", "print" ,"return"};
            string[] keywordsBlockCommand = new string[] { "if", "cursor", "while", "else", "try", "catch" };

            if (text.ToLower().IndexOf("connection") == 0)
            {
                text = Trim(text.Substring("connection".Length));
                var connectionName = GetFirstWord(text);
                var qex = ScriptExecutor.GetQueryExecutor(connectionName.ToUpper());
                ret.QueryExecutor = qex;
                text = Trim(text.Substring(connectionName.Length));
            }
            else
            {
                ret.QueryExecutor = QueryExecutor;
            }

            foreach (var k in keywords)
            {
                if (text.ToLower().IndexOf(k) == 0)
                {
                    ret.Keyword = k;
                    text = Trim(text.Substring(k.Length));
                    break;
                }
            }

          
            if (ret.Keyword == null)
            {
                ret.SqlCommand = text;
            }
            else
            {
                foreach (var k in keywordsNamedCommand)
                {
                    if (ret.Keyword == k)
                    {
                        ret.Name = FirstWord(text);
                        text = Trim(text.Substring(ret.Name.Length));
                    }
                }
                foreach (var k in keywordsBlockCommand)
                {
                    if (ret.Keyword == k)
                    {
                        if (text.IndexOf("{") > -1)
                        {
                            ret.InnerScript = text.Substring(text.IndexOf("{") + 1, (text.Length - text.IndexOf("{")) - 2);
                            text = text.Substring(0, text.IndexOf("{"));
                        }
                    }
                }


                if (text.ToLower().IndexOf("select") > -1)
                {
                    ret.SqlCommand = text.Substring(text.IndexOf("select"), (text.Length - text.IndexOf("select")));
                    text = text.Substring(0, text.IndexOf("select"));
                }

                foreach (var k in keywordsExpressionCommand)
                {
                    if (ret.Keyword == k)
                    {
                        int openParent = text.IndexOf("(");
                        if (openParent > -1)
                        {
                            int closeParent = CloseIndex(openParent, text);
                            ret.Expression = text.Substring(openParent, (closeParent - openParent) + 1);
                        }
                    }
                }

            }

            return ret;
        }


        string GetFirstWord(string text)
        {
            string[] delimiters = new string[] { " ", "\t", "\r", "\n" };

            return text.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0];


        }
        bool _Returned;

        public bool Returned
        {
            get { return _Returned; }

        }
        bool _Breaked;

        public bool Breaked
        {
            get { return _Breaked; }
        }
        string Format(string text)
        {
            string ret = text;

            ret = ret.Replace(";", ";\r\n");
            ret = ret.Replace("}", "}\r\n");
            ret = Trim(ret);
            string oldText = null;
            while (oldText != ret)
            {
                oldText = ret;
                ret = ret.Replace("\r\n\r\n", "\r\n");
                ret = ret.Replace(";\r\n\t", ";\r\n");
                ret = ret.Replace(";\r\n ", ";\r\n");
                ret = ret.Replace("}\r\n\t", "}\r\n");
                ret = ret.Replace("}\r\n ", "}\r\n");
                ret = ret.Replace("\r\n\r\n", "\r\n");
            }
            ret = RemoveLineComments(RemoveComments(ret));
            ret = ret.Replace(";\r\n", ";");

            ret = VariablesToLower(ret);
            return ret;
        }
        private string VariablesToLower(string text)
        {
            string ret = text;
            int currentIndexOpen = -1;
            int currentIndexClose = 0;
            while (true)
            {
                currentIndexOpen = ret.IndexOf("<@", currentIndexOpen + 1);
                if (currentIndexOpen == -1)
                {
                    break;
                }
                currentIndexClose = ret.IndexOf("@>", currentIndexOpen);
                ret = ret.Substring(0, currentIndexOpen) + ret.Substring(currentIndexOpen, currentIndexClose - currentIndexOpen).ToLower() + ret.Substring(currentIndexClose);

            }
            return ret;
        }
        private int CloseIndex(int openBlockIndex, string text)
        {
            string openBlockChar;
            string closeBlockChar;
            if (text.Substring(openBlockIndex, 1) == "(")
            {
                openBlockChar = "(";
                closeBlockChar = ")";
            }
            else if (text.Substring(openBlockIndex, 1) == "{")
            {
                openBlockChar = "{";
                closeBlockChar = "}";
            }
            else if (text.Substring(openBlockIndex, 1) == "[")
            {
                openBlockChar = "[";
                closeBlockChar = "]";
            }
            else { return -1; }
            int openBracketIndex = 0;
            int closeBracketIndex = 0;

            int closing = 1;
            int scriptPoint = openBlockIndex;
            while (closing > 0)
            {
                openBracketIndex = text.IndexOf(openBlockChar, scriptPoint + 1);
                closeBracketIndex = text.IndexOf(closeBlockChar, scriptPoint + 1);
                if (openBracketIndex > -1 && openBracketIndex < closeBracketIndex)
                {
                    scriptPoint = openBracketIndex;
                    closing++;
                }
                else
                {
                    scriptPoint = closeBracketIndex;
                    closing--;
                }

            }
            return closeBracketIndex;
        }
        private CommandStructure[] SplitScript(string _text)
        {
            List<string> _Text2 = new List<string>();

            int lastCharacterIndex = 0;

            List<int[]> texts = new List<int[]>();
            while (true)
            {
                int openTextIndex = _Text.IndexOf("'", lastCharacterIndex);
                int closeTextIndex = -1;
                if (openTextIndex > -1)
                {
                    closeTextIndex = _Text.IndexOf("'", openTextIndex + 1);
                    texts.Add(new int[] { openTextIndex, closeTextIndex });
                    lastCharacterIndex = closeTextIndex + 1;
                }
                else { break; }

            }

            lastCharacterIndex = 0;
            while (true)
            {
                int globalLastCharacterIndex = lastCharacterIndex;
                int semicolonIndex = _Text.IndexOf(";", lastCharacterIndex);
                foreach (var item in texts)
                {
                    semicolonIndex = _Text.IndexOf(";", lastCharacterIndex);
                    if (semicolonIndex < item[0])
                    {
                        break;
                    }
                    else if ((semicolonIndex > item[0] && semicolonIndex < item[1]))
                    {
                        semicolonIndex = -1;
                        lastCharacterIndex = item[1] + 1;
                        break;
                    }
                }

                lastCharacterIndex = globalLastCharacterIndex;
                int bracketIndex = _Text.IndexOf("{", lastCharacterIndex);

                foreach (var item in texts)
                {
                    bracketIndex = _Text.IndexOf("{", lastCharacterIndex);
                    if (bracketIndex < item[0])
                    {
                        break;
                    }
                    else if ((bracketIndex > item[0] && bracketIndex < item[1]))
                    {
                        bracketIndex = -1;
                        lastCharacterIndex = item[1] + 1;
                        break;
                    }
                }
                lastCharacterIndex = globalLastCharacterIndex;


                if (bracketIndex == -1 && semicolonIndex == -1)
                {
                    _Text2.Add(_Text.Substring(lastCharacterIndex));
                    break;
                }
                else if (bracketIndex == -1 || (semicolonIndex > -1 && semicolonIndex < bracketIndex))
                {
                    _Text2.Add(_Text.Substring(lastCharacterIndex, semicolonIndex - lastCharacterIndex));
                    lastCharacterIndex = semicolonIndex + 1;
                }
                else
                {
                    int closeBracketIndex = CloseIndex(bracketIndex, _Text);
                    if (closeBracketIndex > -1)
                    {
                        _Text2.Add(_Text.Substring(lastCharacterIndex, (closeBracketIndex - lastCharacterIndex) + 1));
                        lastCharacterIndex = closeBracketIndex + 1;
                    }
                }
            }
            var ret = new List<CommandStructure>();
            foreach (var t in _Text2)
            {
                if (!string.IsNullOrEmpty(Trim(t)))
                {
                    ret.Add(SplitCommand(t));
                }
            }
            return ret.ToArray();
        }
        private List<IScriptCommand> _Statements = new List<IScriptCommand>();
        public IScriptCommand[] Statements
        {
            get
            {
                return _Statements.ToArray();
            }
        }
        private string[] _Text2;
        static public string RemoveEmptyLine(string text)
        {
            if (text.IndexOf("\r\n\r\n") > 0)
            {

                return (RemoveEmptyLine(text.Replace("\r\n\r\n", "\r\n")));
            }
            else
            {
                return text;
            }
        }
        static public string RemoveComments(string text)
        {
            int C0 = text.IndexOf("/*");
            int C1 = 0;
            if (C0 > -1)
            {
                C1 = text.IndexOf("*/", C0);
                string newText = text.Substring(0, C0) + text.Substring(C1 + 2);
                return RemoveComments(newText);
            }
            else
            {
                return text;
            }
        }
        static public string RemoveLineComments(string text)
        {
            int C0;
            /*
            if (text.IndexOf("--") == 0)
            {
                C0 = 0;
            }
            else
            {
                C0 = text.IndexOf("\r\n--");
                if (C0 > -1)
                {
                    C0 += 2;
                }
            }
            */
            C0 = text.IndexOf("--");
            int C1 = 0;
            if (C0 > -1)
            {
                C1 = text.IndexOf("\r\n", C0);
                string newText = text.Substring(0, C0) + text.Substring(C1 + 2);
                return RemoveLineComments(newText);
            }
            else
            {
                return text;
            }
        }
        private string _Text;

        public string Text
        {
            get { return _Text; }
        }

        private BusinessIntelligence.Data.QueryExecutor _QueryExecutor;
        public BusinessIntelligence.Data.QueryExecutor QueryExecutor
        {
            get { return _QueryExecutor; }
            set { _QueryExecutor = value; }
        }

        public void SetVariables(DataRow dataRow, string named)
        {
            if (!string.IsNullOrEmpty(named))
            {

                named = named + ".";
            }
            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                if (dataRow[i] == DBNull.Value)
                {
                    AddParameter(named + dataRow.Table.Columns[i].ColumnName, "null");

                }
                else
                {
                    switch (dataRow.Table.Columns[i].DataType.Name)
                    {
                        case "Int32":
                        case "Int16":
                        case "Int64":
                        case "Long":
                        case "Double":
                        case "Decimal":

                            AddParameter(named + dataRow.Table.Columns[i].ColumnName, dataRow[i].ToString().Replace(",", "."));
                            break;
                        case "DateTime":
                            {
                                AddParameter(named + dataRow.Table.Columns[i].ColumnName, "'" + ((DateTime)dataRow[i]).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                                break;
                            }
                        default:
                            {
                                AddParameter(named + dataRow.Table.Columns[i].ColumnName, "'" + dataRow[i].ToString().Replace("'", "''") + "'");
                                AddParameter("@" + named + dataRow.Table.Columns[i].ColumnName, dataRow[i].ToString());
                                break;
                            }
                    }
                }
            }

        }
        public static void AddParameter(string parameterName, string parameterValue)
        {
            Log.GetInstance().WriteLine("ADICIONANDO VARIÁVEL: NOME: <@" + parameterName.ToLower() + "@>" + " CONTEÚDO: " + parameterValue);

            foreach (var qex in ScriptExecutor.QueryExecutorList.Values)
            {
                qex.AddTextParameter(parameterName.ToLower(), parameterValue);
            }

        }
        public int ReturnCode { get; set; }
        private bool _HasExecutionError = false;
        public bool HasExecutionError
        {
            get { return _HasExecutionError; }
            set { _HasExecutionError = value; }
        }
        private bool _IsInTry = false;
        public bool IsInTry
        {
            get { return _IsInTry; }
            set { _IsInTry = value; }
        }
        public void Execute()
        {

            for (int i = 0; i < Statements.Length; i++)
            {
                Statements[i].IsInTry = this.IsInTry;
                _Statements[i].Execute();
                if (this.IsInTry && _Statements[i].HasExecutionError)
                {
                    this.HasExecutionError = true;
                    return;
                }
                if (_Statements[i] is BreakCommand)
                {
                    _Breaked = true;
                    return;
                }
                if (_Statements[i] is ReturnCommand)
                {
                    
                    _Returned = true;
                    return;
                }
                if (_Statements[i] is IBlockCommand)
                {
                    if (((IBlockCommand)_Statements[i]).Returned)
                    {
                        _Returned = true;
                        return;
                    }
                }


            }

        }

    }
}
