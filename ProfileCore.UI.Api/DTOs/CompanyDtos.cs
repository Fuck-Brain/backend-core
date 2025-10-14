namespace ProfileCore.UI.Api.DTOs;

public record CompanyDto(
	Guid Id,
	string Name,
	Guid OwnerId,
	List<Guid> PluginIds,
	List<Guid> EmployeeIds
);

public record CompanyCreateRequest(
	string Name,
	Guid OwnerId
);

public record CompanyUpdateRequest(
	Guid Id,
	string Name
);

public record CompanyCreateResponse(CompanyDto Company);