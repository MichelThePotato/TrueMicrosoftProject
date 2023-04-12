using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibiliothequeAdmin.Services
{
    public class BibliothequeLivre : ISrcBibliothequeLivre, IDisposable
    {
        private readonly BibliothequeAdminDbContext db;

        public BibliothequeLivre(string connexion, bool ensureCreated = false)
        {
            this.db = new BibliothequeAdminDbContext(connexion);
            if (ensureCreated)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public bool Add(int bibId, int livreId)
        {
            var livre = db.Livres.SingleOrDefault(unLivre => unLivre.Id == livreId);
            if (livre is not null)
            {
                livre.IdBibliotheque = bibId;
                return db.SaveChanges() > 0;
            }
            else
                return false;
        }

        public bool Delete( int livreId)
        {
            var livre = db.Livres.SingleOrDefault(unLivre => unLivre.Id == livreId);
            if (livre != null)
            {
                livre.IdBibliotheque = null;
                return db.SaveChanges() > 0;
            }
            else
                return false;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<Livre>? GetAllLivresWithBibliotheque()
        {
            return db.Livres.Where(livre => livre.IdBibliotheque != null);
        }

        public IEnumerable<Livre>? GetAllLivresWithBibliotheque(int bibId)
        {
            return db.Livres.Where(livre => livre.IdBibliotheque == bibId);

        }

        public bool Update(int bibId, int previousLivreId, int newLivreId)
        {
            var previousLivre = db.Livres.SingleOrDefault(unLivre => unLivre.Id == previousLivreId);
            var newLivre = db.Livres.SingleOrDefault(unLivre => unLivre.Id == newLivreId);
            if (previousLivre != null && newLivre != null) 
            {
                previousLivre.IdBibliotheque = null;
                newLivre.IdBibliotheque = bibId;
                return db.SaveChanges() > 0;

            }else
                return false;

        }
    }
}
