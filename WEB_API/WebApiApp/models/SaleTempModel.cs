namespace WebApiApp.Models;

public class SaleTempModel {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Isbn { get; set; } = "";
    public int Price { get; set; }
    public int Qty { get; set; }
    public int Total { get; set; }
}

