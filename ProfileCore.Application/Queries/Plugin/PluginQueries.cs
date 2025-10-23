using MediatR;
using ProfileCore.Application.Dtos;

namespace ProfileCore.Application.Queries.Plugin;

public record QueryCompanyPlugins(Guid Id, Guid UserId) : IRequest<IEnumerable<PluginDto>>;

public record QueryAllPlugins() : IRequest<IEnumerable<PluginDto>>;