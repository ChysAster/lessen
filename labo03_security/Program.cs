var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(settings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IOccasionRepository, OccasionRepository>();
builder.Services.AddTransient<ISneakerRepository, SneakerRepository>();
builder.Services.AddTransient<ISneakerService, SneakerService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/setup", async (ISneakerService sneakerService) =>
{
  await sneakerService.SetupData();
  return "Setup Done";
});

app.Run("http://localhost:5000");
