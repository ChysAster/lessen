var builder = WebApplication.CreateBuilder(args);

var csvSettings = builder.Configuration.GetSection("CsvConfig");
builder.Services.Configure<CsvConfig>(csvSettings);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IVaccinationTypeRepository, CsvVaccinationTypeRepository>();
builder.Services.AddTransient<IVaccinationRegistrationRepository, CsvVaccinationRegistrationsRepository>();
builder.Services.AddTransient<IVaccinationLocationRepository, CsvVaccinationLocationRepository>();
builder.Services.AddTransient<IVaccinationService, VaccinationService>();
builder.Services.AddValidatorsFromAssemblyContaining<VaccinRegistrationValidator>();
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();
;
app.MapGet("/", () => "Hello World!");

app.MapGet("/locations", (IVaccinationService vaccinationService) =>
{
  return Results.Ok(vaccinationService.GetLocations());
});

app.MapGet("/registrations", (IMapper mapper, IVaccinationService vaccinationService) =>
{
  var mapped = mapper.Map<List<VaccinRegistrationDTO>>(vaccinationService.GetRegistrations(), opts =>
  {
    opts.Items["locations"] = vaccinationService.GetLocations();
    opts.Items["vaccins"] = vaccinationService.GetVaccins();
  });
  return Results.Ok(mapped);

});

app.MapGet("/types", (IVaccinationService vaccinationService) =>
{
  return Results.Ok(vaccinationService.GetVaccins());
});

app.MapPost("/registrations", (IValidator<VaccinRegistration> validator, IVaccinationService vaccinationService, VaccinRegistration vaccinRegistration) =>
{
  var result = validator.Validate(vaccinRegistration);
  if (!result.IsValid) { return Results.BadRequest(result.Errors); }
  return Results.Ok(vaccinationService.AddRegistration(vaccinRegistration));
});



app.Run("http://localhost:5000");

app.UseExceptionHandler(c => c.Run(async context =>
{
  var exception = context.Features
      .Get<IExceptionHandlerFeature>()
      ?.Error;
  if (exception is not null)
  {
    var response = new { error = exception.Message };
    context.Response.StatusCode = 400;

    await context.Response.WriteAsJsonAsync(response);
  }
}));
