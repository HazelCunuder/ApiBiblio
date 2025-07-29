using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class MembreDTO
    {
        public int Id { get; set; }
        public required string NomMembre { get; set; }
        public required string PrenomMembre { get; set; }
        public required string MdpMembre { get; set; }
        public string AdressePostale { get; set; }
        public required string AdresseMail { get; set; }
        public string? Telephone { get; set; }

        public string HistoriqueMembre { get; set; }

        public MembreDTO() { }
        public MembreDTO(Membre MembreItem) =>

        (Id, NomMembre, PrenomMembre, Telephone, AdresseMail, MdpMembre, AdressePostale, HistoriqueMembre) = (MembreItem.Id, MembreItem.NomMembre, MembreItem.PrenomMembre, MembreItem.Telephone, MembreItem.AdresseMail, MembreItem.MdpMembre, MembreItem.AdressePostale, MembreItem.HistoriqueMembre);
    }
}
