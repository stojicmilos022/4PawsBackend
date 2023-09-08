using PawsBackend.Interfaces;
using PawsBackend.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Drawing.Text;
using System.Linq;

namespace PawsBackend.Repository
{
    public class TerminRepository :ITerminRepository
    {
        private readonly AppDbContext _context;
        public TerminRepository(AppDbContext context)
        {
            this._context = context;
        }

        public Termin GetById(int id)
        {
            return _context.Termin.FirstOrDefault(p => p.Id == id);
        }

        public void AddTermin(Termin termin)
        {

            termin.DatumString= termin.Datum.ToString("dd.MM.yyyy HH:mm");

            //    termin.TerminTekst = terminP;
            //termin.Datum = DateTime.Parse(termin.text);
            _context.Termin.Add(termin);
            _context.SaveChanges();
        }

        public void DeleteTermin(Termin termin)
        {
            _context.Termin.Remove(termin);
            _context.SaveChanges();
        }

        public IQueryable<Termin> GetAll()
        {
            var recordsToRemove = _context.Termin.Where(u => u.Datum < DateTime.Now).ToList();
            _context.Termin.RemoveRange(recordsToRemove);
            _context.SaveChanges();
            return _context.Termin.OrderBy(u=>u.Datum);
        }
    }
}
