using AutoMapper;
using AutoMapper.QueryableExtensions;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsBackend.Interfaces;
using PawsBackend.Models;
using PawsBackend.Repository;

namespace PawsBackend.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GaleryController : ControllerBase
    {
        private readonly IGaleryRepository _galeryRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GaleryController(IGaleryRepository GaleryRepository, IMapper mapper, IConfiguration configuration)
        {
            _galeryRepository = GaleryRepository;
            _configuration = configuration;
            _mapper = mapper;
            _configuration = configuration;
        }


        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            string bucketName = "slikegalery";
            List<string> fileIds = new List<string>();

            if (files == null || files.Count == 0)
            {
                return BadRequest("Please select one or more files to upload.");
            }

            foreach (var file in files)
            {
                if (file.Length == 0)
                {

                    continue;
                }

                try
                {
                    var fileId = await FileUpload.UploadToGoogle(file, bucketName);
                    string fName = Path.GetFileName(file.FileName);
                    string fExt = Path.GetExtension(file.FileName);
                    string fullPath = $"https://storage.cloud.google.com/{bucketName}/{fName}";

                    _galeryRepository.AddGalerySlika(fullPath, fName);
                    fileIds.Add(fileId);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }

            if (fileIds.Count > 0)
            {
                return Ok(new { FileIds = fileIds });
            }
            else
            {
                return BadRequest("No files were successfully uploaded.");
            }
            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {

            return Ok(_galeryRepository.GetAll().ProjectTo<GaleryDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult DeleteGelery(int id)
        {
            var galery = _galeryRepository.GetById(id);
            if (galery == null)
            {
                return NotFound();
            }

            _galeryRepository.DeleteGalery(galery);
            string bucketName = "slikegalery";
            string? objectName = galery.FileName;
            GoogleCredential credential = GoogleCredential.FromFile("C:\\Source\\WebDevelopment\\4PawsBackend\\4PawsBackend\\paws-398316-cba858c90847.json");
            var storageClient = StorageClient.Create(credential);


            storageClient.DeleteObject(bucketName, objectName);
            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetGalery(int id)
        {
            var Galery = _galeryRepository.GetById(id);
            if (Galery == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GaleryDTO>(Galery));
        }
    }
}
