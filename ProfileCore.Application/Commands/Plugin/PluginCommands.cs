using System.Windows.Input;
using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Commands.Plugin;

public record AddCompanyPluginCommand(Guid CompanyId, Guid PluginId) : IRequest;

public record DeleteCompanyPluginCommand(Guid CompanyId, Guid PluginId) : IRequest;

public record AddPluginCommand(string Name, string? Description) : IRequest<PluginDto>;

public record DeletePluginCommand(Guid Id) : IRequest;