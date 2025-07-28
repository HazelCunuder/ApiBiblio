namespace ApiBiblio.DTOs
{
    public class AuthDto
    {
        public class LoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponseDto
        {
            public string Token { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public DateTime Expiration { get; set; }
        }

        public class CreateEmployeDto
        {
            public string NomEmploye { get; set; }
            public string PrenomEmploye { get; set; }
            public string LoginEmploye { get; set; }
            public string MdpEmploye { get; set; }
            public int IdRole { get; set; }
        }
    }
}
