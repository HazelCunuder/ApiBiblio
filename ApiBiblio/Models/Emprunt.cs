using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBiblio.Models
{
    public class Emprunt
    {
        public int Id { get; set; }

        [Display(Name = "Date de l'emprunt")]
        [DataType(DataType.Date)]
        public DateOnly DateEmprunt { get; set; }

        [Display(Name = "Rendu ?")]
        public required bool Statut { get; set; }

        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateOnly DateRetour { get; set; }

        public int IdMembre { get; set; }
    }
}
