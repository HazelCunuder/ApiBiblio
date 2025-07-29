using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [DisplayName("Catégorie")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [StringLength(30)]
        public required string NomCategorie { get; set; }
    }
}
