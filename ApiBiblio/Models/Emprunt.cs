namespace ApiBiblio.Models
{
    public class Emprunt
    {
        public int Id { get; set; }
        public DateOnly DateEmprunt { get; set; }
        public required bool Statut {  get; set; }
        public DateOnly DateRetour { get; set; }
        public int IdMembre { get; set; }
    }
}
