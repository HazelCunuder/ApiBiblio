using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public required string NomCategorie { get; set; }
    }
}
