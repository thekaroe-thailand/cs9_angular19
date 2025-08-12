namespace WebApiApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;

// เพิ่มการใช้งาน JWT
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// เพิ่มการใช้งาน Authorization
using Microsoft.AspNetCore.Authorization;

// เพิ่มการใช้งาน Entity Framework Core
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")] // localhost:5000/api/person
public class PersonController : ControllerBase
{
    private readonly BookContext _context;

    public PersonController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult HelloWorld()
    {
        return Ok("Hello World");
    }

    [HttpGet("[action]/{id}")]
    public IActionResult GetPersonById(int id)
    {
        return Ok("Hello World id = " + id);
    }

    [HttpGet("[action]/{name}")]
    public IActionResult GetPersonByName(string name)
    {
        return Ok("Hello World name = " + name);
    }

    [HttpGet("[action]/{age}/{name}")]
    public IActionResult GetPersonByAgeAndName(int age, string name)
    {
        return Ok("Hello World age = " + age + " name = " + name);
    }

    [HttpPost("[action]")]
    public IActionResult CreatePerson(ModelPerson person)
    {
        return Ok(person.Age + " " + person.Name);
    }

    [HttpPut("[action]/{id}")]
    public IActionResult UpdatePerson(int id, ModelPerson person)
    {
        return Ok(id + " age = " + person.Age + " name = " + person.Name);
    }

    [HttpDelete("[action]/{id}")]
    public IActionResult DeletePerson(int id)
    {
        return Ok("Delete person id = " + id);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignIn(ModelPerson person)
    {
        ModelPerson? findPerson = await _context.ModelPerson.FirstOrDefaultAsync<ModelPerson>(p =>
            p.Username == person.Username &&
            p.Password == person.Password
        );

        if (findPerson != null)
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, findPerson.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(7),  // หนึ่งสัปดาห์
                signingCredentials: credentials,
                claims: claims
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { token = tokenString });
        }
        else
        {
            return Unauthorized("SignIn failed");
        }
    }

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> Info()
    {
        string headers = Request.Headers["Authorization"]!;
        string token = headers.ToString().Split(" ")[1];

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var claims = jwtToken.Claims;
        // read name from sub
        string id = claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
        ModelPerson? person = await _context.ModelPerson.FirstOrDefaultAsync(p => p.Id == int.Parse(id));

        return Ok(new { name = person?.Name, level = "Admin" });
    }

    [HttpGet("[action]")]
    [Authorize]
    public IActionResult GetData()
    {
        return Ok("GetData");
    }

    // upload file
    [HttpPost("[action]")]
    public IActionResult UploadFile(IFormFile file)
    {
        if (Directory.Exists("uploads") == false)
        {
            Directory.CreateDirectory("uploads");
        }

        var filePath = Path.Combine("uploads", file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return Ok("File uploaded successfully");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> PersonInfo()
    {
        try
        {
            string id = Service.GetIdFromToken(Request);
            ModelPerson? person = await _context.ModelPerson.FirstOrDefaultAsync(p => p.Id == int.Parse(id));

            return Ok(new
            {
                name = person?.Name,
                username = person?.Username
            });
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeProfile(ModelPerson person)
    {
        try
        {
            string id = Service.GetIdFromToken(Request);
            ModelPerson? findPerson = await _context.ModelPerson.FindAsync(int.Parse(id));

            if (findPerson == null)
            {
                return NotFound("Person not found");
            }

            findPerson.Name = person.Name;
            findPerson.Username = person.Username;

            if (person.Password != "")
            {
                findPerson.Password = person.Password;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "success" });
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> List()
    {
        try
        {
            List<ModelPerson> persons = await _context.ModelPerson.ToListAsync();
            return Ok(persons);
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Create(ModelPerson person)
    {
        try
        {
            await _context.ModelPerson.AddAsync(person);
            await _context.SaveChangesAsync();
            return Ok(new { message = "success" });
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            ModelPerson? findPerson = await _context.ModelPerson.FindAsync(id);

            if (findPerson == null)
            {
                return NotFound("Person not found");
            }

            _context.ModelPerson.Remove(findPerson);
            await _context.SaveChangesAsync();

            return Ok(new { message = "success" });
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Update(int id, ModelPerson person)
    {
        try
        {
            ModelPerson? findPerson = await _context.ModelPerson.FindAsync(id);

            if (findPerson == null)
            {
                return NotFound("Person not found");
            }

            findPerson.Name = person.Name;
            findPerson.Username = person.Username;
            findPerson.Age = person.Age;

            if (person.Password != "")
            {
                findPerson.Password = person.Password;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "success" });
        }
        catch (Exception error)
        {
            return StatusCode(500, error.Message);
        }
    }
}
