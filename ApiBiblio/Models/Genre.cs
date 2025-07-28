using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public required string NomGenre { get; set; }
    }
}
