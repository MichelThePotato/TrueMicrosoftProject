using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;

namespace BibiliothequeAdminData.Services
{
    public class BibliothequeUsager : ISrcBibliothequeUsager, IDisposable
    {
        private readonly BibliothequeAdminDbContext db;

        public BibliothequeUsager(string connexion, bool ensureCreated = false)
        {
            this.db = new BibliothequeAdminDbContext(connexion);

            if (ensureCreated)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public bool Add(int bibId, int usagerId)
        {
            var usager = db.Usagers.SingleOrDefault(unUsager => unUsager.Id == usagerId);
            if (usager is not null)
            {
                usager.IdBibliotheque = bibId;
                return db.SaveChanges() > 0;
            }
            else
                return false;
        }



        public bool Delete(int usagerId)
        {
            var usager = db.Usagers.SingleOrDefault(unUsager => unUsager.Id == usagerId);
            if (usager != null)
            {
                usager.IdBibliotheque = null;
                return db.SaveChanges() > 0;
            }
            else
                return false;
        }



        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<Usager> GetAllUsagersWithBibliotheque()
        {
            return db.Usagers.Where(usager => usager.Id != null);
        }

        public IEnumerable<Usager> GetAllUsagersWithBibliotheque(int? bibId)
        {
            return db.Usagers.Where(usager => usager.Id == bibId);
        }

        public bool Update(int bibId, int previousUsagerId, int newUsagerId)
        {
            var previousUsager = db.Usagers.SingleOrDefault(usager => usager.Id == previousUsagerId);
            var newUsager = db.Usagers.SingleOrDefault(usager => usager.Id == newUsagerId);
            if (previousUsager is not null && newUsager is not null)
            {
                previousUsager.IdBibliotheque = null;
                newUsager.IdBibliotheque = bibId;
                return db.SaveChanges() > 0;

            }
            else
                return false;

        }
    }
}
