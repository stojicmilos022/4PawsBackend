using PawsBackend.Models;
using AutoMapper;

namespace PawsBackend.Models
{
    public class SalonProfile :Profile
    {
        public SalonProfile() 
        {
            CreateMap<Salon, SalonDTO>();
        }
    }
}
