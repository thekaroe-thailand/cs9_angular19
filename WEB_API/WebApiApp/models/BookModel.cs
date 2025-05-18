namespace WebApiApp.Models;

public class BookModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Isbn { get; set; }
    public int Price { get; set; }
    public int PublisherId { get; set; }
    public PublisherModel? Publisher { get; set; } // N:1
    public StockModel? Stock { get; set; } // 1:1
    public List<StockModel>? Stocks { get; set; } // 1:N

}
