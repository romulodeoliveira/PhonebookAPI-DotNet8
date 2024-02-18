namespace Phonebook.Models;

public class Contact
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    public string Phone { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Contact()
    {
        CreatedAt = DateTime.UtcNow;
    }
}