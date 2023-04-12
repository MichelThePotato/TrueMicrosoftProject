using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LesBibiliotheque.Data.Data

{
    public class BibliothequeAdminDbContext : DbContext
    {
        private readonly string _connexionString;

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Usager> Usagers { get; set; }


        public DbSet<UneBibliotheque> Bibliotheques { get; set; }

        public BibliothequeAdminDbContext(string connexion)
        {
            _connexionString = connexion;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //Pour voir comment EF génère des requêtes SQL
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(_connexionString);
        }
    }
}
