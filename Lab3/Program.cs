using Lab3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login",async(User loginData,ModelDB db) =>
{
    User? person = await db.Users!.FirstOrDefaultAsync(p => p.EMail == loginData.EMail &&
p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.EMail!) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.EMail
    };
    return Results.Json(response);
});

app.MapGet("/api/priceLists", async (ModelDB db) => await db.PriceLists!.ToListAsync());
app.MapGet("api/priceList/{id:int}", async (ModelDB db, int id) => await db.PriceLists!.Where(g => g.Id == id).FirstOrDefaultAsync());
app.MapGet("/api/renters", async (ModelDB db) => await db.Renters!.ToListAsync());
app.MapGet("/api/priceList/{apartType}", async (ModelDB db,string apartType) => await db.PriceLists!.Where(u=>u.ApartType==apartType).FirstOrDefaultAsync());
app.MapPost("/api/priceList", async (PriceList priceList, ModelDB db) =>
{
    await db.PriceLists!.AddAsync(priceList);
    await db.SaveChangesAsync();
    return priceList;
});
app.MapPost("/api/renter", [Authorize] async (Renter renter, ModelDB db) =>
{
    await db.Renters!.AddAsync(renter);
    await db.SaveChangesAsync();
    return renter;
});
app.MapDelete("/api/priceList/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    PriceList? priceList = await db.PriceLists!.FirstOrDefaultAsync(u => u.Id == id);
    if (priceList == null) return Results.NotFound(new { message = "Прейскурант не найден" });
    db.PriceLists!.Remove(priceList);
    await db.SaveChangesAsync();
    return Results.Json(priceList);
});
app.MapDelete("/api/renter/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Renter? renter = await db.Renters!.FirstOrDefaultAsync(u => u.Id == id);
    if (renter == null) return Results.NotFound(new { message = "Квартиросъемщик не найден" });
    db.Renters!.Remove(renter);
    await db.SaveChangesAsync();
    return Results.Json(renter);
});
app.MapPut("/api/priceList", [Authorize] async (PriceList priceList, ModelDB db) =>
{
    PriceList? g = await db.PriceLists!.FirstOrDefaultAsync(u => u.Id == priceList.Id);
    if (g == null) return Results.NotFound(new { message = "Прейскурант не найден" });
    g.ApartType = priceList.ApartType;
    g.PricePerMeter = priceList.PricePerMeter;
    g.Utilities = priceList.Utilities;
    await db.SaveChangesAsync();
    return Results.Json(g);
});
app.MapPut("/api/renter", [Authorize] async (Renter renter, ModelDB db) =>
{
    Renter? rt = await db.Renters!.FirstOrDefaultAsync(u => u.Id == renter.Id);
    if (rt == null) return Results.NotFound(new { message = "Прейскурант не найден" });
    rt.Name = renter.Name;
    rt.FirstName = renter.FirstName;   
    rt.LastName = renter.LastName;
    rt.PriceListId = renter.PriceListId;
    await db.SaveChangesAsync();
    return Results.Json(rt);
});
app.Run();
