using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
namespace SalesForceCopyLoader
{
  public  class SqlServerLoader: Loader
    {
      public int BatchSize { get; set; }
      public override string GetStageTableName(string fileName)
      {
          fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);
          string[] parts = fileName.Split('.');

          if (parts.Length >= 2)
          {
              return "[" + parts[0] + "stage].[" + parts[1] + "]";;
          }

          return "";
      }
      public override string GetTableName(string fileName)
      {
          fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);
          string[] parts = fileName.Split('.');

          if (parts.Length >= 2)
          {
              return "[" + parts[0] + "].[" + parts[1] + "]";
          }

          return "";
      }
    public  override void EndOfFileRead()
    {
    
    }
 
      public override void LoadErrors(DataTable errorDataTable)
      {
          if (errorDataTable != null && errorDataTable.Rows.Count > 0)
          {
              string errorTableName = (StageTableName + "_error]").Replace("]_", "_");
              StringBuilder createText = new StringBuilder();
              createText.Append(string.Format("IF object_id(N'{0}') IS NOT NULL \r\n", errorTableName));
              createText.Append("begin\r\n");
              createText.Append(string.Format("drop table {0} \r\n", errorTableName));
              createText.Append("end\r\n");
              createText.Append(string.Format("create table {0} \r\n", errorTableName));
              createText.Append(string.Format("( Id varchar(100) \r\n"));
              createText.Append(string.Format(", ColumnName varchar(100) \r\n"));
              createText.Append(string.Format(", Content varchar(8000) \r\n"));
              createText.Append(string.Format(", ErrorMessage varchar(8000) \r\n"));
              createText.Append(")");
              var cmd = Connection.CreateCommand();
              cmd.CommandTimeout = 0;
              cmd.CommandText = createText.ToString();
              cmd.ExecuteNonQuery();
              using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)Connection))
              {

                  bc.DestinationTableName = errorTableName;
                  bc.BatchSize = BatchSize;
                  bc.BulkCopyTimeout = 0;
                  bc.WriteToServer(errorDataTable);
              }
              Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Error content loaded in " + errorTableName + ".");

          }
      }
      int rw_cnt = 0;
      bool errorInLoad;
      public override void ConsumeData(DataTable dt)
      {

          try
          {


                  using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)Connection))
                  {

                      bc.DestinationTableName = GetStageTableName(FileInfo.Name);
                      bc.BatchSize = BatchSize;
                      bc.BulkCopyTimeout = 0;
                      Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                      bc.WriteToServer(dt);
                  }
              rw_cnt += dt.Rows.Count;
          }
          catch (Exception e)
          {
              errorInLoad = true;
              Console.WriteLine(" ({0}) ", e.Message);
              StringBuilder sb = new StringBuilder();
              foreach (DataRow r in dt.Rows)
              {
                  for (int i = 0; i < dt.Columns.Count; i++)
                  {
                      if (r[i] != DBNull.Value)
                      {
                          sb.Append(string.Format("{0} = {1}", dt.Columns[i].ColumnName, r[i].ToString()) + "\r\n");
                      }
                  }

              }
              //                   Console.WriteLine(sb.ToString());
              Console.WriteLine(e.StackTrace);
              Environment.Exit(1);

          }
      }
    }
}
