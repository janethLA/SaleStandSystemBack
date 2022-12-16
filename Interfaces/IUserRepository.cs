using MySalesStandSystem.Models;
using MySalesStandSystem.Output;

namespace MySalesStandSystem.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        User GetUserById(int id);
        IEnumerable<User> GetUsers();
        Task<bool> UpdateUserAsync(User user);
        List<SalesStandOutput> getSaleStandsByUser(int id);
    }
}
