using ApiBiblio.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ApiBiblio.DTOs
{
    public class LivreDTO
    {
        public class LivreDto
        {
            public int Id { get; set; }
            public string Titre { get; set; }
            public bool Disponible { get; set; }
            public int AnneePublication { get; set; }
            public string Isbn { get; set; }
            public string Categorie { get; set; }
            public string Auteur { get; set; }
            public string Genre { get; set; }
        }

        public class CreateLivreDto
        {
            public string Titre { get; set; }
            public int AnneePublication { get; set; }
            public string Isbn { get; set; }
            public int IdCategorie { get; set; }
            public int IdAuteur { get; set; }
            public int IdGenre { get; set; }
        }

        public class UpdateLivreDto
        {
            public string Titre { get; set; }
            public int AnneePublication { get; set; }
            public int IdCategorie { get; set; }
            public int IdAuteur { get; set; }
            public int IdGenre { get; set; }
        }
    }
}
