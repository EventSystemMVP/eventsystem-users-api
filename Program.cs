var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
// Lägg till under builder.Services
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Lägg till under app.UseRouting()
app.UseCors("AllowAll");
var builder = WebApplication.CreateBuilder(args);

// Lägg till denna rad under builder.Services
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()    // Tillåt alla källor (frontend-URL:er)
              .AllowAnyMethod()    // Tillåt alla HTTP-metoder (GET, POST etc.)
              .AllowAnyHeader();   // Tillåt alla headers
    });
});

// ... resten av dina services (t.ex. AddControllers)

var app = builder.Build();

// Lägg till denna rad Ovanför app.MapControllers() eller app.UseEndpoints
app.UseCors("AllowAll");  // Aktivera CORS-policyn

app.MapControllers();
app.Run();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowAll", policy =>
{
policy.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
});
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

var bookings = new List<BookingModel>();

app.MapGet("/bookings", () => Results.Ok(bookings));

app.MapPost("/bookings", (BookingModel booking) =>
{
booking.Id = bookings.Count + 1;
booking.Timestamp = DateTime.Now;
bookings.Add(booking);
return Results.Created($"/bookings/{booking.Id}", booking);
});

app.Run();

record BookingModel
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string Attendee { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var items = new List<object>();

app.MapGet("/events", () => Results.Ok(items));
app.MapGet("/events/{id:int}", (int id) => Results.Ok(items.Find(i => ((dynamic)i).id == id)));
app.MapPost("/events", async (HttpContext context) =>
{
    var item = await context.Request.ReadFromJsonAsync<object>();
    items.Add(item);
    return Results.Created("/", item);
});

app.Run();
