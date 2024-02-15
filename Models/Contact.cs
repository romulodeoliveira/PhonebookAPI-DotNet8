namespace Phonebook.Models;

public class Contact
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Contact()
    {
        CreatedAt = DateTime.UtcNow;
    }
}