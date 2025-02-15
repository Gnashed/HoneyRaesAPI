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
    new Customer(4, "Michael Stone", "789 Maple Ave, Chicago, IL, 60601"),
    new Customer(5, "Sarah Bloom", "987 Oak Lane, Miami, FL, 33101"),
    new Customer(6, "Kevin Fields", "654 Pine Blvd, Denver, CO, 80201"),
    new Customer(7, "Emily Lake", "321 River Rd, Seattle, WA, 98101"),
    new Customer(8, "David Peak", "159 Mountain St, Phoenix, AZ, 85001"),
    new Customer(9, "Olivia Shore", "741 Ocean Dr, San Diego, CA, 92101"),
    new Customer(10, "Lucas Grove", "852 Forest Path, Portland, OR, 97201"),
    new Customer(11, "Mia Stone", "963 Desert Rd, Las Vegas, NV, 89101"),
    new Customer(12, "Ethan Brook", "159 Sunset Blvd, Austin, TX, 73301"),
    new Customer(13, "Ava Meadows", "753 Hilltop Ln, Nashville, TN, 37201")
};

List<Employee> employees = new List<Employee>
{
    new Employee(1, "Tion Blackmon", "Software Engineer"),
    new Employee(2, "Mia Meadows", "Technical Analyst"),
    new Employee(3, "Liam Anderson", "Project Manager"),
    new Employee(4, "Sophia Chang", "Senior Tech Analyst"),
    new Employee(5, "Ethan Martinez", "DevOps Engineer"),
    new Employee(6, "Ava Hernandez", "Quality Assurance Analyst"),
    new Employee(7, "Noah Thompson", "Product Owner"),
    new Employee(8, "Olivia Brown", "Technical Analyst"),
    new Employee(9, "Mason Lee", "Frontend Developer"),
    new Employee(10, "Isabella Patel", "Backend Developer"),
    new Employee(11, "Lucas Nguyen", "Security Specialist"),
    new Employee(12, "Amelia Wilson", "Technical Support Engineer")
};

List<ServiceTicket> serviceTickets = new List<ServiceTicket>
{
    new ServiceTicket(
        1,
        "Keyboard issue",
        4, 
        2, 
        "Customer was having issues with their Bluetooth keyboard.",
        false, 
        new DateTime(2024, 1, 22)
    ),
    new ServiceTicket(
        2,
        "Account Locked", 
        5, 
        2,
        "Locked out of cloud backup app", 
        false, 
        new DateTime(2025, 1, 4)
    ),
    new ServiceTicket(
        3,
        "External SSD Replacement Needed",
        null,
        2,
        "Employee lost external storage drive and needs a replacement.",
        true,
        new DateTime(2025, 2, 12)
    ),
    new ServiceTicket(
        4,
        "Outlook PW Issue",
        9,
        8,
        "Employee forgot their password to Outlook.",
        true,
        new DateTime(2024, 12, 23)
    ),
    new ServiceTicket(
        5,
        "Outlook pw reset", 
        10, 
        4, 
        "Needs password reset for Outlook.", 
        true, 
        new DateTime(2025, 2, 6)
    ),
    new ServiceTicket(
        6, 
        "Customer - printer issue", 
        6, 
        4,
        "Printer is not connecting to the network.", 
        false, 
        new DateTime(2025, 2, 5)
    ),
    new ServiceTicket(
        7, 
        "",
        8,
        4,
        "Software installation request for project management tool.", 
        false,
        null
    ),
    new ServiceTicket(
        8, 
        "Out of date NPM Package.", 
        11, 
        1, 
        "NPM package for our calendar integration is out of date.",
        true, 
        new DateTime(2025, 2, 3)
    ),
    new ServiceTicket(
        9, 
        "Endpoint issue on backend.",
        13,
        1,
        "I'm receiving an error that a GET request failed due to an issue. It doesn't specify (sorry).",
        true,
        new DateTime(2025, 2, 4)
    ),
    new ServiceTicket(
        10, 
        "Frontend issue on EAP landing page.", 
        7, 
        1, 
        "The form to submit information isn't rendering after clicking on any of the fields. Very odd issue.", 
        true, 
        null
    ),
    new ServiceTicket(
        11, 
        "Email outgoing issue", 
        null, 
        8, 
        "Unable to send emails to external domains.", 
        true, 
        null
    ),
    new ServiceTicket(
        12, 
        "VPN Connection Problems", 
        null, 
        8, 
        "VPN connection dropping intermittently.", 
        true, 
        null
    ),
    new ServiceTicket(
        13, 
        "Network Connectivity Issue", 
        5, 
        null,
        "Intermittent network connectivity issues in the main office.",
        false, 
        null
    ),
    new ServiceTicket(
        14, 
        "Software Update Required", 
        7, 
        4,
        "Outdated software version causing compatibility issues.",
        false, 
        null
    ),
    new ServiceTicket(
        15, 
        "Hardware Replacement Needed", 
        2, 
        null,
        "Customer's hard drive is failing and needs to be replaced.",
        true, 
        null
    ),
    new ServiceTicket(
        16, 
        "VPN Access Request", 
        9, 
        6,
        "New employee needs VPN access configured for remote work.",
        false, 
        null
    ),
    new ServiceTicket(
        17, 
        "Printer Not Responding", 
        3, 
        null,
        "Printer in the finance department is unresponsive.",
        false, 
        null
    ),
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

// Completed Tickets ordered by completion date (oldest first).
app.MapGet("api/servicetickets/completed-oldest-sorted", () =>
{
    var completedTickets = serviceTickets
        .Where(st => st.DateCompleted.HasValue && st.CustomerId.HasValue && st.EmployeeId.HasValue)
        .OrderBy(st => st.DateCompleted)
        .ToList();
    return Results.Ok(completedTickets);
});
// Emergency Tickets
app.MapGet("api/servicetickets/emergencies", () =>
{
    var emergencyTickets = serviceTickets
        .Where(st =>
            (st.DateCompleted == null) && (st.Emergency)
        ).ToList();
    return Results.Ok(emergencyTickets);
});
// Prioritized Tickets
app.MapGet("api/servicetickets/priority", () =>
{
    var prioritizedTickets = serviceTickets
        .Where(st => st.DateCompleted == null)
        .OrderBy(st => st.Emergency)
        .ThenBy(st => st.EmployeeId == null)
        .ThenBy(st => st.EmployeeId)
        .Reverse()
        .ToList();
    return Results.Ok(prioritizedTickets);
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
// Employee's customers.
app.MapGet("api/employees/{id}/customers", (int id) =>
{
    var employeeTickets = serviceTickets
        .Where(t => t.EmployeeId == id && t.CustomerId.HasValue)
        .ToList();
    return Results.Ok(employeeTickets);
});
// Employee of the month
app.MapGet("api/employees/employee-of-the-month", () =>
{
    var completedTickets = serviceTickets
        .Where(st => st.EmployeeId.HasValue && st.CustomerId.HasValue && st.DateCompleted.HasValue)
        .GroupBy(st => st.EmployeeId)
        // Creating new anonymous object that will return the employee-of-the-month's name and how many tickets closed.
        .Select(group => new
        {
            // group.Key == EmployeeID, the value we grouped by
            // (Index) can be used since lists are zero-based.
            EmployeeId = employees[(Index)group.Key].Name,
            CompletedTickets = group.Count() // each ticket in each group is being counted to return the employee with most closes.
        })
        .OrderByDescending(x => x.CompletedTickets)
        .FirstOrDefault();
        
    return Results.Ok(completedTickets);
});

// Available Employees.
app.MapGet("api/employees/available", () =>
{
    // Extracting the employee id of service tickets that are incomplete, but assigned. Should be a list of ints'.
    var assignedEmployeeIds = serviceTickets
        .Where(st => st.DateCompleted == null && st.EmployeeId.HasValue)
        .Select(st => st.EmployeeId.Value) // Need `.HasValue` since we made the employeeId nullable in the class file.
        .ToList();
    // All employees not in the list above.
    var unassignedEmployees = employees
        .Where(e => !assignedEmployeeIds.Contains(e.Id))
        .ToList();
    return Results.Ok(unassignedEmployees);
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
