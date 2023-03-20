global using Dapr.Client;
global using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5010").UseGrpcEndpoint("http://localhost:60001")); //eerste poort is dapr side port
var app = builder.Build();




app.MapGet("/", () => "Hello World1!");

app.MapGet("/calculate/{a}/{b}", async ([FromServices] DaprClient daprClient, int a, int b) =>
{

  var requestMessage = daprClient.CreateInvokeMethodRequest<string>(HttpMethod.Get, "service2", $"sum/{a}/{b}", null);
  var result = await daprClient.InvokeMethodAsync<object>(requestMessage);
  return result;

});

app.MapGet("/setstate/{key}/{value}", async ([FromServices] DaprClient daprClient, string key, string value) =>
{
  try
  {
    await daprClient.SaveStateAsync("statestore", key, value);
    return Results.Created(key, value);
  }
  catch (Exception ex)
  {
    Console.WriteLine(ex.Message);
    return Results.BadRequest();
  }
});

app.MapGet("/getstate/{key}", async ([FromServices] DaprClient daprClient, string key) =>
{
  var result = await daprClient.GetStateAsync<dynamic>("statestore", key);
  return Results.Ok(result);
});

app.MapGet("/secret/{key}", async ([FromServices] DaprClient daprClient, string key) =>
{
  var result = await daprClient.GetSecretAsync("local-secret-store", key);
  return Results.Ok(result);
});

app.MapGet("createorder", async ([FromServices] DaprClient daprClient) =>
{
  var order = new Order(Guid.NewGuid().ToString(), "Order 1", "Order 1 Description", "New");
  await daprClient.PublishEventAsync<Order>("redis-pubsub", "orders", order);
  return Results.Ok(order);
});

app.UseCloudEvents();
app.MapSubscribeHandler();
app.Run();
record Order(string Id, string Name, string Description, string Status);