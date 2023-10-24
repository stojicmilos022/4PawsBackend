using PawsBackend.Interfaces;
using PawsBackend.Models;

namespace PawsBackend.Repository
{
    public class SalonRepository : ISalonRepository
    {
        private readonly AppDbContext _context;
        public SalonRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void AddSalonSlika(string fullPath)
        {
            Salon salon= new Salon();
            salon.Path = fullPath;
            //termin.DatumString = termin.Datum.ToString("dd.MM.yyyy HH:mm");

            //    termin.TerminTekst = terminP;
            //termin.Datum = DateTime.Parse(termin.text);
            _context.SalonSlike.Add(salon);
            _context.SaveChanges();
        }

        public void DeleteSalon(Salon salon)
        {
            _context.SalonSlike.Remove(salon);
            _context.SaveChanges();
        }

        public IQueryable<Salon> GetAll()
        {

            return _context.SalonSlike;
        }

        public Salon GetById(int id)
        {
            return _context.SalonSlike.FirstOrDefault(p => p.Id == id);
        }
    }
}
