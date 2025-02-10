namespace HoneyRaesAPI.Models;

public class ServiceTicket
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CustomerId { get; set; }
    public int? EmployeeId {get; set;}
    public Employee Employee { get; set; }
    public Customer Customer { get; set; }
    public string Description { get; set; }
    public bool Emergency { get; set; }
    public DateTime? DateCompleted { get; set; }

    public ServiceTicket
    (
        int id, 
        string name, 
        int? customerId, 
        int? employeeId, 
        string description, 
        bool emergency, 
        DateTime? dateCompleted
    )
    {
        Id = id;
        Name = name;
        CustomerId = customerId;
        EmployeeId = employeeId;
        Description = description;
        Emergency = emergency;
        DateCompleted = dateCompleted;
    }
}