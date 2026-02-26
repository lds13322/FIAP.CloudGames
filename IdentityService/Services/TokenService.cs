using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Models;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Services
{
    public class TokenService
    {
        // Chave secreta para assinar o token (Num ambiente real, ficaria no Azure Key Vault)
        private const string SecretKey = "AgroSolutions_Hackathon_Secret_Key_Super_Safe_2026";

        public string GenerateToken(UserLogin user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            // ADEQUAÇÃO LGPD: Não trafegamos dados sensíveis no token, apenas o essencial (Email/Role)
            // As senhas em banco estariam armazenadas com Hash (ex: BCrypt).
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "ProdutorRural")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}