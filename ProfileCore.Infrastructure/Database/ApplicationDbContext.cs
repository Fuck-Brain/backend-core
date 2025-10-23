using Microsoft.EntityFrameworkCore;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Plugin> Plugins { get; set; }
        public DbSet<PluginConnection> PluginConnections { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }


        public override int SaveChanges()
        {
            var addedOrModifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in addedOrModifiedEntries)
            {
                if (entry.Entity is Auditable auditableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditableEntity.CreatedAt = DateTime.UtcNow;
                    }
                    auditableEntity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
