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
    new ServiceTicket(1,"Customer - keyboard issue", 1, null, "Customer was having issues with their keyboard.",
            false, new DateTime(2025, 1, 22)
        ),
    new ServiceTicket(
        2,"", null, 1,"Locked out of cloud backup app", 
        false, new DateTime(2025, 1, 4)
        ),
    new ServiceTicket(
        3,"", null, 1, "Employee lost external storage drive and needs a replacement.", 
        true, null),
    new ServiceTicket(
        4,"", null, 2, "Employee forgot their password to Outlook.",
        true, new DateTime(2024, 12, 23)
        ),
    new ServiceTicket(
        5,"customer - pw reset", 2, null, "Needs password reset.", false, null),
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMobileApp", policy =>
    { 
        policy.WithOrigins("http://localhost:8081", "http://10.0.0.42") // Add your mobile app's IP or Expo URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseHttpsRedirection(); // This line might not be needed.

app.UseCors("AllowMobileApp");

/*
* ================================================ ENDPOINTS ================================================
*/
app.MapGet("/servicetickets", () => serviceTickets);
app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTicket.Employee = employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeId);
    return Results.Ok(serviceTicket);
});
app.MapGet("/employees", () => employees);
app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);
});
app.MapGet("/customers", () => customers);
app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    customer.ServiceTickets = serviceTickets.Where(st => st.CustomerId == id).ToList();
    return Results.Ok(customer);
});

app.MapPost("/servicetickets", (ServiceTicket serviceTicket) =>
{
    // Line below is a way for creating a new id. SQL handles this automatically once we go over SQL and databases.
    serviceTicket.Id = serviceTickets.Max(st => st.Id) + 1;
    serviceTickets.Add(serviceTicket);
    return serviceTicket;
});

// Bind to all IPs
app.Urls.Add("http://0.0.0.0:5297");

app.Run();
