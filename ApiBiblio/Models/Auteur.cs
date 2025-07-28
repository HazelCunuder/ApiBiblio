using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [Required]
        [StringLength(150)]
        public string? NomAuteur { get; set; }

        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [Required]
        [StringLength(150)]
        public string? PrenomAuteur { get; set; }
    }
}
