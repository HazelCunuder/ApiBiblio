using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        [DisplayName("Nom")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un nom valide")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [StringLength(150)]
        public string? NomAuteur { get; set; }

        [DisplayName("Prénom")]
        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$", ErrorMessage = "Veuillez entrer un prénom valide")]
        [Required(ErrorMessage = "Vous ne pouvez pas laisser ce champ vide")]
        [StringLength(150)]
        public string? PrenomAuteur { get; set; }
    }
}
