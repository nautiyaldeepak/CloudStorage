//  Environment: Visual Studio 2017
//  Download NuGet Package AWSSDK.S3﻿
//  Use .Net Core Framework
using System;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;

namespace CloudFolder
{
	public static string ClientRegion(string RegionOfClient)
        {
            switch (RegionOfClient)
            {
                case "ireland":
                    return "eu-west-1";
                case "mumbai":
                    return "ap-south-1";
                case "frankfurt":
                    return "eu-central-1";
                case "london":
                    return "eu-west-2";
                case "sydney":
                    return "ap-southeast-2";
                case "ohio":
                    return "us-east-2";
                case "virginia":
                    return "us-east-1";
                case "california":
                    return "us-west-1";
                case "oregon":
                    return "us-west-2";
                case "singapore":
                    return "ap-southeast-1";
                case "tokyo":
                    return "ap-northeast-1";
                case "canada":
                    return "ca-central-1";
                case "seoul":
                    return "ap-northeast-2";
                case "paris":
                    return "eu-west-3";
                case "sao paulo":
                    return "sa-east-1";
            }
            return "";
        }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Transferring Data To S3......");

	    /*
	    	Enter Your Credentials Below (Next 4 Lines)
		If you are using IAM user then allow access to S3 (policy)
	    */
            string AccessKey = "Enter Access Key Here";					
            string SecretKey = "Enter Secret Key Here";					
            string existingBucketName = "Name of The Bucket in S3";			
            string directoryPath = "Path of the location of Cloud Folder";
	    string NameOfTheRegion = "NameOfTheRegion";	
	    string RegionOfTheBucket = " Enter Region Name >> Eg: mumbai ";
            try
            {
                TransferUtility directoryTransferUtility = new TransferUtility(new AmazonS3Client(AccessKey, SecretKey, Amazon.RegionEndpoint.GetBySystemName(RegionOfTheBucket)));
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
