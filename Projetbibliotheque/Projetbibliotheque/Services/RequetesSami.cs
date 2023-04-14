using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;

namespace Projetbibliotheque.Data.Services
{
    public class RequetesSami : ISrcRequeteSami, IDisposable
    {
        private readonly BibliothequeAdminDbContext dbContext;

        public RequetesSami(string connexion, bool ensureCreated = false)
        {
            this.dbContext = new BibliothequeAdminDbContext(connexion);
            if (ensureCreated)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }

        public List<Tuple<string, int>> Agregation()
        {
            // Perform the LINQ query
            var result = (
                from usager in dbContext.Usagers // Select from the Usagers DbSet
                join bibliotheque in dbContext.Bibliotheques on usager.IdBibliotheque equals bibliotheque.Id // Perform a join with the Bibliotheques DbSet based on the IdBibliotheque property
                orderby usager.Nom, bibliotheque.Nom descending // Sort by Nom property of Usagers and Nom property of Bibliotheques in descending order
                select new Tuple<string, int>(
                    usager.Nom + " " + bibliotheque.Nom, // Project a calculated field by concatenating Nom property from Usagers and Nom property from Bibliotheques
                    usager.Id + bibliotheque.Id) // Project another calculated field by adding Id property from Usagers and Id property from Bibliotheques
                ).ToList(); // Convert the result to a List<Tuple<string, int>>

            return result; // Return the result
        }



        public List<Usager> Recherche(string Nom, int biblioId)
        {
            // Query the BibliothequeAdminDbContext to get Usagers and their related Bibliotheque entity
            var result = (
                from usager in dbContext.Usagers
                where usager.Nom.Contains(Nom) // Filter Usagers by name containing the provided Nom parameter
                join bibliotheque in dbContext.Bibliotheques on usager.IdBibliotheque equals bibliotheque.Id // Perform a join with the Bibliotheques DbSet based on the IdBibliotheque property
                where bibliotheque.Id == biblioId // Filter Bibliotheques by the provided biblioId parameter
                select new Usager
                {
                    // Set the properties of the Usager object
                    Id = usager.Id,
                    Nom = usager.Nom,
                    // Set the properties of the related Bibliotheque entity
                    IdBibliotheque = bibliotheque.Id
                }).ToList();

            return result; // Return the list of Usagers with the detailed information about their related Bibliotheque entity

        }
        public List<Usager> TrierUsagerParNom()
        {
            return dbContext.Usagers.OrderBy(u => u.Nom).ToList();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
