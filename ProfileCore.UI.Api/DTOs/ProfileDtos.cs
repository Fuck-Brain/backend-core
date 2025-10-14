namespace ProfileCore.UI.Api.DTOs;

public record ProfileDto(
	Guid Id,
	Guid UserId,
	string? FirstName,
	string? LastName,
	string? DisplayName,
	string? Bio,
	DateTime CreatedAt
);

public record ProfileCreateRequest(
	Guid UserId,
	string? FirstName,
	string? LastName,
	string? DisplayName,
	string? Bio
);

public record ProfileUpdateRequest(
	string? FirstName,
	string? LastName,
	string? DisplayName,
	string? Bio
);

public record ProfileCreateResponse(ProfileDto Profile);
