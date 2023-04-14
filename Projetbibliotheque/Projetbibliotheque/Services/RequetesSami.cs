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
            var result = (
                from usager in dbContext.Usagers
                join bibliotheque in dbContext.Bibliotheques on usager.IdBibliotheque equals bibliotheque.Id
                orderby usager.Nom, bibliotheque.Nom descending
                select new Tuple<string, int>(
                    usager.Nom + " " + bibliotheque.Nom,
                    usager.Id + bibliotheque.Id)
                ).ToList();

            return result;
        }



        public List<Usager> Recherche(string Nom, int biblioId)
        {

            var result = (
                from usager in dbContext.Usagers
                where usager.Nom.Contains(Nom)
                join bibliotheque in dbContext.Bibliotheques on usager.IdBibliotheque equals bibliotheque.Id
                select new Usager
                {

                    Id = usager.Id,
                    Nom = usager.Nom,

                    IdBibliotheque = bibliotheque.Id
                }).ToList();

            return result;

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
