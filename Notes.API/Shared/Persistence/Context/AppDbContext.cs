using Microsoft.EntityFrameworkCore;
using Notes.API.Notes.Domain.Models;
using Notes.API.Security.Models;
using Notes.API.Shared.Extensions;

namespace Notes.API.Shared.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>().HasKey(user => user.UserId);
        modelBuilder.Entity<User>().Property(user => user.UserId).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(user => user.Firstname);
        modelBuilder.Entity<User>().Property(user => user.Lastname);
        modelBuilder.Entity<User>().Property(user => user.Email);
        modelBuilder.Entity<User>().Property(user => user.Password);

        modelBuilder.Entity<Note>().ToTable("Notes");
        modelBuilder.Entity<Note>().HasKey(note => note.NoteId);
        modelBuilder.Entity<Note>().Property(note => note.NoteId).ValueGeneratedOnAdd();
        modelBuilder.Entity<Note>().Property(note => note.Title);
        modelBuilder.Entity<Note>().Property(note => note.Content);
        modelBuilder.Entity<Note>().Property(note => note.Archived);
        

        modelBuilder.UseSnakeCaseConvention();
    }
}