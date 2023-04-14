using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Services.BlogAdmin.Data.Services;

namespace BibiliothequeAdmin.Data.Services
{
    public class Bibliotheques : ISrcBibliotheque, IDisposable
    {
        private readonly BibliothequeAdminDbContext db;

        /*Constructur de la classe Bibliotheques qui prend en parametre
         * connexion:
         * ensureCreated
         */

        public Bibliotheques(string connexion, bool ensureCreated = false)
        {
            this.db = new BibliothequeAdminDbContext(connexion);
            if (ensureCreated)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }


        /*Méthode qui permet d'ajouter une bibliotheque a la base de donnee qui prend en parametre
         * bibliotheque: La bibliotheque a ajouter
         */
        public void Add(UneBibliotheque bibliotheque)
        {
            db.Bibliotheques.Add(bibliotheque);
            db.SaveChanges();
        }


        /*Methode qui permet de suprimmer une bibliotheque de la liste de bibliotheques prend en parametre
         * id: le id de la biblio a supprimer
         * retourne true si le nombre d'entité changé est supérieure a 0 (operation reussi) sinon
         */
        public bool Delete(int? id)
        {
            var biblio = Get(id);
            if (biblio != null)
            {
                db.Bibliotheques.Remove(biblio);
                return db.SaveChanges() >= 0;
            }
            return false;
        }

        /*Methode qui prmet de lire
         */
        public UneBibliotheque? Get(int? id)
        {
            return db.Bibliotheques.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<UneBibliotheque> GetAll()
        {
            return db.Bibliotheques.ToList();
        }

        // Update
        public bool Update(UneBibliotheque bibliothequeModifie)
        {
            var currentBiblio = Get(bibliothequeModifie.Id);
            if (currentBiblio != null)
            {
                currentBiblio.Nom = bibliothequeModifie.Nom;

                return db.SaveChanges() >= 0;
            }
            else
                return false;
        }


        public void Dispose()
        {
            db.Dispose();
        }

    }
}
