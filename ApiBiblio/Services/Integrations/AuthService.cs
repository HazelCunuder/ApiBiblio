using Microsoft.EntityFrameworkCore;
using ApiBiblio.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ApiBiblio.DTOs.AuthDto;

namespace ApiBiblio.Services.Integrations
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeService _employeService;
        private readonly IConfiguration _configuration;

        public AuthService(IEmployeService employeService, IConfiguration configuration)
        {
            _employeService = employeService;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Valider les credentials
            var isValid = await _employeService.ValidateCredentialsAsync(loginDto.Email, loginDto.Password);
            if (!isValid)
                throw new UnauthorizedAccessException("Email ou mot de passe incorrect");

            // Récupérer le rôle
            var role = await _employeService.GetRoleAsync(loginDto.Email);

            // Générer le token
            var token = await GenerateJwtTokenAsync(loginDto.Email, role);

            return new LoginResponseDto
            {
                Token = token,
                Email = loginDto.Email,
                Role = role,
                Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"]))
            };
        }

        public async Task<string> GenerateJwtTokenAsync(string email, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                await tokenHandler.ValidateTokenAsync(token, validationParameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}