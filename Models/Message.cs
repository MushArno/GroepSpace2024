using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroepSpace2024.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [Display (Name = "Titel")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Booschap")]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Verzonden")]
        public DateTime Sent { get; set; } = DateTime.Now;
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        [ForeignKey("Groep")]
        [Display(Name ="Ontvanger")]
        public int RecipientId { get; set; }

        public Groep? Recipient { get; set; } //? = nullable, kan leeg zijn.
    }
}
