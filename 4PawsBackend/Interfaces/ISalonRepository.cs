using PawsBackend.Models;

namespace PawsBackend.Interfaces
{
    public interface ISalonRepository
    {
        IQueryable<Salon> GetAll();


        Salon GetById(int id);

        void AddSalonSlika(string fullPath,string fileName);

        void DeleteSalon(Salon salon);
    }
}
