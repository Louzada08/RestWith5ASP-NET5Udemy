namespace RestWithASPNET.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<bool> Commit();
    bool DatabaseExists();
}
