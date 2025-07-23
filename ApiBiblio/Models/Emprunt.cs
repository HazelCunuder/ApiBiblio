namespace ApiBiblio.Models
{
    public class Emprunt
    {
        public int Id { get; set; }
        public DateOnly Date_Emprunt { get; set; }
        public bool Statut {  get; set; }
        public DateOnly Date_retour { get; set; }
        public int MembreId { get; set; }
    }
}
