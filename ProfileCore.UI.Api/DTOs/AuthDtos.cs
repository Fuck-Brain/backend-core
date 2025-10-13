using System.ComponentModel.DataAnnotations;
namespace ProfileCore.UI.Api.DTOs;

public record RegisterRequest(
	[Required, EmailAddress] string Email,
	[Required, MinLength(6)] string Password,
	[Required] string FirstName,
	[Required] string LastName,
	[Required] string FatherName,
	string Role = "User"
);

public record LoginRequest(
	[Required, EmailAddress] string Email,
	[Required] string Password
);
public record AuthResponse(
	string AccessToken, 
	string RefreshToken
);

public record RefreshRequest(string RefreshToken);

public record RegisterResponse(
	UserDto User,
	AuthResponse Tokens
);