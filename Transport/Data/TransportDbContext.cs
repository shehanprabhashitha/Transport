using Microsoft.EntityFrameworkCore;
using Transport.Models.Domain;

namespace Transport.Data
{
    public class TransportDbContext : DbContext
    {
        public TransportDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.Domain.Route> Routes { get; set; }
    }
}
