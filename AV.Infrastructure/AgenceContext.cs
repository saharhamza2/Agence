using AV.ApplicationCore.Domaine;
using Microsoft.EntityFrameworkCore;


namespace AV.infrastructure;

public class AgenceContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Pack> Packs { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Conseiller> Conseillers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=AgenceVoyageDB;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
        .HasKey(r => new { r.PackId, r.Identifiant });
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Conseiller)
            .WithMany(co => co.Clients)
            .HasForeignKey(c => c.ConseillerFK)
            .OnDelete(DeleteBehavior.Cascade);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.ClrType == typeof(string))
                    property.SetMaxLength(15);
            }
        }
    }

}