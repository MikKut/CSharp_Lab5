namespace Domain.Common.Abstract;

public abstract class BaseDomainObject
{
    public Guid Id { get; }

    public BaseDomainObject(Guid id)
    {
        Id = id;
    }
}