using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class EmpruntDTO
    {
        public int Id { get; set; }
        public DateOnly DateEmprunt { get; set; }
        public bool Statut { get; set; }
        public DateOnly DateRetour { get; set; }
        public int MembreId { get; set; }

        public EmpruntDTO() { }
        public EmpruntDTO(Emprunt EmpruntItem) => 

            (Id, DateEmprunt, DateRetour, MembreId) = (EmpruntItem.Id, EmpruntItem.DateRetour, EmpruntItem.DateEmprunt, EmpruntItem.MembreId)
    }
}
