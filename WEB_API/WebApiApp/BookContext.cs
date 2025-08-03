using Microsoft.EntityFrameworkCore;
using WebApiApp.Models;

namespace WebApiApp;
public class BookContext : DbContext
{
    public DbSet<BookModel> BookModel { get; set; }
    public DbSet<PublisherModel> PublisherModel { get; set; }
    public DbSet<ModelPerson> ModelPerson { get; set; }
    public DbSet<StockModel> StockModel { get; set; }
    public DbSet<SaleTempModel> SaleTempModel { get; set; }

    public DbSet<BillSaleModel> BillSaleModel { get; set; }
    public DbSet<BillSaleDetailModel> BillSaleDetailModel { get; set; }
    public DbSet<CompanyModel> CompanyModel { get; set; }

    public BookContext(DbContextOptions<BookContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublisherModel>()
            .HasMany(p => p.Books)  // 1:N
            .WithOne(b => b.Publisher) // N:1
            .HasForeignKey(b => b.PublisherId);

        modelBuilder.Entity<BookModel>()
            .HasMany(b => b.Stocks)
            .WithOne(s => s.Book)
            .HasForeignKey(s => s.BookId);

        modelBuilder.Entity<StockModel>()
            .HasOne(s => s.Book)
            .WithMany(b => b.Stocks)
            .HasForeignKey(s => s.BookId);

        modelBuilder.Entity<BillSaleModel>()
            .HasMany(b => b.BillSaleDetails)
            .WithOne(b => b.BillSale)
            .HasForeignKey(b => b.BillSaleId);

        modelBuilder.Entity<BillSaleDetailModel>()
            .HasOne(b => b.BillSale)
            .WithMany(b => b.BillSaleDetails)
            .HasForeignKey(b => b.BillSaleId);
    }

}

