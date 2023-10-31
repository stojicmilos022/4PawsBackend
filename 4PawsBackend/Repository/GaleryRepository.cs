using PawsBackend.Interfaces;
using PawsBackend.Models;

namespace PawsBackend.Repository
{
    public class GaleryRepository : IGaleryRepository
    {
        private readonly AppDbContext _context;
        public GaleryRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void AddGalerySlika(string fullPath, string fileName)
        {
            Galery galery = new Galery();
            galery.Path = fullPath;
            galery.FileName = fileName;
            //termin.DatumString = termin.Datum.ToString("dd.MM.yyyy HH:mm");

            //    termin.TerminTekst = terminP;
            //termin.Datum = DateTime.Parse(termin.text);
            _context.GalerySlike.Add(galery);
            _context.SaveChanges();
        }

        public void DeleteGalery(Galery galery)
        {
            _context.GalerySlike.Remove(galery);
            _context.SaveChanges();
        }

        public IQueryable<Galery> GetAll()
        {

            return _context.GalerySlike;
        }

        public Galery GetById(int id)
        {
            return _context.GalerySlike.FirstOrDefault(p => p.Id == id);
        }
    }
}
