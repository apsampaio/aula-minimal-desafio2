using API.Model;

using Microsoft.EntityFrameworkCore;

namespace API.Infra.Context;

public class DatabaseContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite(@"DataSource=app.db;Cache=Shared");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Package>().HasKey(p => p.id);
        modelBuilder.Entity<Details>().HasKey(d => d.id);

        modelBuilder.Entity<Package>()
        .HasOne<Details>(p => p.Details)
        .WithOne(d => d.Package)
        .HasForeignKey<Details>(d => d.packageId);
    }

    public DbSet<Package> Packages { get; set; } = default!;
    public DbSet<Details> Details { get; set; } = default!;

}