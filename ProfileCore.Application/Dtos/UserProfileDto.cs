namespace ProfileCore.Application.Dtos;

public record UserProfileDto(
	string DisplayName,
	string? Name, 
	string? Surname, 
	string? FatherName, 
	string? Bio, 
	DateTime CreatedAt);