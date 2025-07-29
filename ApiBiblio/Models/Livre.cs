using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Livre
    {
        public int Id { get; set; }

        [DisplayName("Titre")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Veuillez insérez un titre")]
        public required string Titre { get; set; }

        [DisplayName("Est-ce que le livre est Disponible ?")]
        public bool Disponible { get; set; } = true;

        [DisplayName("Année de Publication")]
        public Int16 AnnePublication { get; set; }

        [DisplayName("ISBN")]
        [StringLength(17, MinimumLength = 13)]
        [RegularExpression(@"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$", ErrorMessage = "Veuillez entrer un ISBN valide")]
        public string? ISBN { get; set; }

        [DisplayName("Catégorie")]
        public int IdCategorie { get; set; }
    }
}
