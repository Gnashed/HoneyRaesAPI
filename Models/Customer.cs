namespace HoneyRaesAPI.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public Customer(int id, string name, string address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}