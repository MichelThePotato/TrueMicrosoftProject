using BibiliothequeAdmin.Data.Services;
using BibiliothequeAdmin.Services;
using BibiliothequeAdminData.Services;
using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using Projetbibliotheque.Data.Services;

namespace TestsBibliotheque.Tests
{
    [TestClass]
    public class RequetesMichelTests
    {
        [TestMethod]
        public void TestDeLaBd()
        {
            //Arrange
            var baseName = "TestDeLaBd";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var db = new RequetesMichel(connexionString, ensureCreated: true))
            {


            }

        }

        [TestMethod]
        public void TestAgregegationEntreBibliothequeLivre()
        {
            //Arrange
            var baseName = "AgregegationEntreBibliothequeLivre";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            //Premiere session qui crees la table bibliotheques
            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                var uneBibliotheque2 = new UneBibliotheque() { Nom = "Patate" };
                var uneBibliotheque3 = new UneBibliotheque() { Nom = "Tomate" };
                bibliotheques.Add(uneBibliotheque);
                bibliotheques.Add(uneBibliotheque2);
                bibliotheques.Add(uneBibliotheque3);
            }

            //deuxieme session qui cree la table livre
            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var canard = new Livre { Titre = "Canard", AnneePublication = 4 };
                var vache = new Livre { Titre = "Vache", AnneePublication = 4 };
                var cochon = new Livre { Titre = "Cochon", AnneePublication = 3 };
                var mouton = new Livre { Titre = "Mouton", AnneePublication = 2 };
                db.Livres.Add(canard);
                db.Livres.Add(vache);
                db.Livres.Add(cochon);
                db.Livres.Add(mouton);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var michel = 1;
                var patate = 2;
                var tomate = 3;

                var canardId = 1;
                var vacheId = 2;
                var cochonId = 3;
                var moutonId = 4;

                var actual = bibliothequeLivres.Add(michel, canardId);
                var actual2 = bibliothequeLivres.Add(michel, vacheId);
                var actual3 = bibliothequeLivres.Add(patate, cochonId);
                var actual4 = bibliothequeLivres.Add(tomate, moutonId);
            }
            using (var db = new RequetesMichel(connexionString))
            {

                var nbLivreM = 2;
                var nbLivreP = 1;
                var nbLivreT = 1;

                var indexZ = 0;
                var indexU = 1;
                var indexD = 2;


                // Act
                var result = db.AgregegationEntreBibliothequeLivre();

                foreach (var tuple in result)
                {
                    Console.WriteLine($"Bibliothèque {tuple.Item1} : {tuple.Item2} livre(s)");
                }

                Assert.AreEqual(("Michel", nbLivreM), (result[indexZ].Item1, result[indexZ].Item2));
                Assert.AreEqual(("Patate", nbLivreP), (result[indexU].Item1, result[indexU].Item2));
                Assert.AreEqual(("Tomate", nbLivreT), (result[indexD].Item1, result[indexD].Item2));


            }

        }
      

        [TestMethod]
        public void TestRechercheLeNomDuLivreEtSonIdBiblio()
        {
            //Arrange
            var baseName = "RechercheLeNomDuLivreEtIDBiblio";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var michelId = 1;

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }

            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var canard = new Livre { Titre = "Canard", AnneePublication = 4 };
                var fromage = new Livre { Titre = "Fromage", AnneePublication = 4 };
                db.Livres.Add(canard);
                db.Livres.Add(fromage);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var canardId = 1;
                var fromageId = 2;

                var actual = bibliothequeLivres.Add(michelId, canardId);
                var actual2 = bibliothequeLivres.Add(michelId, fromageId);
            }

            using (var db = new RequetesMichel(connexionString))
            {
                var indexZ = 0;

                var resultat = db.RechercheUnLivreDansUneBibliotheque("Canard", michelId);
                Assert.IsTrue(resultat.Count >= indexZ);
                Assert.AreEqual("Canard", resultat[indexZ].Titre);
                Assert.AreEqual(1, resultat[indexZ].IdBibliotheque);
                //Assert.AreEqual("Fromage", resultat[1].Titre);
                //Assert.AreEqual("Michel", resultat[1].Bibliotheque.Nom);
            }

        }
        [TestMethod]
        public void TestTrierLesLivresParNomEtParAnnee()
        {
            //Arrange 
            var arrangeList = new List<Livre>()
              {
                      new Livre() {Titre="Olive",AnneePublication=2000},
                      new Livre() {Titre="Homard",AnneePublication=2023},
                      new Livre() {Titre="Amande",AnneePublication=2013},
                      new Livre() {Titre="Olive",AnneePublication=1999},
              };

            var expectedList = new List<Livre?>()
              {
                    new Livre() {Titre="Amande",AnneePublication=2013},
                    new Livre() {Titre="Homard",AnneePublication=2023},
                    new Livre() {Titre="Olive",AnneePublication=2000},
                    new Livre() {Titre="Olive",AnneePublication=1999},
              };
            ISrcRequetesMichel re = new RequetesMichel();

            var liste = re.TrierLesLivresParNomEtParAnnee(arrangeList.ToList());


            // liste.ToList().ForEach(Console.WriteLine());
            Console.WriteLine("Liste Triée");
            foreach (var livre in liste)
            {
                Console.WriteLine($"{livre.Titre} {livre.AnneePublication}");
            }

            Console.WriteLine("\nListe Attendu");

            foreach (var livre in expectedList)
            {
                Console.WriteLine($"{livre.Titre} {livre.AnneePublication}");
            }

            CollectionAssert.AreEqual(expectedList.ToList(), liste);
            //Assert.AreEqual(expectedList.ToList(), liste);
            //Assert.AreSame(expectedList.ElementAt(2), liste.);
        }
    }



}
