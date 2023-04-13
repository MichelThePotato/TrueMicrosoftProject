using BibiliothequeAdmin.Data.Services;
using BibiliothequeAdmin.Services;
using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;

namespace TestsBibliotheque.Tests
{
    [TestClass]
    public class BibliothequeLivreTests
    {


        [TestMethod]
        public void TestAdd1LivreAndAdd1Bibliotheque()
        {
            //Arrange
            var baseName = "AddLivreEtBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            //var unlivre = new Livre { Id = 1, Titre = "Attaque des titans", AnneePublication = 4 };

            /*           var liste = new List<Livre>
                       {
                           new Livre { Id = 1, Titre = "Attaque des titans", AnneePublication = 4 }
                       };
                       ISrcLivre livres = new LivresEnMemoire(liste);*/

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "Cheval", AnneePublication = 4 };
                db.Livres.Add(unlivre);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var numDeLaBiblio = 1;
                var numDulivre = 1;
                var actual = bibliothequeLivres.Add(numDeLaBiblio, numDulivre);
                Assert.IsTrue(actual);

            }

            /*  using (var connection = new SqlConnection(connexionString))
              {
                  connection.Open(); // Ouverture de la connexion
                  var command = new SqlCommand($"SELECT IdBibliotheque FROM Livres ", connection);
                  var actualValue = command.ExecuteScalar();
                  Assert.AreEqual(1, actualValue);
              }*/
        }
        [TestMethod]
        public void TestAdd1LivreAndAdd1BibliothequeIsFalse()
        {
            //Arrange
            var baseName = "AddLivreEtBibliothequeIsFalse";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var michelId = 1;
                var numDulivre = 1;
                var actual = bibliothequeLivres.Add(michelId, numDulivre);
                Assert.IsFalse(actual); // Vérification que l'ajout a échoué
            }
        }

        [TestMethod]
        public void TestDelete1LivreBibliothequeExistant()
        {
            //Arrange
            var baseName = "Delete1LivreBibliothequeExistant";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
            var unlivre = new Livre { Titre = "Tacos", AnneePublication = 4 };


            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                db.Livres.Add(unlivre);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {

                bibliothequeLivres.Add(uneBibliotheque.Id, unlivre.Id);
                var actual = bibliothequeLivres.Delete(unlivre.Id);
                Assert.IsTrue(actual);

            }


        }
        [TestMethod]
        public void TestDelete1LivreBibliothequeInexistant()
        {
            //Arrange
            var baseName = "Delete1LivreBibliothequeInexistant";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var numDulivre = 1;
                var actual = bibliothequeLivres.Delete(numDulivre);
                Assert.IsFalse(actual);
            }
        }

        [TestMethod]
        public void TestGetAllivreWithBibliotheque()
        {
            //Arrange
            var baseName = "GetAllivreWithBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "MacNCheese", AnneePublication = 4 };
                db.Livres.Add(unlivre);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var michelId = 1;
                var macNCheeseId = 1;
                bibliothequeLivres.Add(michelId, macNCheeseId);
                var result = bibliothequeLivres.GetAllLivresWithBibliotheque();
                Assert.AreEqual(1, result?.Count());
            }
        }
        [TestMethod]
        public void TestGetAllivreWithBibliothequeWithId()
        {
            //Arrange
            var baseName = "GetAllivreWithBibliothequeWithId";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "CocaCola", AnneePublication = 4 };
                db.Livres.Add(unlivre);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var michelId = 1;
                var cocacolaId = 1;

                bibliothequeLivres.Add(michelId, cocacolaId);
                var result = bibliothequeLivres.GetAllLivresWithBibliotheque(1);
                Assert.AreEqual(1, result?.Count());
            }
        }

        [TestMethod]
        public void TestUpdateLivreBibliotheque()
        {
            //Arrange
            var baseName = "UpdateLivreBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                var uneBibliotheque2 = new UneBibliotheque() { Nom = "Patate" };
                bibliotheques.Add(uneBibliotheque);
                bibliotheques.Add(uneBibliotheque2);

            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "Brocoli", AnneePublication = 4 };
                var unlivre2 = new Livre { Titre = "Raisin", AnneePublication = 6 };
                var unlivre3 = new Livre { Titre = "Carotte", AnneePublication = 10 };
                db.Livres.Add(unlivre);
                db.Livres.Add(unlivre2);
                db.Livres.Add(unlivre3);
                db.SaveChanges();

            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var michelId = 1;
                var precedent = 1;
                var actuellement = 3;

                bibliothequeLivres.Add(michelId, precedent);
                var actual = bibliothequeLivres.Update(michelId, precedent, actuellement);
                Assert.IsTrue(actual);

            }
        }
        [TestMethod]
        public void TestUpdateNonExistingLivreBibliotheque()
        {
            //Arrange
            var baseName = "UpdateNonExistingLivreBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "Brocoli", AnneePublication = 4 };
                var unlivre2 = new Livre { Titre = "Raisin", AnneePublication = 6 };
                db.Livres.Add(unlivre);
                db.Livres.Add(unlivre2);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var michelId = 1;
                var precedent = 1;
                var actuellement = 3;
                var actual = bibliothequeLivres.Update(michelId, precedent, actuellement);
                Assert.IsFalse(actual);

            }
        }
        [TestMethod]
        public void TestBDisCreated()
        {
            //Arrange
            var baseName = "BDisCreated";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliothequeLivre = new BibliothequeLivre(connexionString, ensureCreated: true))
            {

            }

        }

    }
}
