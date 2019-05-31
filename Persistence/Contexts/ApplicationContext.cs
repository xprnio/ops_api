using Microsoft.EntityFrameworkCore;
using OPS_API.Domain.Models;

namespace OPS_API.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rescuer> Rescuers { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentRequest> EquipmentRequests { get; set; }

        public ApplicationContext(DbContextOptions opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");
            builder.HasPostgresExtension("postgis");

            builder.Entity<Operation>()
                .OwnsOne(o => o.MissingPerson);
        }
    }
}