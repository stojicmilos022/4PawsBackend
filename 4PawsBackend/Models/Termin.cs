using System.ComponentModel.DataAnnotations;

namespace PawsBackend.Models
{
    public class Termin
    {
        public int Id { get; set; }
        [Required]
        public string text { get; set; }

        public DateTime Datum { get; set; }
    }
}
