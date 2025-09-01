using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// BaseAddress apunta al nombre del servicio de Docker Compose (inventory)
var inventoryBaseUrl = builder.Configuration["Inventory:BaseUrl"] ?? "http://inventory";

builder.Services.AddHttpClient("inventory", client =>
{
    client.BaseAddress = new Uri(inventoryBaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
