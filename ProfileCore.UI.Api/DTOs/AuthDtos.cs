using System.ComponentModel.DataAnnotations;
namespace ProfileCore.UI.Api.DTOs;

public class RegisterRequest
{
	[Required, EmailAddress] public string Email { get; set; }
	[Required, MinLength(6)] public string Password { get; set; }
	[Required] public string FirstName { get; set; }
	[Required] public string LastName { get; set; }
	[Required] public string FatherName { get; set; }
	public string Role { get; set; } = "User";
}

public record LoginRequest(
	[Required, EmailAddress] string Email,
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