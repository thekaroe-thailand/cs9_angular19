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
    [Route("[action]/{id}")]
    public async Task<ActionResult> Update(int id, CompanyModel companyModel)
    {
        var company = await _context.CompanyModel.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
        {
            return NotFound();
        }
        company.Name = companyModel.Name;
        company.Address = companyModel.Address;
        company.Phone = companyModel.Phone;
        company.Email = companyModel.Email;
        company.TaxId = companyModel.TaxId;
        await _context.SaveChangesAsync();
        return Ok();
    }
}