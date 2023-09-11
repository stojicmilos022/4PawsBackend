using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsBackend.Interfaces;

namespace PawsBackend.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly ISalonRepository _salonRepository;
        private readonly IMapper _mapper;

        public SalonController(ISalonRepository SalonRepository, IMapper mapper)
        {
            _salonRepository = SalonRepository;
            _mapper = mapper;

        }
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string bucketName = "slikesalona";
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }
            //some test comment 
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
                    string fileName = Path.GetFileName(file.FileName);
                    var uploadObject = await storageClient.UploadObjectAsync(bucketName, fileName, fileExtension, stream);
                    return Ok(new { fileId = uploadObject.Name });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
