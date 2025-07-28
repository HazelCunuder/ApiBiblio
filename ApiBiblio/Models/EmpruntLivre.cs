using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBiblio.Models
{
    public class EmpruntLivre
    {
        public int IdEmprunt { get; set; }
        public int IdLivre { get; set; }

        [ForeignKey("IdEmprunt")]
        public virtual Emprunt Emprunt { get; set; }

        [ForeignKey("IdLivre")]
        public virtual Livre Livre { get; set; }
    }
}
