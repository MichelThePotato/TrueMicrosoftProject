using BibiliothequeAdmin.Data.Services;
using BibiliothequeAdminData.Services;
using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using Projetbibliotheque.Data.Services;

namespace TestsBibliotheque.Tests
{
    [TestClass]
    public class RequetesSamiTest
    {

        [TestMethod]
        public void TestDeLaBd()
        {
            //Arrange
            var baseName = "TestDeLaBd";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var db = new RequetesSami(connexionString, ensureCreated: true))
            {


            }

        }

        [TestMethod]
        public void TestAgrege()
        {
            // Arrange
            var baseName = "TestAgrege";
            var connectionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            // Première session qui crée la table bibliotheques
            using (var bibliotheques = new Bibliotheques(connectionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio1" };
                var uneBibliotheque2 = new UneBibliotheque() { Nom = "Biblio2" };
                var uneBibliotheque3 = new UneBibliotheque() { Nom = "Biblio3" };
                bibliotheques.Add(uneBibliotheque);
                bibliotheques.Add(uneBibliotheque2);
                bibliotheques.Add(uneBibliotheque3);
            }

            // Deuxième session qui crée la table livre
            using (var db = new Usagers(connectionString))
            {
                var usager1 = new Usager { Nom = "Usager 1" };
                var usager2 = new Usager { Nom = "Usager 3" };
                var usager3 = new Usager { Nom = "Usager 2" };
                var usager4 = new Usager { Nom = "Usager 4" };
                db.Add(usager1);
                db.Add(usager2);
                db.Add(usager3);
                db.Add(usager4);
            }

            using (var bibliothequeUsager = new BibliothequeUsager(connectionString))
            {
                var usager1 = 1;
                var usager2 = 2;
                var usager3 = 3;

                var usager1Id = 1;
                var usager2Id = 2;
                var usager3Id = 3;
                var usager4Id = 4;

                var actual = bibliothequeUsager.Add(usager1, usager1Id);
                var actual2 = bibliothequeUsager.Add(usager1, usager2Id);
                var actual3 = bibliothequeUsager.Add(usager2, usager3Id);
                var actual4 = bibliothequeUsager.Add(usager3, usager4Id);
            }

            using (var db = new RequetesSami(connectionString))
            {


                // Act
                var result = db.Agregation();

                foreach (var tuple in result)
                {
                    Console.WriteLine($"Bibliothèque {tuple.Item1} : {tuple.Item2} livre(s)");
                }
            }
        }//TestAgrege


        [TestMethod]
        public void TestRechercheNomUsagerEtBiblioId()
        {
            //Arrange
            var baseName = "TestRechercheNomUsagerEtBiblioId";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var userId = 1;

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Biblio1" };
                bibliotheques.Add(uneBibliotheque);
            }

            using (var db = new Usagers(connexionString))
            {
                var usager1 = new Usager { Nom = "Usager1" };
                var usager2 = new Usager { Nom = "Usager2" };
                db.Add(usager1);
                db.Add(usager2);
            }

            using (var biblioUsager = new BibliothequeUsager(connexionString))
            {
                var usager1Id = 1;
                var usager2Id = 2;

                var actual = biblioUsager.Add(userId, usager1Id);
                var actual2 = biblioUsager.Add(userId, usager2Id);
            }

            using (var db = new RequetesSami(connexionString))
            {
                var indexZ = 0;

                var resultat = db.Recherche("Usager1", userId);
                Assert.IsTrue(resultat.Count >= indexZ);
                Assert.AreEqual("Usager1", resultat[indexZ].Nom);
                Assert.AreEqual(1, resultat[indexZ].IdBibliotheque);

            }

        }
        [TestMethod]
        public void TestTrierUsagersParNom()
        {
            // Arrange
            var baseName = "TestTrierUsagersParNom";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var db = new Usagers(connexionString, true))
            {
                var usager1 = new Usager { Nom = "Usager 1" };
                var usager2 = new Usager { Nom = "Usager 3" };
                var usager3 = new Usager { Nom = "Usager 2" };
                var usager4 = new Usager { Nom = "Usager 4" };
                db.Add(usager1);
                db.Add(usager2);
                db.Add(usager3);
                db.Add(usager4);
            }

            // Act
            using (var db = new RequetesSami(connexionString))
            {
                var liste = db.TrierUsagerParNom(); // Sort the usagers by name and store the sorted list

                // Assert
                var expectedList = new List<Usager?>{
            new Usager { Nom = "Usager 1" },
            new Usager { Nom = "Usager 2" },
            new Usager { Nom = "Usager 3" },
            new Usager { Nom = "Usager 4" }
        };

                CollectionAssert.AreEqual(expectedList, liste); // Compare the sorted list with the expected list
            }
        }



    }//class
}//namespace