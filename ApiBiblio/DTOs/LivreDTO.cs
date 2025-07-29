using ApiBiblio.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.DTOs
{
    public class LivreDTO
    {
        public int IdAuteur { get; set; }
        public required string Titre { get; set; }
        public bool Disponible { get; set; }
        public int AnneePublication { get; set; }
        public string? ISBN { get; set; }
        public int IdCategorie { get; set; }
        public int IdGenre { get; set; }

        public LivreDTO() { }
        public LivreDTO(Livre livre) =>
            (IdAuteur, Titre, Disponible, AnneePublication, ISBN, IdCategorie, IdGenre) = (livre.IdAuteur, livre.Titre, livre.Disponible, livre.AnnePublication, livre.ISBN, livre.IdCategorie, livre.IdGenre);
    }
}
