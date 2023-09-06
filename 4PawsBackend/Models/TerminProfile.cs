using PawsBackend.Models;
using AutoMapper;


namespace PawsBackend.Models
{
    public class TerminProfile :Profile
    {
        public TerminProfile()
        {
            CreateMap<Termin, TerminDTO>();
        }
    }
}
