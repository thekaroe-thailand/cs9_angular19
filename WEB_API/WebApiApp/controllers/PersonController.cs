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

[ApiController]
[Route("api/[controller]")] // localhost:5000/api/person
public class PersonController : ControllerBase
{
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
    public IActionResult SignIn(ModelPerson person){
        if (person.Username == "admin" && person.Password == "1234") {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, person.Username),
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
        } else {
            return Unauthorized("SignIn failed");
        }
    }

    [HttpGet("[action]")]
    [Authorize]
    public IActionResult GetData()
    {
        return Ok("GetData");
    }

    // upload file
    [HttpPost("[action]")]
    public IActionResult UploadFile(IFormFile file) {
        if (Directory.Exists("uploads") == false) {
            Directory.CreateDirectory("uploads");
        }

        var filePath = Path.Combine("uploads", file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create)) {
            file.CopyTo(stream);
        }

        return Ok("File uploaded successfully");
    }

}

