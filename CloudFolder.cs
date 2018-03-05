//  Environment: Visual Studio 2017
//  Download NuGet Package AWSSDK.S3﻿
//  Use .Net Core Framework
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

	    /*
	    	Enter Your Credentials Below (Next 4 Lines)
		If you are using IAM user then allow access to S3 (policy)
	    */
            string AccessKey = "Enter Access Key Here";					
            string SecretKey = "Enter Secret Key Here";					
            string existingBucketName = "Name of The Bucket in S3";			
            string directoryPath = "Path of the location of Cloud Folder";

	    /*
	     *
	     
		NOTE: Check your Region of the bucket (next line).
		      This bucket is based was Mumbai i.e why region is APSouth1
		      Check region code in the resources section.
	     
	     *
	     */
		
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
            catch (Exception e)
            {
                Console.WriteLine("ERROR MESSAGE : " + e.Message);
            }
            Directory.Delete(directoryPath, true);
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Transfer Complete");
            Console.ReadLine();
        }
    }
}
