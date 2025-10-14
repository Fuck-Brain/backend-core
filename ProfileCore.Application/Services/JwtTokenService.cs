using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProfileCore.Application.Services.Interfaces;

namespace ProfileCore.Application.Services
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly IConfiguration _configuration;

		public JwtTokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(Guid userId, string email, string role)
		{
			var jwtSection = _configuration.GetSection("Jwt");
			string secretKey = jwtSection["SecretKey"]!;
			string issuer = jwtSection["Issuer"]!;
			string audience = jwtSection["Audience"]!;
			int expireMinutes = int.Parse(jwtSection["AccessTokenExpireMinutes"]!);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(ClaimTypes.Email, email),
				new Claim(ClaimTypes.Role, role)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(expireMinutes),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}