namespace ApiBiblio.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string? NomAuteur { get; set; }
        public string? PrenomAuteur { get; set; }
        public virtual List<Livre> Livres { get; set; } = new();

    }
}
