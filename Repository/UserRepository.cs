using Microsoft.EntityFrameworkCore;
using MySalesStandSystem.Data;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;

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
            //return _context.categories.Find(id);
            var user = _context.users.Where(c => c.id == id).Include(c => c.salesStands).First();
            return user;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.users.AddAsync(user);
            //await _context.Set<Cow>().AddAsync(cow);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            //_context.categories.Update(category);
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            //var entity = await GetByIdAsync(id);
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
