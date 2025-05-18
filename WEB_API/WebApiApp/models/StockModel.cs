namespace WebApiApp.Models;
public class StockModel
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int Price { get; set; } // ราคาซื้อเข้า
    public int Quantity { get; set; } 
    public string? Remark { get; set; }
    public BookModel? Book { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<BookModel>? Books { get; set; } // 1:N
}

