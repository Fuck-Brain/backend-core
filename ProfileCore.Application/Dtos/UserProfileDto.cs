namespace ProfileCore.Application.Dtos;

public record UserProfileDto(string? Name, string? Surname, string? FathersName, string? Bio, DateTime CreatedAt);