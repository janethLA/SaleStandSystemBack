using Microsoft.EntityFrameworkCore;
using MySalesStandSystem.Models;

namespace MySalesStandSystem.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<SalesStand> salesStands { get; set; }
        public DbSet<User> users { get; set; }

    }
}
