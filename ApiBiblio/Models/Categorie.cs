namespace ApiBiblio.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public required string NomCategorie { get; set; }
        public virtual List<Livre> Livres { get; set; } = new();

    }
}
