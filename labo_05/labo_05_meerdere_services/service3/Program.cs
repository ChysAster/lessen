var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5012").UseGrpcEndpoint("http://localhost:60003"));
var app = builder.Build();


app.MapGet("/", () => "Hello World3!");

app.UseCloudEvents();
app.MapSubscribeHandler();
app.Run();
