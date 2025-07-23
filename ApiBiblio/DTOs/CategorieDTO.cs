using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class CategorieDTO
    {
        public int Id { get; set; }

        public required string NomCategorie { get; set; }

        public CategorieDTO() { }

        public CategorieDTO(Categorie categorie) =>
            (Id, NomCategorie) = (categorie.Id, categorie.NomCategorie);
    }
}
