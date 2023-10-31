using AutoMapper;
using PawsBackend.Models;

namespace PawsBackend.Models
{
    public class GaleryProfile :Profile
    {
        public GaleryProfile()
        {
            CreateMap<Galery, GaleryDTO>();
        }
    }
}
