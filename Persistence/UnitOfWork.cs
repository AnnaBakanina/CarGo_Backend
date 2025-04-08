namespace Backend.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarGoDbContext _context;
    public UnitOfWork(CarGoDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}