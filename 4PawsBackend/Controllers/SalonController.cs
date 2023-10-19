using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsBackend.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing.Constraints;
using PawsBackend.Models;
using PawsBackend.Repository;
using AutoMapper.QueryableExtensions;

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
        private readonly IConfiguration _configuration;

        public SalonController(ISalonRepository SalonRepository, IMapper mapper, IConfiguration configuration)
        {
            _salonRepository = SalonRepository;
            _configuration = configuration;
            _mapper = mapper;
            _configuration = configuration;

        }



        //[HttpPost("upload")]

        //[HttpPost("api/upload")]
        [HttpPost]
        [AllowAnonymous]
        //public async Task<IHttpActionResult> Upload()
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
                await FileUpload.UploadToGoogle(file, bucketName);
                string fName=Path.GetFileName(file.FileName);
                string fExt=Path.GetExtension(file.Name);
                string fullPath = @"https://storage.cloud.google.com/slikesalona/"+fName;

                _salonRepository.AddSalonSlika(fullPath);
                //string appDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ////var serviceAccountEmail = "vaskozabata@paws-398316.iam.gserviceaccount.com";
                ////string bucketName = "slikesalona";
                //var serviceAccountKeyPath = _configuration["GoogleApi:ApiKeyPath"]; ;

                //var credential = GoogleCredential.FromFile(serviceAccountKeyPath)
                //    .CreateScoped(DriveService.Scope.Drive);

                //var storageClient = StorageClient.Create(credential);

                //using (var stream = file.OpenReadStream())
                //{
                //    string fileExtension = Path.GetExtension(file.FileName);
                //    string fileName = Path.GetFileName(file.FileName);
                //    var uploadObject = await storageClient.UploadObjectAsync(bucketName, fileName, fileExtension, stream);
                //    return Ok(new { fileId = uploadObject.Name });
                //}
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {

            return Ok(_salonRepository.GetAll().ProjectTo<SalonDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
