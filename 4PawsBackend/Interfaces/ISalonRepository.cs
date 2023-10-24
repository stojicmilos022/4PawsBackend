using PawsBackend.Models;

namespace PawsBackend.Interfaces
{
    public interface ISalonRepository
    {
        IQueryable<Salon> GetAll();


        Salon GetById(int id);

        void AddSalonSlika(string fullPath);

        void DeleteSalon(Salon salon);
    }
}
