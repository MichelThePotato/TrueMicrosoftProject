using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;

namespace LesBibiliotheque.Data.Services
{
    public class Usagers : ISrcUsager, IDisposable
    {
        private readonly BibliothequeAdminDbContext db;

        /*  public Usagers(BibliothequeAdminDbContext db)
          {
              this.db = db;
          }*/
        public Usagers(string connexion, bool ensureCreated = false)
        {
            this.db = new BibliothequeAdminDbContext(connexion);
            if (ensureCreated)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

        }

        public void Add(Usager nouveauUsager)
        {
            db.Usagers.Add(nouveauUsager);
            db.SaveChanges();
        }

        public bool Delete(int? id)
        {
            var usager = Get(id);
            if (usager != null)
            {
                db.Usagers.Remove(usager);
                return db.SaveChanges() >= 0;
            }
            return false;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Usager? Get(int? id)
        {
            return db.Usagers.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Usager?> GetAll()
        {
            return db.Usagers.ToList();

        }

        public bool Update(Usager usagerModifié)
        {
            var currentUsager = Get(usagerModifié.Id);
            if (currentUsager != null)
            {
                currentUsager.Nom = usagerModifié.Nom;

                return db.SaveChanges() >= 0;
            }
            else
                return false;
        }
    }
}
