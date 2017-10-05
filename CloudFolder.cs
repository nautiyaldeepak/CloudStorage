using System;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;

namespace CloudFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Transferring Data To Amazon Cloud......");
            string AccessKey = "Enter Access Key Here";
            string SecretKey = "Enter Secret Key Here";
            string existingBucketName = "Name of The Bucket in S3";
            string directoryPath = "Path of the location of Cloud Folder";
            try
            {
                TransferUtility directoryTransferUtility = new TransferUtility(new AmazonS3Client(AccessKey, SecretKey, Amazon.RegionEndpoint.APSouth1));
                directoryTransferUtility.UploadDirectory(directoryPath, existingBucketName);
                directoryTransferUtility.UploadDirectory(directoryPath, existingBucketName, "*.*", SearchOption.AllDirectories);
                TransferUtilityUploadDirectoryRequest request = new TransferUtilityUploadDirectoryRequest
                {
                    BucketName = existingBucketName,
                    Directory = directoryPath,
                    SearchOption = SearchOption.AllDirectories,
                    SearchPattern = "*.*"
                };
                directoryTransferUtility.UploadDirectory(request);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("There is Some Problem");
                Console.WriteLine(e.Message, e.InnerException);
            }
            Directory.Delete(directoryPath, true);
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Transfer Complete");
            Console.ReadLine();
        }
    }
}
