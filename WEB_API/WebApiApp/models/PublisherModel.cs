namespace WebApiApp.Models;

public class PublisherModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public List<BookModel>? Books { get; set; } // 1:N
}
