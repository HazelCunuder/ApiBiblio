using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ApiBiblio.Models
{
    public class Employe
    {
        public int Id { get; set; }

        [RegularExpression(@"[A-Z]+[A-zà-ÿ\s'-]+$")]
        [StringLength(200)]
        [Required]
        public required string NomEmploye { get; set; }

        [RegularExpression(@"^[A-Z]+[A-zà-ÿ\s'-]+$")]
        [StringLength(200)]
        [Required]
        public required string PrenomEmploye { get; set; }

        [Required]
        public required string LoginEmploye { get; set; } // Identifiant utilisé pour se connecter

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$")]
        [StringLength(16, MinimumLength = 8)]
        public required string MdpEmploye { get; set; } 
    }
}
