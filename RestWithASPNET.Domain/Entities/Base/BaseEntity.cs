using RestWithASPNET.Domain.Interfaces.Base;

namespace RestWithASPNET.Domain.Entities.Base;

public class BaseEntity : IBase
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public BaseEntity(DateTime createdAt) : this()
    {
        CreatedAt = createdAt;
    }

    public BaseEntity(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }
    public DateTime? CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime? UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime? DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
