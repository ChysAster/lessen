using Dapr;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5011").UseGrpcEndpoint("http://localhost:60002"));
var app = builder.Build();


app.MapGet("/", () => "Hello World2!");

app.MapGet("/sum/{a}/{b}", (int a, int b) =>
{
  return new { result = a + b };
});

app.MapPost("/orders", [Topic("redis-pubsub", "orders")] (Order order) =>
{
  Console.WriteLine("Subscriber received : " + order);
  return Results.Ok(new MessageReply("SUCCESS"));
});

app.UseCloudEvents();
app.MapSubscribeHandler();
app.Run();
record Order(string Id, string Name, string Description, string Status);
record MessageReply(string status);

