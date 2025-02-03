using Microsoft.EntityFrameworkCore;
using WebApiApp.Models;

namespace WebApiApp;
public class BookContext : DbContext
{
    public DbSet<BookModel> BookModel { get; set; }
    public DbSet<PublisherModel> PublisherModel { get; set; }
    
    public BookContext(DbContextOptions<BookContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublisherModel>()
            .HasMany(p => p.Books)  // 1:N
            .WithOne(b => b.Publisher) // N:1
            .HasForeignKey(b => b.PublisherId);
    }

}

