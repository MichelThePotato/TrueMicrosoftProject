using BibiliothequeAdmin.Data.Services;
using BibiliothequeAdminData.Services;
using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;

namespace TestsBibliotheque.Tests
{
    [TestClass]
    public class BibliothequeUsagerTests
    {
        [TestMethod]
        public void TestAdd1UsagerAnd1Bibliotheque()
        {
            //Arrange
            var baseName = "AddUsagerEtBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";


            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio1" };
                bibliotheques.Add(uneBibliotheque);
            }

            using (var usagers = new Usagers(connexionString))
            {
                var unUsager = new Usager() { Nom = "Bob" };
                usagers.Add(unUsager);
            }


            using (var bibliothequeUsager = new BibliothequeUsager(connexionString))
            {
                var numDeLaBiblio = 1;
                var usagerNum = 1;
                var actual = bibliothequeUsager.Add(numDeLaBiblio, usagerNum);
                Assert.IsTrue(actual);

            }
        }//TesstAdd1UsagerAnd1Bibliotheque()

        [TestMethod]
        public void TestAdd1UsagerAndAdd1BibliothequeIsFalse()
        {
            //Arrange
            var baseName = "AddUsagerEtBibliothequeIsFalse";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio" };
                bibliotheques.Add(uneBibliotheque);
            }

            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {
                var numDeLaBiblio = 1;
                var numDulivre = 1;
                var actual = biblioUsager.Add(numDeLaBiblio, numDulivre);
                Assert.IsFalse(actual); // Vérification que l'ajout a échoué
            }
        }//TestAdd1UsagerAndAdd1BibliothequeIsFalse()


        [TestMethod]
        public void TestDelete1UsagerBibliothequeExistant()
        {
            //Arrange
            var baseName = "Delete1UsagerBibliothequeExistant";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio" };
            var unUsager = new Usager { Nom = "Jean" };


            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                db.Usagers.Add(unUsager);
                db.SaveChanges();
            }

            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {

                biblioUsager.Add(uneBibliotheque.Id, unUsager.Id);
                var actual = biblioUsager.Delete(unUsager.Id);
                Assert.IsTrue(actual);

            }
        }//TestDelete1UsagerBibliothequeExistant()


        [TestMethod]
        public void TestDelete1UsagerBibliothequeInexistant()
        {
            //Arrange
            var baseName = "Delete1UsagerBibliothequeInexistant";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Bibliotheque" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {
                var numUsager = 1;
                var actual = biblioUsager.Delete(numUsager);
                Assert.IsFalse(actual);
            }
        }//TestDelete1UsagerBibliothequeInexistant()

        [TestMethod]
        public void TestGetAllUsagerWithBibliotheque()
        {
            //Arrange
            var baseName = "GetAllUsagerWithBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Bibliotheque" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unUsager = new Usager { Nom = "Tom" };
                db.Usagers.Add(unUsager);
                db.SaveChanges();
            }

            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {
                var numDeLaBiblio = 1;
                var numDulivre = 1;
                biblioUsager.Add(numDeLaBiblio, numDulivre);
                var result = biblioUsager.GetAllUsagersWithBibliotheque();
                Assert.AreEqual(1, result.Count());
            }
        }//TestGetAllUsagerWithBibliotheque()



        [TestMethod]
        public void TestGetAllUsagersWithBibliothequeId()
        {
            //Arrange
            var baseName = "GetAllUsagersWithBibliothequeId";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unUsager = new Usager { Nom = "Jon" };
                db.Usagers.Add(unUsager);
                db.SaveChanges();
            }

            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {
                var numDeLaBiblio = 1;
                var numUsager = 1;
                biblioUsager.Add(numDeLaBiblio, numUsager);
                var result = biblioUsager.GetAllUsagersWithBibliotheque(1);
                Assert.AreEqual(1, result.Count());
            }
        }//TestGetAllUsagersWithBibliothequeId


        [TestMethod]
        public void TestUpdateUsagerBibliotheque()
        {
            //Arrange
            var baseName = "UpdateUsagerBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio1" };
                var uneBibliotheque2 = new UneBibliotheque() { Nom = "Biblio2" };
                bibliotheques.Add(uneBibliotheque);
                bibliotheques.Add(uneBibliotheque2);

            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unUsager = new Usager { Nom = "John" };
                var unUsager2 = new Usager { Nom = "Bob" };
                var unUsager3 = new Usager { Nom = "Robert" };
                db.Usagers.Add(unUsager);
                db.Usagers.Add(unUsager2);
                db.Usagers.Add(unUsager3);
                db.SaveChanges();

            }

            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {
                var numDeLaBiblio = 1;
                var precedent = 1;
                var actuellement = 3;
                var autre = 2;
                var actual = biblioUsager.Update(numDeLaBiblio, precedent, actuellement);
                Assert.IsTrue(actual);

            }
        }//TestUpdateUsagerBibliotheque



        [TestMethod]
        public void TestUpdateNonExistingUsagerBibliotheque()
        {
            //Arrange
            var baseName = "UpdateNonExistingLivreBibliotheque";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unUsager = new Usager { Nom = "Jean" };
                var unUsager2 = new Usager { Nom = "Bilal" };
                db.Usagers.Add(unUsager);
                db.Usagers.Add(unUsager2);
                db.SaveChanges();
            }

            using (var bibliousager = new BibliothequeUsager(connexionString))
            {
                var numDeLaBiblio = 1;
                var precedent = 1;
                var actuellement = 3;
                var actual = bibliousager.Update(numDeLaBiblio, precedent, actuellement);
                Assert.IsFalse(actual);

            }
        }//TestUpdateNonExistingUsagerBibliotheque


        [TestMethod]
        public void TestBDisCreated()
        {
            //Arrange
            var baseName = "BDisCreated";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var biblioUsager = new BibliothequeUsager(connexionString, ensureCreated: true))
            {

            }

        }

    }//class
}//namespace
