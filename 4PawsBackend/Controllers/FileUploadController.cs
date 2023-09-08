using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }

            try
            {
                var serviceAccountEmail = "vaskozabata@paws-398316.iam.gserviceaccount.com";
                var serviceAccountKeyPath = "path-to-your-service-account-json-key.json";

                var credential = GoogleCredential.FromFile(serviceAccountKeyPath)
                    .CreateScoped(DriveService.Scope.Drive);

                using (var driveService = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential
                }))
                {
                    var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = file.FileName
                    };

                    FilesResource.CreateMediaUpload request;

                    using (var stream = file.OpenReadStream())
                    {
                        request = driveService.Files.Create(fileMetadata, stream, file.ContentType);
                        request.Fields = "id";
                        await request.UploadAsync();
                    }

                    var uploadedFile = request.ResponseBody;

                    return Ok(new { fileId = uploadedFile.Id });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
