namespace ProfileCore.UI.Api.DTOs;

public record UserDto(
	Guid Id,
	string Email,
	string FirstName,
	string LastName,
	string FatherName,
	string Role
);