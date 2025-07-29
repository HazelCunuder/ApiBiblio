namespace ApiBiblio.Models
{
    public class Livre_Genre
    {
        public int IdLivre { get; set; }
        public int IdGenre{ get; set; }

        public Livre? Livre { get; set; }
        public Genre? Genre { get; set; }
    }
}
