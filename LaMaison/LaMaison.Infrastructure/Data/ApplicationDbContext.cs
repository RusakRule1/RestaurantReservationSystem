using LaMaison.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LaMaison.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.ReferenceCode)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.HasIndex(r => r.ReferenceCode)
                  .IsUnique();

            entity.Property(r => r.FullName)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(r => r.Email)
                  .IsRequired()
                  .HasMaxLength(254);

            entity.Property(r => r.PhoneNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(r => r.SpecialRequests)
                  .HasMaxLength(500);

            entity.Property(r => r.Status)
                  .HasConversion<string>();
        });
    }
}
