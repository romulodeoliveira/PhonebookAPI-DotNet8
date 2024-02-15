using Microsoft.EntityFrameworkCore;

namespace Phonebook.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Models.Contact> Contacts { get; set; }
    public DbSet<Models.User> Users { get; set; }
}