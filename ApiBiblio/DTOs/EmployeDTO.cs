namespace ApiBiblio.DTOs
{
    public class EmployeDTO
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}
