namespace HoneyRaesAPI.Models;

public class ServiceTicket
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CustomerId { get; set; }
    public string EmployeeId {get; set;}
    public string Description { get; set; }
}