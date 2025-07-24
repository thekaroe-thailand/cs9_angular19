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

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<StockModel>>> SumPerProduct()
    {
        var results = await _context.StockModel.GroupBy(item => item.BookId)
        .Select(group => new {
            Id = group.Key,
            Name = group.First().Book.Name,
            Isbn = group.First().Book.Isbn,
            Total = group.Sum(item => item.Quantity)
        }).ToListAsync();

        return Ok(results);
    }

    [HttpGet]
    [Route("[action]/{productId}")]
    public async Task<ActionResult<IEnumerable<StockModel>>> GetByProductId (int productId)
    {
        var results = await _context.StockModel.Where(item => item.BookId == productId)
        .Select(item => new {
            item.Id,
            item.Quantity,
            item.Price,
            item.CreatedDate,
            item.Remark,
            item.Book,
        }).ToListAsync();

        return Ok(results);
    }

    [HttpDelete]
    [Route("[action]/{id}")]
    public async Task<ActionResult> Delete(int id) {
        var stock = await _context.StockModel.FindAsync(id);
        if (stock == null) {
            return NotFound("Stock not found");
        }

        _context.StockModel.Remove(stock);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut]
    [Route("[action]/{id}")]
    public async Task<ActionResult> Update(int id, StockModel stock) {
        var stockForUpdate = await _context.StockModel.FindAsync(id);

        if (stockForUpdate == null) {
            return NotFound("Stock not found");
        }

        stockForUpdate.BookId = stock.BookId;
        stockForUpdate.Quantity = stock.Quantity;
        stockForUpdate.Price = stock.Price;
        stockForUpdate.CreatedDate = stock.CreatedDate;
        stockForUpdate.Remark = stock.Remark;

        _context.StockModel.Update(stockForUpdate);
        await _context.SaveChangesAsync();

        return Ok();
    }
}