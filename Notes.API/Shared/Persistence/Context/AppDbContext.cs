using Microsoft.EntityFrameworkCore;
using Notes.API.Security.Models;
using Notes.API.Shared.Extensions;

namespace Notes.API.Shared.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>().HasKey(user => user.UserId);
        modelBuilder.Entity<User>().Property(user => user.UserId);
        modelBuilder.Entity<User>().Property(user => user.Firstname);
        modelBuilder.Entity<User>().Property(user => user.Lastname);
        modelBuilder.Entity<User>().Property(user => user.Email);
        modelBuilder.Entity<User>().Property(user => user.Password);
        
        modelBuilder.UseSnakeCaseConvention();
    }
}