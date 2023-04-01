using Microsoft.EntityFrameworkCore;
using Parcial2_CarmonaSantiago.DAL.Entities;

namespace Parcial2_CarmonaSantiago.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
