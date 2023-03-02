var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(settings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<ICarService, CarService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();

var app = builder.Build();
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.MapGet("/setup", async (ICarService CarService) =>
{
  await CarService.SetupDummyData();
  return "Setup Done";
});

app.MapGet("/cars", async (ICarService CarService) => { return await CarService.GetAllCars(); });

app.Run("http://localhost:5000");
