using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Amazon.S3;
using Amazon.S3.Model;
using BusinessIntelligence.Util;
namespace SalesForceCopyLoader
{
    public class RedshiftLoader : Loader
    {
    //    public string S3AppPath { get; set; }
        public string S3AccessKey { get; set; }
        public string S3SecretKey { get; set; }
        public string CsvDirectory { get; set; }
        //    public string RedshiftLoaderPath { get; set; }
        //    public string JavaPath { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public override void LoadErrors(DataTable errorDataTable)
        {
            //throw new NotImplementedException();
        }
        public override string GetStageTableName(string fileName)
        {
            fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);
            string[] parts = fileName.Split('.');

            if (parts.Length >= 2)
            {
                return "salesforce." + parts[0] + "stage" + parts[1];
            }

            return "";
        }
        public override string GetTableName(string fileName)
        {
            fileName = fileName.Substring(fileName.LastIndexOf('/') + 1);
            string[] parts = fileName.Split('.');

            if (parts.Length >= 2)
            {
                return "salesforce." + parts[0] + parts[1];
            }

            return "";
        }



 
        private bool moveToS3(string Bucket,string csvFileName)
        {
            putFileToS3(Bucket, csvFileName);
            var ret = true;
            if (ret)
            {
                File.Delete(csvFileName);

            }
            return ret;

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
        bool deleteFileFromS3(string Bucket,string fileName)
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
        bool bulkLoadToRedShift(string bucket, string csvFileName, string tableName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Loading " + csvFileName + " in Redshift.");
            var cmd = Connection.CreateCommand();
            string copyCommand = "COPY " + tableName + " FROM 's3://" + bucket + csvFileName + "' ";

            copyCommand += " CREDENTIALS 'aws_access_key_id=" + S3AccessKey + ";aws_secret_access_key=" + S3SecretKey + "'";

            copyCommand += " delimiter ',' removequotes";
            try
            {
                cmd.CommandText = copyCommand;
                cmd.ExecuteNonQuery();
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + csvFileName + "loaded in Redshift.");
                deleteFileFromS3(bucket, csvFileName);
                return true;
            }
            catch (System.Data.Common.DbException ex)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Error on load.");
                Console.WriteLine(ex.Message);
                return false;
            }



            /*

           

            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = "cmd";
            p.Start();

            p.StandardInput.WriteLine("set AWS_ACCESS_KEY_ID=" + S3AccessKey);
            p.StandardInput.WriteLine("set AWS_SECRET_KEY=" + S3SecretKey);
            p.StandardInput.WriteLine("set AWS_REDSHIFT_USER=" + UserId);
            p.StandardInput.WriteLine("set AWS_REDSHIFT_PASS=" + Password);
            p.StandardInput.WriteLine("\"" + JavaPath.Replace("\\\\", "\\") + "\" -jar " + RedshiftLoaderPath + " --load --cluster pu-dw-1 --from s3://" + bucket + csvFileName + " --to " + tableName + "  --option \"delimiter ','\" --option removequotes --discard-source");
            p.StandardInput.WriteLine("exit  %errorlevel%");

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
                Environment.Exit(5);
                return false;
            }
             */
        }
        void DataTableToCsv(DataTable dt, string csvFileName, bool hasColumnNames, DataTable SchemaTableDefinition)
        {
            //        using (var sw = new System.IO.StreamWriter(csvFileName, true, Encoding.GetEncoding("iso-8859-1")))

            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            using (var sw = new System.IO.StreamWriter(csvFileName, true, utf8WithoutBom))
            {
                bool isFirst = true;
                StringBuilder sb = new StringBuilder();
                if (hasColumnNames)
                {
                    foreach (var c in dt.Columns)
                    {
                        if (!isFirst)
                        {
                            sb.Append(",");
                        }
                        sb.Append(c.ToString().Replace(",", " ").Replace("\r", " ").Replace("\n", " "));
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

                    /*
                                 if (linha == 773)
                                 {
                                     Console.WriteLine(93);
                                 }
              */
                    foreach (var c in r.ItemArray)
                    {

                        if (!isFirst)
                        {
                            sb.Append(",");
                        }
                        if (dt.Columns[columnIndex].DataType.ToString() == "System.DateTime" && c != DBNull.Value)
                        {
                            if (SchemaTableDefinition.Rows[columnIndex]["DataTypeName"].ToString() == "date")
                            {
                                sb.Append(((DateTime)c).ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                sb.Append(((DateTime)c).ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else
                        {
                            if (utf8WithoutBom.GetByteCount(c.ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", "")) > (int)(SchemaTableDefinition.Rows[columnIndex]["ColumnSize"]))
                            {

                                var bytes = new byte[(int)(SchemaTableDefinition.Rows[columnIndex]["ColumnSize"]) - 3];
                                int byteIndex = 0;
                                foreach (byte b in utf8WithoutBom.GetBytes(c.ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", "").ToCharArray()))
                                {
                                    bytes[byteIndex] = b;
                                    byteIndex++;
                                    if (byteIndex >= bytes.Length)
                                    {
                                        break;
                                    }
                                }
                                sb.Append(utf8WithoutBom.GetString(bytes));

                            }
                            else
                            {
                                sb.Append(c.ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", ""));
                            }
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

        private bool odbcLoadToRedShift(string csvDirectory, string csvFileName, string tableName)
        {
            /*
            DataTable SchemaTableDefinition;

            if (cnRedShift.State != ConnectionState.Open)
            {
                cnRedShift.Open();
            }
            var cmd = cnRedShift.CreateCommand();
            cmd.CommandTimeout = 0;
            string values = "";
            foreach (var f in SchemaTableDefinitionRedShift.Rows)
            {
                values += ",?";
            }
            values = values.Substring(1);
            cmd.CommandText = string.Format("insert into {0} values ({1})", tableName, values);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + e.Message + " " + e.StackTrace);
                return false;
            }*/
            return false; //TODO
        }
        bool loadToRedShift(string bucket, string csvDirectory, string csvFileName, string tableName)
        {
            FileInfo fi = new FileInfo(csvDirectory + "\\" + csvFileName);
            if (fi.Length > 0)
            {
                if (!moveToS3(bucket, csvDirectory + "\\" + csvFileName))
                {
                    return false;
                }
                return bulkLoadToRedShift(bucket, csvFileName, tableName);

            }
            else
            {
                return odbcLoadToRedShift(csvDirectory, csvFileName, tableName);
            }
        }
        public override void ConsumeData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string csvFileName = FileInfo.Name.Split('.')[0] + FileInfo.Name.Split('.')[1] + ".csv";
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Generating csv file " + CsvDirectory + "\\" + csvFileName);
                DataTableToCsv(dt, CsvDirectory + "\\" + csvFileName, false, SchemaTableDefinition);
            }
        }
        public override void EndOfFileRead()
        {
            string csvFileName = FileInfo.Name.Split('.')[0] + FileInfo.Name.Split('.')[1] + ".csv";

            if (File.Exists(CsvDirectory + "\\" + csvFileName))
            {
                string bucket = @"pu-redshift-workarea/input/subject=salesforce/data/object=" + TableName.Replace("[", "").Replace("]", "").Replace(".", "") + "/";
                if (!loadToRedShift(bucket, CsvDirectory, csvFileName, StageTableName))
                {
                    Environment.Exit(5);
                }
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " No file " + CsvDirectory + "\\" + csvFileName + " to load.");

            }
        }
        public override DataTable GetSchema(string tableName)
        {

            DataTable dt = base.GetSchema(tableName);
            if (dt == null)
            {
                return null;
            }
            dt.Columns.Add("DataTypeName");
            var dic = new Dictionary<string, string>();
            if (DdlFileInfo != null)
            {
                using (StreamReader sr = new StreamReader(DdlFileInfo.FullName))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Replace(",", "").Replace("\"", "").Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                        if (line.Length == 2)
                        {
                            dic.Add(line[0].ToLower(), line[1].ToLower());
                        }
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        string d;
                        if (dic.TryGetValue(row["ColumnName"].ToString(), out d))
                        {
                            row["DataTypeName"] = d;
                        }
                    }
                }
            }
            return dt;
        }
    }
}
