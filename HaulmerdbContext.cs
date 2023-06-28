using Microsoft.EntityFrameworkCore;
using Haulmer_3.Models;

public class HaulmerDbContext : DbContext
{
    public HaulmerDbContext(DbContextOptions<HaulmerDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .HasKey(u => new { u.Nombre, u.Apellido });
    }
}