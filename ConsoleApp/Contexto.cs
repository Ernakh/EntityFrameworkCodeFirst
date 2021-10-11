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
    }
}
