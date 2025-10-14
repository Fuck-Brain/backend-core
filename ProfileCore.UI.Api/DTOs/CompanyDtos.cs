namespace ProfileCore.UI.Api.DTOs;

public record CompanyDto(
	Guid Id,
	string Name,
	Guid OwnerId,
	string OwnerFullName,
	List<Guid> PluginIds,
	List<Guid> EmployeeIds
);

public record CompanyCreateRequest(
	string Name,
	Guid OwnerId
);

public record CompanyUpdateRequest(
	string Name
);

public record CompanyCreateResponse(CompanyDto Company);