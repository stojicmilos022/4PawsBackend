using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace PawsBackend
{
    public class FileUpload
    {

        public static async Task<string> UploadToGoogle(IFormFile file, string bucketName)
        {
            //string projectId = "your-project-id"; // Replace with your Google Cloud project ID
            //string bucketName = "your-bucket-name"; // Replace with your Google Cloud Storage bucket name
            //string filePath = Path.GetFullPath(file.FileName); // Replace with the path to your local file
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.GetFullPath(file.FileName);
            //string objectName = "file.txt"; // Replace with the desired object name in the bucket

            // Authenticate with Google Cloud using a service account key file
            GoogleCredential credential = GoogleCredential.FromFile("C:\\Source\\WebDevelopment\\4PawsBackend\\4PawsBackend\\paws-398316-cba858c90847.json");
            StorageClient storageClient = StorageClient.Create(credential);

            // Upload the file to Google Cloud Storage
            using (var stream = file.OpenReadStream())
            {
                var uploadObject = await storageClient.UploadObjectAsync(bucketName, fileName, fileExtension, stream);
                //Console.WriteLine($"File {objectName} uploaded to {bucketName}.");
                
            }

            return fileName;
        }

    }
}
