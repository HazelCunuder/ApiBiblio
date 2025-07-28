using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Livre
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public required string Titre { get; set; }
        public bool Disponible { get; set; } = true;
        public Int16 AnnePublication { get; set; }

        [StringLength(17, MinimumLength = 13)]
        [RegularExpression(@"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$")]
        public string? ISBN { get; set; }
        public int IdCategorie { get; set; }
    }
}
