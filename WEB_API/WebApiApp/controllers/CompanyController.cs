namespace WebApiApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class CompanyController : ControllerBase
{
    private readonly BookContext _context;

    public CompanyController(BookContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Create(CompanyModel company)
    {
        _context.CompanyModel.Add(company);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<CompanyModel>> Info()
    {
        var company = await _context.CompanyModel.FirstOrDefaultAsync();
        return Ok(company);
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<ActionResult> Update(CompanyModel company)
    {
        _context.CompanyModel.Update(company);
        await _context.SaveChangesAsync();
        return Ok();
    }
}