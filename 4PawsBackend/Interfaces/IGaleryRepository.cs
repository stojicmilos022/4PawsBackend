
using PawsBackend.Models;
namespace PawsBackend.Interfaces
{
    public interface IGaleryRepository
    {
        IQueryable<Galery> GetAll();


        Galery GetById(int id);

        void AddGalerySlika(string fullPath, string fileName);

        void DeleteGalery(Galery galery);
    }
}
