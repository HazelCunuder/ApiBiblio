using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class EmployeDTO
    {
        public int Id { get; set; }
        public required string NomEmploye { get; set; }
        public required string PrenomEmploye { get; set; }
        public required string LoginEmploye { get; set; }
        public required string MdpEmploye { get; set; }

        public EmployeDTO() { }
        public EmployeDTO(Employe employe) =>
            (Id, NomEmploye, PrenomEmploye, LoginEmploye, MdpEmploye) = (employe.Id, employe.NomEmploye, employe.PrenomEmploye, employe.LoginEmploye, employe.MdpEmploye);
    }
}

