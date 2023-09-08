using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsBackend.Interfaces;
using PawsBackend.Models;

namespace PawsBackend.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TerminController : ControllerBase
    {
        private readonly ITerminRepository _terminRepository;
        private readonly IMapper _mapper;

        public TerminController(ITerminRepository TerminRepository, IMapper mapper)
        {
            _terminRepository = TerminRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {

            return Ok(_terminRepository.GetAll().ProjectTo<TerminDTO>(_mapper.ConfigurationProvider).ToList());
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetTermin(int id)
        {
            var Termin = _terminRepository.GetById(id);
            if (Termin == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TerminDTO>(Termin));
        }

        [HttpPost]
        [AllowAnonymous]
        //[Route("4paws/api/createTermin")]
        public IActionResult AddTermin(Termin termin)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //Termin termin=new Termin();
            //termin.DatumString = termin.Datum.ToString("dd.MM.yyyy HH:mm");
            _terminRepository.AddTermin(termin); 
                   return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTermin(int id)
        {
            var termin = _terminRepository.GetById(id);
            if (termin == null)
            {
                return NotFound();
            }

            _terminRepository.DeleteTermin(termin);
            return NoContent();
        }
    }
}
