namespace ProfileCore.Application.Dtos;

public record UserProfileDto(string? Name, 
							 string? Surname, 
							 string? FatherName, 
							 string? Bio, 
							 DateTime CreatedAt);