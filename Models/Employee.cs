namespace HoneyRaesAPI.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }

    public Employee(int id, string name, string specialty)
    {
        Id = id;
        Name = name;
        Specialty = specialty;
    }
}