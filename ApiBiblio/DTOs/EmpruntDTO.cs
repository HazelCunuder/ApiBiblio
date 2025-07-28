using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class EmpruntDTO
    {
        public int Id { get; set; }
        public DateOnly DateEmprunt { get; set; }
        public bool Statut { get; set; }
        public DateOnly DateRetour { get; set; }
        public int IdMembre { get; set; }

        public EmpruntDTO() { }
        public EmpruntDTO(Emprunt EmpruntItem) =>

            (Id, DateEmprunt, DateRetour, IdMembre) = (EmpruntItem.Id, EmpruntItem.DateRetour, EmpruntItem.DateEmprunt, EmpruntItem.IdMembre);
    }
}
