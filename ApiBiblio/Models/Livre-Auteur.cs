namespace ApiBiblio.Models
{
    public class Livre_Auteur
    {
        public int IdLivre { get; set; }
        public int IdAuteur { get; set; }

        public Livre? Livre { get; set; }
        public Auteur? Auteur { get; set; } 
    }
}
