using Backend.Models;

namespace Backend.Persistence;

public interface IUserRepository
{
    void AddUser(User user);
    Task<User> GetUserById(int id, bool includeRelated = true);
}