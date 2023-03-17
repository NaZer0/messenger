using Messenger.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Messang> Messangs { get; set; }
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost; port=5432; user id=postgres; password=Iridik228; database=messenger;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Messang>().HasKey(u => u.Id);
    }
}