namespace ProfileCore.Domain.DomainAbstract;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
}