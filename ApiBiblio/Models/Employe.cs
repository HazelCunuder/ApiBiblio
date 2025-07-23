using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ApiBiblio.Models
{
    public class Employe
    {
        public int Id { get; set; }
        public required string NomEmploye { get; set; }
        public required string PrenomEmploye { get; set; }
        public required string LoginEmploye { get; set; } // Identifiant utilisé pour se connecter
        public required string MdpEmploye { get; set; } 
    }
}
