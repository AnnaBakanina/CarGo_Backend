using Backend.Models;
using Microsoft.EntityFrameworkCore;

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
    
    public void RemoveUser(User user)
    {
        _context.Users.Remove(user);
    }
    
    /*
     * includeRelated має вирішувати, чи підвантажувати зв’язані сутності (наприклад, ролі, адреси, замовлення, тощо).
     */
    public async Task<User> GetUserById(int id, bool includeRelated = true)
    {
        if (!includeRelated)
            return await _context.Users.FindAsync(id);

        return await _context.Users
            // .Include(v => v.Vehicles)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new InvalidOperationException();
    }
}