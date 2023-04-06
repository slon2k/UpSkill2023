using System.Xml.Serialization;

namespace ConsoleApp1.Models;

public class Customer
{
    [XmlAttribute("customerId")]
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string MiddleName { get; set; }

    public string LastName { get; set; };

    public string CompanyName { get; set; }

    public string SalesPerson { get; set; }

    public string EmailAddress { get; set; }

    public string Phone { get; set; }

    public override string? ToString()
    {
        return $"{Id} {FirstName} {LastName}";
    }
}
