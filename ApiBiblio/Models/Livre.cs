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
        public string? ISBN { get; set; }
        public int IdCategorie { get; set; }
    }
}
