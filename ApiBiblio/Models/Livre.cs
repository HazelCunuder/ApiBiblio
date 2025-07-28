namespace ApiBiblio.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public required string Titre { get; set; }
        public bool Disponible { get; set; } = true;
        public int AnnePublication { get; set; }
        public string? ISBN { get; set; }
        public int IdCategorie { get; set; }
    }
}
