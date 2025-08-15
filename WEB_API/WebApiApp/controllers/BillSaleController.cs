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

    [HttpGet]
    [Route("[action]/{month}/{year}")]
    public async Task<ActionResult<IEnumerable<object>>> Dashboard(int month, int year)
    {
        try
        {
            var totalBillSale = await _context.BillSaleModel.Where(b => b.CreatedAt.Month == month && b.CreatedAt.Year == year).CountAsync();
            var totalBookSale = await _context.BillSaleDetailModel.CountAsync();
            var totalPublisher = await _context.PublisherModel.CountAsync();
            var sumBookInStock = await _context.StockModel.SumAsync(b => b.Quantity);

            var arrSumSalePerDay = new List<int>();
            var totalDaysInMonth = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= totalDaysInMonth; i++)
            {
                var sumSalePerDay = await _context.BillSaleModel
                    .Where(b => b.CreatedAt.Day == i && b.CreatedAt.Month == month && b.CreatedAt.Year == year)
                    .SumAsync(b => b.Amount);
                //sumSalePerDay = Random.Shared.Next(1000, 10000);
                arrSumSalePerDay.Add(sumSalePerDay);
            }

            return Ok(new
            {
                totalBillSale = totalBillSale,
                totalBookSale = totalBookSale,
                totalPublisher = totalPublisher,
                sumBookInStock = sumBookInStock,
                arrSumSalePerDay = arrSumSalePerDay
            });
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }
}

