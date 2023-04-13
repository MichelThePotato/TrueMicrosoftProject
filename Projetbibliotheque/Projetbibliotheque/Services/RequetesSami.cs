using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;

namespace Projetbibliotheque.Data.Services
{
    internal class RequetesSami : ISrcRequeteSami, IDisposable
    {
        private readonly BibliothequeAdminDbContext dbContext;

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
            throw new NotImplementedException();
        }

        public List<Usager> TrierLesBibliothequeParNom(List<Usager> liste)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
