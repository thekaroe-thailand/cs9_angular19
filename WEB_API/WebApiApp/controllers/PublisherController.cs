namespace WebApiApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp.Models;

[Route("api/[controller]")]
[ApiController]
public class PublisherController : ControllerBase {
    private readonly BookContext _context;

    public PublisherController(BookContext context) {
        _context = context;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<PublisherModel>>> List() {
        var results = await _context.PublisherModel.ToListAsync();
        return Ok(results);
    }
}   
