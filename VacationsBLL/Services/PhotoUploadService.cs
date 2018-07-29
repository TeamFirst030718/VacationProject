using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using VacationsBLL.Interfaces;

namespace VacationsBLL.Services
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private const string ConnectionStringSettingName = "StorageConnectionString";
        private const string ContainerName = "photos";
        private const string defaultPhoto = "default.jpg";

        private byte[] ConvertFileIntoByteArray(HttpPostedFileBase file)
        {
            if (file != null)
            {
                byte[] buffer = new byte[file.ContentLength];
                byte[] content;
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    content = ms.ToArray();
                }

                return content;
            }

            return new byte[0];
        }

        public void UploadPhoto(HttpPostedFileBase file, string id)
        {
            if (file == null)
            {
                var bytePhoto = DownloadFileInBlocks(defaultPhoto);
                var fileName = string.Format($"{id}.jpg");
                UploadFileInBlocks(bytePhoto, fileName);
            }
            else
            {
                var bytePhoto = ConvertFileIntoByteArray(file);
                var fileName = string.Format($"{id}.jpg");
                UploadFileInBlocks(bytePhoto, fileName);
            }        
        }

        private byte[] DownloadFileInBlocks(string fileName)
        {
            CloudBlobContainer cloudBlobContainer = GetContainerReference();
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(Path.GetFileName(fileName));

            int blockSize = 1024 * 1024; // 1 MB block size

            blob.FetchAttributes();
            long fileSize = blob.Properties.Length;

            byte[] blobContents = new byte[fileSize];
            int position = 0;

            while (fileSize > 0)
            {
                int blockLength = (int)Math.Min(blockSize, fileSize);

                blob.DownloadRangeToByteArray(blobContents, position, position, blockLength);

                position += blockLength;
                fileSize -= blockSize;
            }

            return blobContents;
        }

        private void UploadFileInBlocks(byte[] file, string fileName)
        {
            CloudBlobContainer cloudBlobContainer = GetContainerReference();
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(Path.GetFileName(fileName));

            blob.DeleteIfExists();

            List<string> blockIDs = new List<string>();

            int blockSize = 5 * 1024 * 1024;

            long fileSize = file.Length;

            int blockId = 0;

            while (fileSize > 0)
            {                  
                int blockLength = (int)Math.Min(blockSize, fileSize);


                string blockIdEncoded = GetBase64BlockId(blockId);
                blockIDs.Add(blockIdEncoded);

                byte[] bytesToUpload = new byte[blockLength];
                Array.Copy(file, blockId * blockSize, bytesToUpload, 0, blockLength);
                using (MemoryStream memoryStream = new MemoryStream(bytesToUpload, 0, blockLength))
                {
                    blob.PutBlock(blockIdEncoded, memoryStream, null, null, new BlobRequestOptions
                    {
                        RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(2), 1)
                    });
                }

                blockId++;
                fileSize -= blockLength;
            }

            blob.PutBlockList(blockIDs);
        }

        private string GetBase64BlockId(int blockId)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}", blockId.ToString("0000000"))));
        }

        private CloudBlobContainer GetContainerReference()
        {
            string connectionString = CloudConfigurationManager.GetSetting(ConnectionStringSettingName);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);

            container.CreateIfNotExists();

            return container;
        }
    }
}
