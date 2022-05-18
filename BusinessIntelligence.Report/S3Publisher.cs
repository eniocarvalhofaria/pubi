using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Report
{
    public class S3Publisher : IPublisher
    {
        public S3Publisher(string bucket, string objectName,string fileName) 
        {
            Bucket = bucket;
            ObjectName = objectName;
            FileName = fileName;
        }
        public bool Publish()
        {

                 return putFileToS3().Sucess;
          



        }
        public string Bucket { get; set; }
        public string FileName { get; set; }
        public string ObjectName { get; set; }
        public string GetUrl()
        {
            string ret = null;
            if (Bucket.EndsWith("/"))
            {
                Bucket = Bucket.Substring(0, Bucket.Length - 1);
            }
            string bucketRoot = Bucket.Substring(0, Bucket.IndexOf("/") + 1 );

            ret = "https://reports.peixeurbano.com.br/" + Bucket.Replace(bucketRoot,"") +"/" + ObjectName ;
            return ret;
        }


        public Result putFileToS3()
        {
            var S3AccessKey = BusinessIntelligence.Util.EnvironmentParameters.S3AccessKey;
            var S3SecretKey = BusinessIntelligence.Util.EnvironmentParameters.S3SecretKey;
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Putting " + FileName + " in s3.");
            using (var client = new AmazonS3Client(S3AccessKey, S3SecretKey, Amazon.RegionEndpoint.USEast1))
            {
                string objectName = ObjectName;
                if (Bucket.EndsWith("/"))
                {
                    Bucket = Bucket.Substring(0, Bucket.Length - 1);
                }
                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = Bucket,
                    Key = objectName,
                    FilePath = FileName
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

    }
}
