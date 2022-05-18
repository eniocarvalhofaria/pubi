using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using BusinessIntelligence.Util;
using Amazon.S3;
using Amazon.S3.Model;
using BusinessIntelligence.Authentication;
namespace BusinessIntelligence.Data.Redshift
{
    public class RedshiftLoader : DatabaseLoader
    {
        public RedshiftLoader(string s3AccessKey, string s3SecretKey, string userId, string password, System.Data.Common.DbConnection connection, string schemaName, string tableName)
            : base(connection, schemaName, tableName)
        {
            this.S3AccessKey = s3AccessKey;
            this.S3SecretKey = s3SecretKey;
            this.UserId = userId;
            this.Password = password;
        }
        public RedshiftLoader(string schemaName, string tableName)
         : base(Connections.GetNewConnection("REDSHIFT"), schemaName, tableName)
        {
            if (BusinessIntelligence.Util.EnvironmentParameters.S3AccessKey == null)
            {
                if (EnvironmentCredentials.AWS == null)
                {
                    var c = EnvironmentCredentials.GetNewCredential("AWS");
                    if (c == null)
                    {
                        throw new Exception("Credencial do AWS não informada.");
                    }
                    var client = new AmazonS3Client(c.Username, c.Password, Amazon.RegionEndpoint.USEast1);
                    c.Save();
                }
                this.S3AccessKey = EnvironmentCredentials.AWS.Username;
                this.S3SecretKey = EnvironmentCredentials.AWS.Password;
            }
            else
            {
                this.S3AccessKey = BusinessIntelligence.Util.EnvironmentParameters.S3AccessKey;
                this.S3SecretKey = BusinessIntelligence.Util.EnvironmentParameters.S3SecretKey;
            }
            /*
            Result testS3Connectivity = this.putFileToS3(@"pu-redshift-workarea/input/subject=adhoc/data/object=test/", "Test.txt");

            if (testS3Connectivity.Sucess)
            {
              
            }
            else
            {
                throw new Exception(testS3Connectivity.Message);
            }
    */
        }
        public RedshiftLoader(System.Data.Common.DbConnection connection, string schemaName, string tableName)
            : base(connection, schemaName, tableName)
        {
            if (BusinessIntelligence.Util.EnvironmentParameters.S3AccessKey == null)
            {
                if (EnvironmentCredentials.AWS == null)
                {
                    var c = EnvironmentCredentials.GetNewCredential("AWS"); ;
                    var client = new AmazonS3Client(c.Username, c.Password, Amazon.RegionEndpoint.USEast1);
                    c.Save();
                }
                this.S3AccessKey = EnvironmentCredentials.AWS.Username;
                this.S3SecretKey = EnvironmentCredentials.AWS.Password;
            }
            else {
                this.S3AccessKey = BusinessIntelligence.Util.EnvironmentParameters.S3AccessKey;
                this.S3SecretKey = BusinessIntelligence.Util.EnvironmentParameters.S3SecretKey;
            }/*
            Result testS3Connectivity = this.putFileToS3(@"pu-redshift-workarea/input/subject=adhoc/data/object=test/", "Test.txt");
            if (testS3Connectivity.Sucess)
            {
              
            }
            else
            {
                throw new Exception(testS3Connectivity.Message);
            }
            */
            this.UserId = EnvironmentParameters.RedshiftUserId;
            this.Password = EnvironmentParameters.RedshiftPwd;
        }
        private string S3AppPath
        {
            get
            {
                return GetExecutingDirectory + "\\" + "s3.exe";
            }
        }
        private string GetExecutingDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetAssembly(typeof(RedshiftLoader)).CodeBase).Replace("file:\\", "");
            }
        }
        private string Bucket { get { return @"pu-redshift-workarea/input/subject=adhoc/data/object=" + TableName + "/"; } }
        private string S3AccessKey { get; set; }
        private string S3SecretKey { get; set; }
        private string CsvDirectory
        {
            get
            {
                return System.IO.Path.GetTempPath();
            }
        }
        private string UserId { get; set; }
        private string Password { get; set; }
        private DataTable schemaTable = null;
        public override DataTable GetNewDataTable()
        {
            var e = new QueryExecutor(Connection);
            return e.ReturnData(string.Format("select top 0 * from \"{0}\".\"{1}\"", SchemaName, TableName), false);

        }
      
        Result putFileToS3(string bucket, string fileName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Putting " + fileName + " in s3.");
            using (var client = new AmazonS3Client(this.S3AccessKey, this.S3SecretKey, Amazon.RegionEndpoint.USEast1))
            {
                string objectName = new FileInfo(fileName).Name;
                if (bucket.EndsWith("/"))
                {
                    bucket = bucket.Substring(0, bucket.Length - 1);
                }
                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = bucket,
                    Key = objectName,
                    FilePath = fileName
                };
                try
                {
                    var p = client.PutObject(putObjectRequest);

                    return new Result(true);
                }
                catch (AmazonS3Exception s3Exception)
                {
                    Console.WriteLine(s3Exception.Message,
                                      s3Exception.InnerException);
                    return new Result(false, s3Exception.Message);
                }
            }


        }

        Result deleteFileFromS3(string bucket, string fileName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Deleting " + fileName + " in s3.");
            using (var client = new AmazonS3Client(this.S3AccessKey, this.S3SecretKey, Amazon.RegionEndpoint.USEast1))
            {
                string objectName = new FileInfo(fileName).Name;
                if (bucket.EndsWith("/"))
                {
                    bucket = bucket.Substring(0, bucket.Length - 1);
                }
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucket,
                    Key = objectName,
                };
                try
                {
                    var p = client.DeleteObject(deleteObjectRequest);

                    return new Result(true);
                }
                catch (AmazonS3Exception s3Exception)
                {
                    Console.WriteLine(s3Exception.Message,
                                      s3Exception.InnerException);
                    return new Result(false, s3Exception.Message);
                }
            }


        }

        private bool commandToS3(string command)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = "cmd";
            p.Start();
            p.StandardInput.WriteLine(S3AppPath + " " + " auth " + S3AccessKey + " " + S3SecretKey + "");
            p.StandardInput.WriteLine(S3AppPath + " " + command);
            p.StandardInput.WriteLine("exit %errorlevel%");
            var sb = new StringBuilder();
            while (!p.HasExited)
            {
                sb.Append(p.StandardOutput.ReadToEnd());
            }
            p.WaitForExit();
            if (p.ExitCode == 0)
            {
                return true;
            }
            else
            {
                Console.Write(sb.ToString());
                return false;
            }

        }
        private bool deleteFromS3(string csvFileName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Deleting " + csvFileName + " from s3.");
            //    var ret = commandToS3(" delete " + Bucket + " " + csvFileName);
            var ret = deleteFileFromS3(csvFileName);
            return ret;
        }
        private bool moveToS3(string csvFileName)
        {
            putFileToS3(Bucket, csvFileName);
            var ret = true;
            if (ret)
            {
                File.Delete(csvFileName);

            }
            return ret;

        }
        bool deleteFileFromS3(string fileName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Deleting " + fileName + " from s3.");
            using (var client = new AmazonS3Client(this.S3AccessKey, this.S3SecretKey, Amazon.RegionEndpoint.USEast1))
            {
                DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = Bucket,
                    Key = fileName
                };
                try
                {
                    client.DeleteObject(deleteObjectRequest);
                    return true; 
                }
                catch (AmazonS3Exception s3Exception)
                {
                    Console.WriteLine(s3Exception.Message,
                                      s3Exception.InnerException);
                    return false;
                }
            }


        }
 
        private bool bulkLoadToRedShift(string csvFileName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Loading " + csvFileName + " in Redshift.");

            var cmd = Connection.CreateCommand();
            string copyCommand = "COPY " + SchemaName + "." + TableName + " FROM 's3://" + Bucket + csvFileName + "' ";

            copyCommand += " CREDENTIALS 'aws_access_key_id=" + S3AccessKey + ";aws_secret_access_key=" + S3SecretKey + "'";

            copyCommand += " delimiter '\\t' removequotes";
            try
            {
                try
                {
                    cmd.CommandText = copyCommand;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + csvFileName + "loaded in Redshift.");
                    deleteFileFromS3(csvFileName);
                    return true;
                }
                catch (System.Data.Common.DbException exc)
                {
                    ErrorMessage = exc.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance().Write(ex);
                return false;
            }
        }
        private void DataTableToCsv(DataTable dt, string csvFileName, bool hasColumnNames, DataTable SchemaTableDefinition)
        {
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            using (var sw = new System.IO.StreamWriter(CsvDirectory + "\\" + csvFileName, true, utf8WithoutBom))
            {
                bool isFirst = true;
                StringBuilder sb = new StringBuilder();
                if (hasColumnNames)
                {
                    foreach (var c in dt.Columns)
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

                int linha = 0;
                foreach (DataRow r in dt.Rows)
                {
                    linha++;
                    isFirst = true;
                    int columnIndex = 0;
                    foreach (var c in r.ItemArray)
                    {

                        if (!isFirst)
                        {
                            sb.Append("\t");
                        }
                        if (dt.Columns[columnIndex].DataType.ToString() == "System.DateTime" && c != DBNull.Value)
                        {
                            if (SchemaTableDefinition.Rows[columnIndex]["datatypeName"].ToString() == "date")
                            {
                                sb.Append(((DateTime)c).ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                sb.Append(((DateTime)c).ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else if (c.ToString() == "True" || c.ToString() == "False")
                        {
                            sb.Append(c.ToString().Replace("True", "1").Replace("False", "0"));
                        }
                        else
                        {
                            sb.Append(c.ToString().Replace("\t", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", ""));
                        }

                        isFirst = false;
                        columnIndex++;
                    }
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }
                sw.Close();
                sw.Dispose();
            }
        }
        private bool loadToRedShift(string csvFileName)
        {
            FileInfo fi = new FileInfo(CsvDirectory + "\\" + csvFileName);
            if (fi.Length > 0)
            {
                if (!moveToS3(CsvDirectory + csvFileName))
                {
                    return false;
                }
                return bulkLoadToRedShift(csvFileName);
            }
            return true;
        } 
        public override bool Load(DataTable dt)
        {
            if (!TestIfTableExist())
            {
                CreateTable(dt);
            }
            string csvFileName = SchemaName + "." + TableName + ".csv";
            DataTableToCsv(dt, csvFileName, false, GetSchemaTable());
            return loadToRedShift(csvFileName);
        }

    }
}
