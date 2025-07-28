using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(200)]
        public string? NomAuteur { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(200)]
        public string? PrenomAuteur { get; set; }
    }
}
