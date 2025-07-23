using ApiBiblio.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.DTOs
{
    public class LivreDTO
    {
        public int Id { get; set; }

        public required string Titre { get; set; }
        public bool Disponible { get; set; }
        public int AnneePublication { get; set; }
        public string? ISBN { get; set; }
        public int IdEmprunt { get; set; }
        public int IdCategorie { get; set; }

        public LivreDTO() { }
        public LivreDTO(Livre Livre) =>
            (Id, Titre, Disponible, AnneePublication, ISBN, IdEmprunt, IdCategorie) = (Livre.Id, Livre.Titre, Livre.Disponible, Livre.AnnePublication, Livre.ISBN, Livre.IdEmprunt, Livre.IdCategorie);
    }
}
