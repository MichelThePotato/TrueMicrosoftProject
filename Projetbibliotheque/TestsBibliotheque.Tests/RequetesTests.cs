using BibiliothequeAdmin.Data.Services;
using BibiliothequeAdmin.Services;
using BibiliothequeAdminData.Services;
using BibiliothequeProjet.Data.Models;
using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsBibliotheque.Tests
{
    [TestClass]
    public class RequetesTests
    {
        [TestMethod]
        public void TestDeLaBd()
        {
            //Arrange
            var baseName = "TestDeLaBd";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var db = new Requetes(connexionString, ensureCreated: true))
            {


            }

        }

        [TestMethod]
        public void TestAgrege()
        {
            //Arrange
            var baseName = "TestAgrege";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";

            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                var uneBibliotheque2 = new UneBibliotheque() { Nom = "Patate" };
                var uneBibliotheque3 = new UneBibliotheque() { Nom = "Tomate" };
                bibliotheques.Add(uneBibliotheque);
                bibliotheques.Add(uneBibliotheque2);
                bibliotheques.Add(uneBibliotheque3);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "Attaque des titans", AnneePublication = 4 };
                var unlivre2 = new Livre { Titre = "Naruto", AnneePublication = 4 };
                var unlivre3 = new Livre { Titre = "One Piece", AnneePublication = 3 };
                var unlivre4 = new Livre { Titre = "Dragon ball", AnneePublication = 2 };
                db.Livres.Add(unlivre);
                db.Livres.Add(unlivre2);
                db.Livres.Add(unlivre3);
                db.Livres.Add(unlivre4);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var numDeLaBiblio = 1;
                var numDulivre = 1;
                var actual = bibliothequeLivres.Add(numDeLaBiblio, numDulivre);
                var actual2 = bibliothequeLivres.Add(numDeLaBiblio, 2);
                var actual3 = bibliothequeLivres.Add(2, 3);
                var actual4 = bibliothequeLivres.Add(3, 4);
            }
            using (var db = new Requetes(connexionString))
            {

               var result= db.Agrege();
                Assert.AreEqual(3, result.Count);
            }

        }

        [TestMethod]
        public void TestRechercheLeNomDuLivreEtSonIdBiblio()
        {
            //Arrange
            var baseName = "TestRechercheLeNomDuLivreEtIDBiblio";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
           
            using (var bibliotheques = new Bibliotheques(connexionString, ensureCreated: true))
            {
                var uneBibliotheque = new UneBibliotheque() { Nom = "Michel" };
                bibliotheques.Add(uneBibliotheque);
            }


            using (var db = new BibliothequeAdminDbContext(connexionString))
            {
                var unlivre = new Livre { Titre = "Attaque des titans", AnneePublication = 4 };
                var unlivre2 = new Livre { Titre = "Naruto", AnneePublication = 4 };
                db.Livres.Add(unlivre);
                db.Livres.Add(unlivre2);
                db.SaveChanges();
            }

            using (var bibliothequeLivres = new BibliothequeLivre(connexionString))
            {
                var numDeLaBiblio = 1;
                var numDulivre = 1;
                var actual = bibliothequeLivres.Add(numDeLaBiblio, numDulivre);
                var actual2 = bibliothequeLivres.Add(numDeLaBiblio, 2);
            }
            using (var db = new Requetes(connexionString))
            {

                var resultat = db.Recherche("Attaque des titans",1);
                Assert.IsTrue(resultat.Count >= 0);
                Assert.AreEqual("Attaque des titans", resultat[0].Titre);
                Assert.AreEqual(1, resultat[0].IdBibliotheque);
                //Assert.AreEqual("Naruto", resultat[1].Titre);
                //Assert.AreEqual("Michel", resultat[1].Bibliotheque.Nom);

            }

        }
        [TestMethod]
        public void TestTrierLesLivresParNomEtParAnnee()
        {
            //Arrange 
            var arrangeList = new List<Livre>()
            {
                    new Livre() {Titre="One Piece",AnneePublication=2000},
                    new Livre() {Titre="Hell Paradise",AnneePublication=2023},
                    new Livre() {Titre="Attaque des titans",AnneePublication=2013},
                    new Livre() {Titre="One Piece",AnneePublication=1999},
            };

            var expectedList = new List<Livre?>()
            {
                  new Livre() {Titre="Attaque des titans",AnneePublication=2013},
                  new Livre() {Titre="Hell Paradise",AnneePublication=2023},
                  new Livre() {Titre="One Piece",AnneePublication=2000},
                  new Livre() {Titre="One Piece",AnneePublication=1999},
            };
            IsrcRequetes re = new Requetes();

            var liste = re.TrierLesBibliothequeParNom(arrangeList.ToList());
            

           // liste.ToList().ForEach(Console.WriteLine());
            foreach (var livre in liste)
            {
                Console.WriteLine(livre.Titre + " " + livre.AnneePublication);
            }

            CollectionAssert.AreEqual(expectedList.ToList(), liste);
            //Assert.AreEqual(expectedList.ToList(), liste);
            //Assert.AreSame(expectedList.ElementAt(2), liste.);
        }
    }

   

}
