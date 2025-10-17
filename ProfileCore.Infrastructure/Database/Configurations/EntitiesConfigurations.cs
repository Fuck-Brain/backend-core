using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Infrastructure.Database.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.HasMany(c => c.PluginConnections)
            .WithOne(pc => pc.Company)
            .HasForeignKey(pc => pc.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId);
        
        builder.HasOne(e => e.User)
            .WithMany(u => u.UserWorks)
            .HasForeignKey(e => e.UserId);
    }
}

public class PluginConfiguration : IEntityTypeConfiguration<Plugin>
{
    public void Configure(EntityTypeBuilder<Plugin> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.HasMany(p => p.Connections)
            .WithOne(p => p.Plugin)
            .HasForeignKey(p => p.PluginId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PluginConnectionConfiguration : IEntityTypeConfiguration<PluginConnection>
{
    public void Configure(EntityTypeBuilder<PluginConnection> builder)
    {
        builder.HasKey(pc => pc.Id);

        builder.HasOne(pc => pc.Plugin)
            .WithMany(p => p.Connections)
            .HasForeignKey(pc => pc.PluginId);
        
        builder.HasOne(pc => pc.Company)
            .WithMany(c => c.PluginConnections)
            .HasForeignKey(pc => pc.CompanyId);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.HasIndex(u => u.Login)
            .IsUnique();
        
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.Login).IsRequired();
        builder.Property(u => u.HashPassword).IsRequired();
        
        builder.HasMany(u => u.UserWorks)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<UserProfile>(up => up.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(up => up.DisplayName).IsRequired();
    }
}