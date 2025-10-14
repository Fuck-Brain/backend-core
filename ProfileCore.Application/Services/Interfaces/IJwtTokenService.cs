namespace ProfileCore.Application.Services.Interfaces;

public interface IJwtTokenService
{
	/// <summary>
	/// Генерирует access-токен с заданным временем жизни.
	/// </summary>
	/// <returns>Строковое представление токена</returns>
	string GenerateToken(Guid userId, string email, string role);
}