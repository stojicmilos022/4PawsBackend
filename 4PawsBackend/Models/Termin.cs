using System.ComponentModel.DataAnnotations;

namespace PawsBackend.Models
{
    public class Termin
    {
        public int Id { get; set; }
        [Required]
        public DateTime Datum { get; set; }

        public string ?DatumString { get; set; }

    }
}
