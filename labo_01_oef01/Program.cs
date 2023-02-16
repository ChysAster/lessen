var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<WineValidator>();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(); zonder aangepaste versie
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "Wine API", Version = "v1" });
  c.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();
app.MapSwagger();
app.UseSwaggerUI(); // /swagger in de url om de ui te zien


var wines = new List<Wine>();
wines.Add(new Wine
{
  Name = "Lekker wijntje",
  Year = 2018,
  Grapes = "Bordeaux",
  Country = "France",
  Color = "Red",
  Price = 14.5M
});
wines.Add(new Wine
{
  Name = "Cabarnet Sauvignon",
  Year = 2018,
  Grapes = "Bordeaux",
  Country = "France",
  Color = "Red",
  Price = 14.5M
});
wines.Add(new Wine
{
  Name = "Vies wijntje",
  Year = 2018,
  Grapes = "Bordeaux",
  Country = "France",
  Color = "Red",
  Price = 14.5M
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/wines", () => { return Results.Ok(wines); });

// app.MapGet("/wine", () =>
// {
//   return Results.Ok(new Wine
//   {
//     Name = "Cabarnet Sauvignon",
//     Year = 2018,
//     Grapes = "Bordeaux",
//     Country = "France",
//     Color = "Red",
//     Price = 14.5M //bij gebruik decimal moet de M achter kommagetal
//   });
// });

app.MapGet("/wine/{wineId}", (int wineId) =>
{
  var wine = wines.FirstOrDefault(w => w.WineId == wineId);
  if (wine == null)
  {
    return Results.NotFound();
  }
  return Results.Ok(wine);
});

//post gaat met een body, get met url

app.MapPost("/wines", (IValidator<Wine> validator, Wine wine) =>
{
  var result = validator.Validate(wine);
  if (!result.IsValid)
  {
    var error = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(error);
  }
  wine.WineId = wines.Max(w => w.WineId) + 1;
  wines.Add(wine);
  return Results.Created($"/wine/{wine.WineId}", wine);
});

app.MapDelete("wines/{wineId}", (int wineId) =>
{
  var wine = wines.Find((Wine w) =>
  {
    return w.WineId == wineId;
  });
  if (wine == null)
  {
    return Results.NotFound();
  }

  wines.Remove(wine);
  return Results.Ok();
});

app.MapPut("/wines/{wineId}", (Wine wine, int wineId) =>
{
  // var existingWine = wines.Find((Wine w) =>
  // {
  //   return w.WineId == wine.WineId;
  // });
  var existingWine = wines.FirstOrDefault(w => w.WineId == wineId);
  if (existingWine == null)
  {
    return Results.NotFound();
  }

  existingWine.Name = wine.Name;
  existingWine.Year = wine.Year;
  existingWine.Country = wine.Country;
  existingWine.Color = wine.Color;
  existingWine.Price = wine.Price;
  existingWine.Grapes = wine.Grapes;

  return Results.Ok();
});

app.Run("http://localhost:5000");
