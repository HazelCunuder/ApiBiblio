namespace ApiBiblio.Models
{
    public class Role
    {
        public int Id { get; set; }
        public required string NomRole { get; set; } // Ex: Admin ou Bibliothecaire
        public virtual List<Emprunt> Emprunts { get; set; } = new();

    }
}
