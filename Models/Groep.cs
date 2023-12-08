using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace GroepSpace2024.Models
{
    public class Groep
    {
        public int Id { get; set; }
        [Display (Name="Naam")]
        public string Name { get; set; }

        [Display(Name = "Omschrijving")]
        public string Description { get; set; }

        [Display(Name = "Groep Aangemaakt")]
        [DataType(DataType.Date)]
        public DateTime Started { get; set; }

        [Display(Name = "Groep Gestopt")]
        [DataType(DataType.Date)]
        public DateTime Ended { get; set; } = DateTime.MaxValue;
    }
}
