/*
 * ================================================ DATA SOURCE ================================================
 */
using HoneyRaesAPI.Models; // Imports all the names of HoneyRaesAPI.Models into this file so we don't have to 
// append 'HoneyRaesAPI.Models.' in front of each class name like below. Do this when referencing classes from 
// other namespaces when possible.

List<Customer> customers = new List<Customer>
{
    new Customer(1, "John Dough", "456 Guap Rd, Madison, TN, 37001"),
    new Customer(2, "Mariah Rice", "1234 Apple Street, Los Angeles, CA, 90001"),
    new Customer(3, "Jane Though", "345 E. Rock St., New York, NY, 10001"),
};

List<Employee> employees = new List<Employee>
{
    new Employee(1, "Tion Blackmon", "Software Engineer"),
    new Employee(2, "Mia Meadows", "Business Analyst"),
    new Employee(3, "Liam Anderson", "Project Manager"),
    new Employee(4, "Sophia Chang", "UX Designer"),
    new Employee(5, "Ethan Martinez", "DevOps Engineer"),
    new Employee(6, "Ava Hernandez", "Quality Assurance Analyst"),
    new Employee(7, "Noah Thompson", "Product Owner"),
    new Employee(8, "Olivia Brown", "Data Scientist"),
    new Employee(9, "Mason Lee", "Frontend Developer"),
    new Employee(10, "Isabella Patel", "Backend Developer"),
    new Employee(11, "Lucas Nguyen", "Security Specialist"),
    new Employee(12, "Amelia Wilson", "Technical Support Engineer")
};

List<ServiceTicket> serviceTickets = new List<ServiceTicket>
{
    new ServiceTicket(1,"Customer - keyboard issue", 1, null, "Customer was having issues with their keyboard.",
            false, new DateTime(2024, 1, 22)
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
        5,"Employee - pw reset", null, null, "Needs password reset for Outlook.", true, null),
    
    new ServiceTicket(6, "Customer - printer issue", 2, null, "Printer is not connecting to the network.", 
        false, new DateTime(2024, 11, 15)),

    new ServiceTicket(7, "", null, 3, "Software installation request for project management tool.", 
        false, null),

    new ServiceTicket(8, "", null, 3, "Account locked due to too many failed login attempts.", 
        true, null),

    new ServiceTicket(9, "Employee - software crash", null, null, "Design software crashes frequently.", 
        false, null),

    new ServiceTicket(10, "", null, 2, "New hardware request - additional RAM for laptop.", 
        false, null),

    new ServiceTicket(11, "Customer - email issue", null, 8, "Unable to send emails to external domains.", 
        true, null),

    new ServiceTicket(12, "", null, 8, "VPN connection dropping intermittently.", 
        false, null)
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

// GET ENDPOINTS
app.MapGet("api/servicetickets", () => serviceTickets);
app.MapGet("api/employees", () => employees);
app.MapGet("api/customers", () => customers);
// Emergency Tickets
app.MapGet("api/servicetickets/emergencies", () =>
{
    var emergencyTickets = serviceTickets
        .Where(st =>
            (st.DateCompleted == null) && (st.Emergency)
        ).ToList();
    return Results.Ok(emergencyTickets);
});
app.MapGet("api/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTicket.Employee = employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeId);
    return Results.Ok(serviceTicket);
});
app.MapGet("api/servicetickets/unassigned", () =>
{
    var unassignedTickets = serviceTickets.Where(st => 
        st.CustomerId == null && st.EmployeeId == null).ToList();
    return Results.Ok(unassignedTickets);
});
app.MapGet("api/employees/{id}", (int id) =>
{
    Employee employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);
});
// Available Employees.
app.MapGet("api/employees/available", () =>
{
    var availableTickets = serviceTickets.Where(st => st.)
});
app.MapGet("api/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    customer.ServiceTickets = serviceTickets.Where(st => st.CustomerId == id).ToList();
    return Results.Ok(customer);
});
app.MapGet("api/customers/inactive-customers", () =>
{
    DateTime currentDate = DateTime.Now;
    // Need to add `.hasValue` to both CustomerId and DateCompleted. Without it on CustomerId,
    // you are comparing bool to int? which isn't valid. DateCompleted needs `.hasValue`
    // because we want to make sure we are comparing actual values before doing any calculations on it.
    // Lastly, `.Value` was added to `.st.DateCompleted`. This gets the actual value that is stored in the object's
    // DateTime property. Also wanted to note that `.TotalDays` gets the value of the timespan that is being calculated.
    // The value can be a whole number or a floating-point number.
    var inactiveCustomers = serviceTickets
        .Where(st =>
            st.CustomerId.HasValue && 
            st.DateCompleted.HasValue && 
            (currentDate - st.DateCompleted.Value).TotalDays > 365)
        .ToList();
    return Results.Ok(inactiveCustomers);
});

// POST ENDPOINTS
// serviceTicket is the updated ticket from the req body.
app.MapPost("api/servicetickets", (ServiceTicket serviceTicket) =>
{
    // Line below is a way for creating a new id. SQL handles this automatically once we go over SQL and databases.
    serviceTicket.Id = serviceTickets.Max(st => st.Id) + 1;
    serviceTickets.Add(serviceTicket);
    return serviceTicket;
});
// For marking a service ticket complete with a timestamp.
app.MapPost("api/servicetickets/{id}/complete", (int id) =>
{
    ServiceTicket? ticketToComplete = serviceTickets.FirstOrDefault(st => st.Id == id);
    ticketToComplete.DateCompleted = DateTime.Today;
});

// PUT ENDPOINTS
app.MapPut("api/servicetickets/{id}", (int id, ServiceTicket serviceTicket) =>
{
    ServiceTicket? ticketToUpdate = serviceTickets.FirstOrDefault(st => st.Id == id);
    int ticketIndex = serviceTickets.IndexOf(ticketToUpdate);
    if (ticketToUpdate == null)
    {
        return Results.NotFound();
    }
    if (id != serviceTicket.Id)
    {
        return Results.BadRequest();
    }
    serviceTickets[ticketIndex] = serviceTicket;
    return Results.Ok();
});

// DELETE ENDPOINTS
app.MapDelete("api/servicetickets/{id}", (int id) =>
{
    ServiceTicket? serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTickets.Remove(serviceTicket);
    return Results.Ok(serviceTicket);
});
// Bind to all IPs. 
// app.Urls.Add("http://0.0.0.0:5297");    // TODO: Might need to update port number.

app.Run();
