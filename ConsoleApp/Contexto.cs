using EFCExample.DataModels;
using Microsoft.EntityFrameworkCore;

namespace EFCExample
{
    public class Contexto: DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Email> Emails { get; set; }

        public Contexto()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;initial Catalog=EntitySolution;User ID=teste;password=teste;language=Portuguese;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
            .HasMany(e => e.Emails)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Pessoa>()
                .Navigation(b => b.Emails)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.Entity<Email>()
            .HasOne(p => p.pessoa)
            .WithMany(b => b.Emails)
            //.HasForeignKey(e => e.pessoa)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
