using KampusPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace KampusPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}