using ApiBiblio.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiBiblio.DTOs
{
    public class AuteurDTO
    {
        public int Id { get; set; }
        public string? NomAuteur { get; set; }
        public string? PrenomAuteur { get; set; }

        public AuteurDTO() { }

        public AuteurDTO(Auteur auteur) =>
            (Id, NomAuteur, PrenomAuteur) = (auteur.Id, auteur.NomAuteur, auteur.PrenomAuteur);
    }
}
