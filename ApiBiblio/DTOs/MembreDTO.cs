using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class MembreDTO
    {
        public int Id { get; set; }
        public string? NomMembre { get; set; }
        public string? PrenomMembre { get; set; }
        public string? MdpMembre { get; set; }
        public string? AdressePostale { get; set; }
        public string? AdresseMail { get; set; }
        public string? Telephone { get; set; }

        public List<string> HistoriqueMembre { get; set; } = new();

        public MembreDTO() { }
        public MembreDTO(Membre MembreItem) =>

        (Id, NomMembre, PrenomMembre, Telephone, AdresseMail, MdpMembre, AdressePostale, HistoriqueMembre) = (MembreItem.Id, MembreItem.NomMembre, MembreItem.PrenomMembre, MembreItem.Telephone, MembreItem.AdresseMail, MembreItem.MdpMembre, MembreItem.AdressePostale, MembreItem.HistoriqueMembre);
    }
}
