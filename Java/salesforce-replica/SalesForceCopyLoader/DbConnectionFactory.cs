using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
namespace SalesForceCopyLoader
{
  public abstract  class DbConnectionFactory
    {
      private string _ConnectionString;
      public string ConnectionString
      {
          get { return _ConnectionString; }
          set { _ConnectionString = value; }
      }
    protected  abstract DbConnection GetNewConnection();
      private int _LimitSessions = 10;
      public int LimitSessions
      {
          get { return _LimitSessions; }
          set { _LimitSessions = value; }
      }
      private List<DbConnection> allConnections = new List<DbConnection>();
      private List<DbConnection> releseadConnections = new List<DbConnection>();
      public bool CloseOnRelease { get; set; }
      private bool gettingConnection = false;
      public DbConnection GetConnection()
      {
          if (gettingConnection)
          {
              System.Threading.Thread.Sleep(1000);
              return GetConnection();
          }
          gettingConnection = true;
          if (releseadConnections.Count > 0)
          {
              var toProvide = releseadConnections[0];
              releseadConnections.Remove(toProvide);
              gettingConnection = false;
              return toProvide;
          }
          else if (allConnections.Count < LimitSessions)
          {
              var toProvide = GetNewConnection();
              allConnections.Add(toProvide);
              Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + allConnections.Count.ToString() + " total connections.");
              gettingConnection = false;
              return toProvide;
          }
          else {
              gettingConnection = false;
              System.Threading.Thread.Sleep(1000);
              return GetConnection();
          }
      
      }
      public void ReleaseConnection(DbConnection connection) {
          if (CloseOnRelease)
          {
              connection.Close();
          }
          releseadConnections.Add(connection);
          Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + allConnections.Count.ToString() + " total connections.");
          Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + releseadConnections.Count.ToString() + " connections free.");
             
      }
    }
}
