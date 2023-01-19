using MinimalApiTest;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", KataController.Say);

app.Run();