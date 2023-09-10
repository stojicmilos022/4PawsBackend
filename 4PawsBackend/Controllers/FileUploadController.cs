using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace _4PawsBackend.Controllers
{
    [Route("api")]
    public class FileUploadController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file,string bucketName)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }

            try
            {
                string appDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //var serviceAccountEmail = "vaskozabata@paws-398316.iam.gserviceaccount.com";
                //string bucketName = "slikesalona";
                var serviceAccountKeyPath = "C:\\Source\\WebDevelopment\\4PawsBackend\\4PawsBackend\\paws-398316-cba858c90847.json";

                var credential = GoogleCredential.FromFile(serviceAccountKeyPath)
                    .CreateScoped(DriveService.Scope.Drive);

                var storageClient = StorageClient.Create(credential);

                using (var stream = file.OpenReadStream())
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName=Path.GetFileName(file.FileName);
                    var uploadObject = await storageClient.UploadObjectAsync(bucketName, fileName, fileExtension, stream);
                    return Ok(new { fileId = uploadObject.Name });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        //    using (var driveService = new DriveService(new BaseClientService.Initializer
        //    {
        //        HttpClientInitializer = credential
        //    }))
        //    {
        //        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        //        {
        //            Name = file.FileName
        //        };

        //        FilesResource.CreateMediaUpload request;

        //        using (var stream = file.OpenReadStream())
        //        {
        //            request = driveService.Files.Create(fileMetadata, stream, file.ContentType);
        //            request.Fields = "id";
        //            await request.UploadAsync();
        //        }

        //        var uploadedFile = request.ResponseBody;

        //        return Ok(new { fileId = uploadedFile.Id });
        //    }
        //}
        //catch (Exception ex)
        //{
        //    return StatusCode(500, $"Error: {ex.Message}");
        //}
    }
    
}
