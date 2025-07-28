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
        public int IdCategorie { get; set; }

        public LivreDTO() { }
        public LivreDTO(Livre livre) =>
            (Id, Titre, Disponible, AnneePublication, ISBN, IdCategorie) = (livre.Id, livre.Titre, livre.Disponible, livre.AnnePublication, livre.ISBN, livre.IdCategorie);
    }
}
