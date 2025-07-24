namespace WebApiApp.Models;

public class BillSaleModel
{
    public int Id { get; set; }
    public string Status { get; set; } = "";

    public int Amount { get; set; }
    public int ReceiveAmount { get; set; }

    public DateTime CreatedAt { get; set; }
    public List<BillSaleDetailModel>? BillSaleDetails { get; set; }
}
