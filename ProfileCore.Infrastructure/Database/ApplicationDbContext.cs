using Microsoft.EntityFrameworkCore;
using ProfileCore.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<UserEntity> users;
        DbSet<EmployeeEntity> employees;
        DbSet<CompanyEntity> companies;
        DbSet<PluginEntity> plugins;

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
                if (entry.Entity is IAuditable auditableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditableEntity.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        auditableEntity.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
