namespace Backend.Persistence;

public interface IUnitOfWork
{
    Task CompleteAsync();
}