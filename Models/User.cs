namespace Phonebook.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public byte[]? ProfilePicture { get; set; }
    
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public User()
    {
        CreatedAt = DateTime.UtcNow;
    }
}