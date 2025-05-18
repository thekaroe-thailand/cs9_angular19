using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly BookContext _context;

    public StockController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<StockModel>>> List()
    {
        var results = await _context.StockModel.Select(item => new {
            item.Id,
            item.BookId,
            item.Quantity,
            item.Price,
            item.CreatedDate,
            item.Remark,
            item.Book,
        }).ToListAsync();
        return Ok(results);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Create(StockModel stock)
    {
        await _context.StockModel.AddAsync(stock);
        await _context.SaveChangesAsync();
        return Ok(new {message = "success"});
    }
}