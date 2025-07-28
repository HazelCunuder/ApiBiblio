using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.Models
{
    public class Membre
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [Required]
        [StringLength(150)]
        public required string NomMembre{ get; set; }

        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [Required]
        [StringLength(150)]
        public required string PrenomMembre { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$")]
        [StringLength(16, MinimumLength = 8)]
        public required string MdpMembre { get; set; }

        [Required]
        public required string AdressePostale {  get; set; }

        [RegularExpression(@"/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/")]
        [Required]
        public  required string AdresseMail { get; set; }

        [RegularExpression(@"^(0[1-9])(?:[ .-/]?[0-9]{2}){4}$")]
        [Required]
        public required string Telephone {  get; set; }
        public List<string>? HistoriqueMembre { get; set; } = new();
    }
}
