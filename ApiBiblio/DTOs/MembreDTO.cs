using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class MembreDTO
    {
        public class MembreDto
        {
            public int Id { get; set; }
            public string NomMembre { get; set; }
            public string PrenomMembre { get; set; }
            public string AdresseMail { get; set; }
            public string Telephone { get; set; }
            public string AdressePostale { get; set; }
        }

        public class CreateMembreDto
        {
            public string NomMembre { get; set; }
            public string PrenomMembre { get; set; }
            public string AdresseMail { get; set; }
            public string MdpMembre { get; set; }
            public string Telephone { get; set; }
            public string AdressePostale { get; set; }
        }

        public class UpdateMembreDto
        {
            public string NomMembre { get; set; }
            public string PrenomMembre { get; set; }
            public string AdresseMail { get; set; }
            public string Telephone { get; set; }
            public string AdressePostale { get; set; }
        }
    }
}
