using ProfileCore.Domain.Aggregate;

namespace ProfileCore.Domain.Service
{
    public class OrderDomainService
    {
        public bool CanOrderBeShipped(Order order)
        {
            return order.CreationDate <= DateTime.Now;
        }
    }
}