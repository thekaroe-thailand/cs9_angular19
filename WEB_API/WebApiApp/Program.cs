using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.EntityFrameworkCore;
using WebApiApp;

var builder = WebApplication.CreateBuilder(args);

// ลบบรรทัดนี้ออก เพราะเราใช้ Swagger แทน
// builder.Services.AddOpenApi();

// เพิ่มการกำหนดค่า Swagger ให้ละเอียดขึ้น
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My Web API",
        Version = "v1",
        Description = "A simple example ASP.NET Core Web API",
    });
});

//
// เพิ่มการใช้งาน DbContext
//
builder.Services.AddDbContext<BookContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//
// authentication
//
// เพิ่ม configuration สำหรับ JWT
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")
            )
    };
});

//
// เพิ่มการใช้งาน Cors : Cross-Origin Resource Sharing
//
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAllOrigins", builder => { // สร้าง policy ชื่อ AllowAllOrigins
        builder.WithOrigins("http://localhost:4200") // อนุญาตให้ localhost:4200 เข้าถึง
            .AllowAnyMethod() // GET, POST, PUT, DELETE, PATCH, OPTIONS
            .AllowAnyHeader(); // Content-Type, Accept, Authorization
    });
});

//
// เพิ่มการใช้งาน Controller
//
builder.Services.AddControllers();

var app = builder.Build();

//
// เพิ่ม middleware สำหรับ authentication และ authorization
//

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1"));
    // app.MapOpenApi();
}

app.UseHttpsRedirection();
// แก้ไขการกำหนด static files
app.UseStaticFiles();

app.UseRouting();

//
// เพิ่มการใช้งาน Cors
//
app.UseCors("AllowAllOrigins"); // ต้องระบุชื่อ policy ที่กำหนดไว้

app.UseAuthentication();
app.UseAuthorization();


//
// เพิ่มการใช้งาน Controller
//
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () => "Hello World");
app.MapGet("/Hello", () => "Hello World from the API Change By Kob");

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
