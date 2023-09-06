using PawsBackend.Interfaces;
using PawsBackend.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

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
            //Termin termin=new Termin();
            //    termin.TerminTekst = terminP;
            termin.Datum = DateTime.Parse(termin.text);
            _context.Termin.Add(termin);
            _context.SaveChanges();
        }

        public void DeleteTermin(Termin termin)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Termin> GetAll()
        {
            return _context.Termin;
        }
    }
}
