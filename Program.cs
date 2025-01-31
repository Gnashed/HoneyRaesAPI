/*
 * ================================================ DATA SOURCE ================================================
 */
using HoneyRaesAPI.Models; // Imports all the names of HoneyRaesAPI.Models into this file so we don't have to 
// append 'HoneyRaesAPI.Models.' in front of each class name like below. Do this when referencing classes from 
// other namespaces when possible.

List<Customer> customers = new List<Customer>
{
    new Customer(1, "John Dough", "456 Guap Rd, Madison, TN, 37001"),
    new Customer(2, "Tion Blackmon", "1234 Apple Street, Nashville, TN, 37000"),
    new Customer(3, "Jane Though", "345 E. Rock St., New York, NY, 10001"),
};
List<Employee> employees = new List<Employee>
{
    new Employee(1, "Tion Blackmon", "Software Engineer"),
    new Employee(2, "Mia Meadows", "Business Analyst"),
};
List<ServiceTicket> serviceTickets = new List<ServiceTicket>
{
    new ServiceTicket(1,"", 1, null, "Customer was having issues with their keyboard.", false, new DateTime(2025, 1, 22)),
    new ServiceTicket(2,"", 2, null,"Locked out of cloud backup app", false, new DateTime(2025, 1, 4)),
    new ServiceTicket(3,"", null, 1, "Employee lost external storage drive and needs a replacement.", true, null),
    new ServiceTicket(4,"", null, 2, "Employee forgot their password to Outlook.", true, new DateTime(2024, 12, 23)),
    new ServiceTicket(5,"", null, null, "", false, null),
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/hello", () => "Hello");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
