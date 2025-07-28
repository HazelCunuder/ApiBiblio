using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBiblio.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public required string Titre { get; set; }
        public bool Disponible { get; set; } = true;
        public int AnnePublication { get; set; }
        public string? ISBN { get; set; }
        
        // Clé étrangères
        public int IdCategorie { get; set; }
        public int IdAuteur { get; set; }
        public int IdGenre { get; set; }
        public int IdEmprunt { get; set; }

        [ForeignKey("IdCategorie")]
        public virtual Categorie Categorie { get; set; }

        [ForeignKey("IdAuteur")]
        public virtual Auteur Auteur { get; set; }

        [ForeignKey("IdGenre")]
        public virtual Genre Genre { get; set; }

        [ForeignKey("IdEmprunt")]
        public virtual Emprunt Emprunt { get; set; }
        public virtual List<EmpruntLivre> EmpruntLivres { get; set; } = new();

    }
}
