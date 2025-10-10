using ProfileCore.Domain.Aggregate;

namespace ProfileCore.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}