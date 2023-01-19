using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello");
app.MapPost("/{id}", ([FromRoute] string id) => $"Create a resource with id: {id}");

app.Run();