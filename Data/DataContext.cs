using Microsoft.EntityFrameworkCore;

namespace Phonebook.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Models.Contact> Contacts { get; set; }
    public DbSet<Models.User> Users { get; set; }
    
    // Configurando a relação de muitos contatos para um usuário
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Contact>()
            .HasOne(c => c.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.UserId)
            .IsRequired();
    }
}