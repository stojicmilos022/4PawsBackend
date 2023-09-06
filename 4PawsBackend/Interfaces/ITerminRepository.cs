using PawsBackend.Models;

namespace PawsBackend.Interfaces
{
    public interface ITerminRepository
    {
        IQueryable<Termin> GetAll();


        Termin GetById(int id);

        void AddTermin(Termin termin);

        void DeleteTermin(Termin termin);

    }
}
