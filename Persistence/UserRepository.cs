using Backend.Models;

namespace Backend.Persistence;

public class UserRepository : IUserRepository
{
    private readonly CarGoDbContext _context;
    public UserRepository(CarGoDbContext context)
    {
        _context = context;
    }
    
    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<User> GetUserById(int id, bool includeRelated = true)
    {
        if (!includeRelated)
            return await _context.Users.FindAsync(id);
        // TO DO: finish this method
        return await _context.Users.FindAsync(id);
    }
}