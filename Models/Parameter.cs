using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroepSpace2024.Models
{
    public class Parameter
    {
        [Key]
        public string Name { get; set; }

        public string Value { get; set; }
        [ForeignKey("GroepSpace2024User")]
        public string UserId {  get; set; }
        public DateTime lastChanged { get; set; }
    }
}
