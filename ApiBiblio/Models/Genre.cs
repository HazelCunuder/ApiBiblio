using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [DisplayName("Genre")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un nom de genre valide")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [StringLength(30)]
        public required string NomGenre { get; set; }
    }
}
