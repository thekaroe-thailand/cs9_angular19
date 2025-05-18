using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookContext _context;

    public BookController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<BookModel>>> List()
    {
        var results = await _context.BookModel.Select(item => new {
            item.Id,
            item.Name,
            item.Price,
            item.Isbn,
            item.Publisher
        }).ToListAsync(); 

        return Ok(results);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookModel>> Info(int id)
    {
        var book = await _context.BookModel.FindAsync(id);

        if (book == null) return NotFound();

        return book;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Create(BookModel book)
    {
        _context.BookModel.Add(book);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut()]
    [Route("[action]/{id}")]
    public async Task<ActionResult> Update(int id, BookModel book)
    {
        try {
            // update book
            BookModel? findBook = await _context.BookModel.FindAsync(id);

            if (findBook == null) return NotFound();

            findBook.Name = book.Name;
            findBook.Price = book.Price;
            findBook.Isbn = book.Isbn;
            findBook.PublisherId = book.PublisherId;

            _context.BookModel.Update(findBook);

            await _context.SaveChangesAsync();
        } catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return Ok(new {message = "success"});
    }

    [HttpDelete()]
    [Route("[action]/{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        var book = await _context.BookModel.FindAsync(id);

        if (book == null) return NotFound();

        _context.BookModel.Remove(book);
        await _context.SaveChangesAsync(); // บันทึกการลบ

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<PublisherModel>>> PublisherList()
    {
        var results = await _context.PublisherModel.Select(b => new {
            b.Id,
            b.Name,
            b.Address,
            books = b.Books
        }).ToListAsync();

        return Ok(results);
    }

}

