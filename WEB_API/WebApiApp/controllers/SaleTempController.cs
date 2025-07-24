namespace WebApiApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SaleTempController : ControllerBase
{
    private readonly BookContext _context;

    public SaleTempController(BookContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Create(SaleTempModel saleTemp)
    {
        string isbn = saleTemp.Isbn;

        BookModel? book = await _context.BookModel.FirstOrDefaultAsync(b => b.Isbn == isbn);

        if (book == null)
        {
            return NotFound("Book not found");
        }

        SaleTempModel? saleTempRow = await _context.SaleTempModel.FirstOrDefaultAsync(s => s.Isbn == isbn);

        if (saleTempRow == null)
        {
            // insert new row
            saleTemp.Qty = 1;
            saleTemp.Name = book.Name ?? "";
            saleTemp.Price = book.Price;
            saleTemp.Total = saleTemp.Price * saleTemp.Qty;

            _context.SaleTempModel.Add(saleTemp);
        }
        else
        {
            // update existing row
            saleTempRow.Qty = saleTempRow.Qty + 1;
            saleTempRow.Total = saleTempRow.Price * saleTempRow.Qty;

            _context.SaleTempModel.Update(saleTempRow);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<SaleTempModel>>> List()
    {
        var results = await _context.SaleTempModel.OrderBy(s => s.Id).ToListAsync();

        return Ok(results);
    }

    [HttpDelete]
    [Route("[action]/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var saleTemp = await _context.SaleTempModel.FindAsync(id);

        if (saleTemp == null)
        {
            return NotFound("SaleTemp not found");
        }

        _context.SaleTempModel.Remove(saleTemp);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    [Route("[action]/{id}")]
    public async Task<ActionResult> UpdateQty(int id, SaleTempModel saleTemp)
    {
        var saleTempRow = await _context.SaleTempModel.FindAsync(id);

        if (saleTempRow == null)
        {
            return NotFound("SaleTemp not found");
        }

        saleTempRow.Qty = saleTemp.Qty;
        saleTempRow.Total = saleTempRow.Price * saleTempRow.Qty;

        _context.SaleTempModel.Update(saleTempRow);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> EndSale(EndSaleModel endSale)
    {
        try
        {
            // step 1 create bill sale
            var billSaleModel = new BillSaleModel();
            billSaleModel.Status = "paid";
            billSaleModel.CreatedAt = DateTime.UtcNow;
            billSaleModel.Amount = endSale.amount;
            billSaleModel.ReceiveAmount = endSale.receiveAmount;
            _context.BillSaleModel.Add(billSaleModel);
            await _context.SaveChangesAsync();

            // step 2 create bill sale detail from sale temp
            var saleTempList = await _context.SaleTempModel.ToListAsync();

            foreach (var saleTemp in saleTempList)
            {
                // get book from book model by isbn
                var book = await _context.BookModel.FirstOrDefaultAsync(b => b.Isbn == saleTemp.Isbn);

                if (book != null)
                {
                    var billSaleDetailModel = new BillSaleDetailModel();
                    billSaleDetailModel.BillSaleId = billSaleModel.Id;
                    billSaleDetailModel.BookId = book.Id;
                    billSaleDetailModel.Qty = saleTemp.Qty;
                    billSaleDetailModel.Price = book.Price;
                    billSaleDetailModel.Total = book.Price * saleTemp.Qty;

                    _context.BillSaleDetailModel.Add(billSaleDetailModel);
                }
            }

            await _context.SaveChangesAsync();

            // step 3 delete all sale temp
            _context.SaleTempModel.RemoveRange(_context.SaleTempModel);
            await _context.SaveChangesAsync();

            // response status code 200
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}
