using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Data
{
    public  class DataTransfer
    {
        public DataTransfer(System.Data.Common.DbConnection sourceConnection,string readCommand, DatabaseLoader loader)
        {
            this.Loader = loader;
            this.SourceConnection = sourceConnection;
            this.ReadCommand = readCommand;
        }
        private string _ReadCommand;

        public string ReadCommand
        {
            get { return _ReadCommand; }
            set { _ReadCommand = value; }
        }
        private DatabaseLoader _Loader;
        public DatabaseLoader Loader
        {
            get { return _Loader; }
            set { _Loader = value; }
        }
        private System.Data.Common.DbConnection _SourceConnection;
        public System.Data.Common.DbConnection SourceConnection
        {
            get { return _SourceConnection; }
            set { _SourceConnection = value; }
        }
     
        public void Execute(bool clearTable)
        {
            QueryExecutor qex = new QueryExecutor(SourceConnection);
            var dt = Loader.GetNewDataTable();
            DataTable readed = qex.ReturnData(ReadCommand);

            if (qex.ReturnCode > 0)
            {
                throw new Exception(qex.DatabaseMessage);
            }
            else if (readed == null)
            {

                throw new Exception("Erro ao obter os dados");
            }
            else if (readed.Rows.Count == 0)
            {

                throw new Exception("A consulta não retornou dados");
            }


            var tableColumnsIndex = new Dictionary<string, int>();
            var mapping = new List<Mapping>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tableColumnsIndex.Add(dt.Columns[i].ColumnName.ToLower(), i);
            }

            for (int i = 0; i < readed.Columns.Count; i++)
            {
                int indexFinded;
                if (tableColumnsIndex.TryGetValue(readed.Columns[i].ColumnName.ToLower(), out indexFinded))
                {
                    var a = new Mapping();
                    a.SourceColumnIndex = i;
                    a.DestColumnIndex = indexFinded;
                    mapping.Add(a);
                }
            }
            foreach (DataRow r in readed.Rows)
            {
                DataRow nr = dt.NewRow();
                foreach (var m in mapping)
                {
                    nr[m.DestColumnIndex] = r[m.SourceColumnIndex];
                }
                dt.Rows.Add(nr);
            }
            if (clearTable)
            {
                Loader.ClearDestinationTable();
            }
            Loader.Load(dt);
        }
        private struct Mapping
        {
          public  int SourceColumnIndex;
          public int DestColumnIndex;
        }
    }
}
