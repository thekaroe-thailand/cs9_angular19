namespace WebApiApp.Models;

public class BillSaleDetailModel {
    public int Id { get; set; }
    public int BillSaleId { get; set; }
    public BillSaleModel? BillSale { get; set; }
    public int BookId { get; set; }
    public BookModel? Book { get; set; }
    public int Qty { get; set; }
    public int Price { get; set; }
    public int Total { get; set; }
}
