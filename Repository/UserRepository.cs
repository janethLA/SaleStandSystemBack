using Microsoft.EntityFrameworkCore;
using MySalesStandSystem.Data;
using MySalesStandSystem.Interfaces;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;
using MySalesStandSystem.Utils;

namespace MySalesStandSystem.Repository
{
    public class UserRepository:IUserRepository
    {
        protected readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<User> GetUsers()
        {
            return _context.users.Include(c => c.salesStands).ToList();
        }

        public User GetUserById(int id)
        {
            var user = _context.users.Where(c => c.id == id).Include(c => c.salesStands).First();
            return user;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            user.password = Encrypt.getSHA256(user.password);
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user is null)
            {
                return false;
            }
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
        public List<SalesStandOutput> getSaleStandsByUser(int id)
        {
            List<SalesStandOutput> salesByUser = new List<SalesStandOutput>();
            User user = GetUserById(id);
            IEnumerable<SalesStand> listFind = user.salesStands.Reverse();
            foreach (SalesStand sale in listFind)
            {
                SalesStandOutput s = new SalesStandOutput();
                s.id = sale.id;
                s.salesStandName = sale.salesStandName;
                s.description = sale.description;
                s.image = sale.image;
                s.address = sale.address;
                salesByUser.Add(s);
            }
            return salesByUser;
        }

    }
}
