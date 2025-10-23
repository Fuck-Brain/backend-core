namespace ProfileCore.UI.Api.DTOs;

public record PluginCreateRequest(
	string Name, 
	string? Description);

public record PluginCompanyRequest(
	Guid CompanyId, 
	Guid PluginId);