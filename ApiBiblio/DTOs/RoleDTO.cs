using ApiBiblio.Models;

namespace ApiBiblio.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public required string NomRole { get; set; }
        public RoleDTO() { }
        public RoleDTO(Role role) =>
            (Id, NomRole) = (role.Id, role.NomRole);
    }
}
