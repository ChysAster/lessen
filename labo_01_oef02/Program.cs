var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<BrandValidator>();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

var brands = new List<Brand>();
brands.Add(new Brand { Name = "Audi", Country = "Duitsland", Logo = "dd" });
brands.Add(new Brand { Name = "BMW", Country = "Duitsland", Logo = "dd" });
brands.Add(new Brand { Name = "Porsche", Country = "Duitsland", Logo = "dd" });

app.MapGet("/", () => "Hello World!");

app.MapGet("/brands", () => { return Results.Ok(brands); });

app.MapGet("/brands/{country}", (string country) =>
{
  var brandsfrom = brands.Where(b => b.Country == country);
  return Results.Ok(brandsfrom);
});

app.MapPost("/brands", (IValidator<Brand> validator, Brand brand) =>
{
  var result = validator.Validate(brand);
  if (!result.IsValid)
  {
    var error = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(error);
  }
  brand.BrandId = brands.Max(b => b.BrandId) + 1;
  brands.Add(brand);
  return Results.Created($"/brand/{brand.BrandId}", brand);
});

app.Run("http://localhost:5000");
