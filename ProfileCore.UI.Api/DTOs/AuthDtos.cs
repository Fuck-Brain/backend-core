using System.ComponentModel.DataAnnotations;
namespace ProfileCore.UI.Api.DTOs;

public class RegisterRequest
{
	[Required] public string Login { get; set; }
	[Required, EmailAddress] public string Email { get; set; }
	[Required, MinLength(6)] public string Password { get; set; }
}

public record LoginRequest(
	string? Login,
	[EmailAddress] string? Email,
	[Required] string Password
);
public record AuthResponse(
	string AccessToken, 
	string RefreshToken
);

public record RefreshRequest(
	Guid	UserId,
	string	RefreshToken
);