namespace ApiBiblio.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public required string NomMembre{ get; set; }
        public required string PrenomMembre { get; set; }
        public required string MdpMembre { get; set; }
        public string? AdressePostale {  get; set; }
        public  required string AdresseMail { get; set; }
        public string? Telephone {  get; set; }
        public List<string> HistoriqueMembre { get; set; } = new();
    }
}
