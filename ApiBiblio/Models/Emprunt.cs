using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBiblio.Models
{
    public class Emprunt
    {
        public int Id { get; set; }

        [DisplayName("Date de début d'emprunt")]
        [DataType(DataType.Date)]
        public DateOnly DateEmprunt { get; set; }

        [DisplayName("Rendu ?")]
        public required bool Statut { get; set; }

        [DisplayName("Date de retour")]
        [DataType(DataType.Date)]
        public DateOnly DateRetour { get; set; }

        [DisplayName("Id de l'emprunteur")]
        public int IdMembre { get; set; }

        [ForeignKey("IdMembre")]
        public Membre? Membre { get; set; }

        [DisplayName("Id du livre")]
        public int IdLivre { get; set; }

        [ForeignKey("IdLivre")]
        public Livre? Livre { get; set; }
    }
}
