using Microsoft.EntityFrameworkCore;
using HelloApi4.Models;

namespace HelloApi4.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> People => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(e =>
        {
            e.ToTable("people");
            e.Property(p => p.Id).ValueGeneratedOnAdd();
            e.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            e.Property(p => p.Apellido).HasMaxLength(100).IsRequired();
            e.Property(p => p.Edad).IsRequired();
        });
    }
}
