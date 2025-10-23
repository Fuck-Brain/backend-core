namespace ProfileCore.UI.Api.DTOs;

public record ProfileDto(
	string? Name,
	string? Surname,
	string? FatherName,
	string? DisplayName,
	string? Bio,
	DateTime CreatedAt
);

public record ProfileCreateRequest(
	Guid Id,
	string? Name,
	string? Surname,
	string? FatherName,
	string? DisplayName,
	string? Bio
);

public record ProfileUpdateRequest(
	string? Name,
	string? Surname,
	string? FatherName,
	string? DisplayName,
	string? Bio
);

public record ProfileCreateResponse(ProfileDto Profile);
