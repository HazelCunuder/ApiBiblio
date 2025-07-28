using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [Required]
        [StringLength(30)]
        public required string NomGenre { get; set; }
    }
}
