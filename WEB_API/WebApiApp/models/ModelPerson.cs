namespace WebApiApp.Models;
public class ModelPerson
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }

    // for signin
    public string? Username { get; set; }
    public string? Password { get; set; }
}