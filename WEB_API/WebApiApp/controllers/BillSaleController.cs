namespace WebApiApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class BillSaleController : ControllerBase
{
    private readonly BookContext _context;

    public BillSaleController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<BillSaleModel>>> List()
    {
        var billSales = await _context.BillSaleModel.ToListAsync();
        return Ok(billSales);
    }

    [HttpGet]
    [Route("[action]/{billSaleId}")]
    public async Task<ActionResult<BillSaleDetailModel>> BillSaleDetails(int billSaleId)
    {
        var billSaleDetails = await _context.BillSaleDetailModel
            .Where(b => b.BillSaleId == billSaleId)
            .Include(b => b.Book)
            .OrderByDescending(b => b.Id)
            .ToListAsync();

        return Ok(billSaleDetails);
    }

}

