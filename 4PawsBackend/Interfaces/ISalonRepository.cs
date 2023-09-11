using PawsBackend.Models;

namespace PawsBackend.Interfaces
{
    public interface ISalonRepository
    {
        IQueryable<Salon> GetAll();


        Salon GetById(int id);

        void AddSalonSlika(IFormFile file);

        void DeleteTermin(Salon salon);
    }
}
