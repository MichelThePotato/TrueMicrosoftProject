using BibiliothequeAdmin.Data.Services;
using BibiliothequeProjet.Data.Models;

namespace Bibliotheque.Tests
{
    [TestClass]
    public class BibliothequesTests
    {
        [TestMethod]
        public void TestAdd1BibliothequeAndDelete()
        {
            //Arrange
            var baseName = "DeleteAddBibliothequeAdd1";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var biblio = new UneBibliotheque { Nom = "tom" };

            //Ajoute un usager
            using (var biblios = new Bibliotheques(connexionString, ensureCreated: true))
            {
                //Act
                biblios.Add(biblio);

            }
            //Delete un usager
            using (var biblios = new Bibliotheques(connexionString))
            {
                //Act
                var actual = biblios.Delete(biblio.Id);

                //Assert
                Assert.IsTrue(actual);

            }
        }


        [TestMethod]
        public void TestDelete1BiblioInEmptyDb()
        {
            //Arrange
            var baseName = "DeletingBiblioEmptyDb";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var biblio = new UneBibliotheque { Nom = "bibliotheque1" };

            using (var biblios = new Bibliotheques(connexionString, ensureCreated: true))
            {
                //Act
                var actual = biblios.Delete(biblio.Id);

                //Assert
                Assert.IsFalse(actual);

            }
        }

        [TestMethod]
        public void TestUpdate1Biblio()
        {
            //Arrange
            var baseName = "UpdateBiblio";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var biblio = new UneBibliotheque { Nom = "biblio" };


            using (var biblios = new Bibliotheques(connexionString, ensureCreated: true))
            {
                //Act
                biblios.Add(biblio);

                var biblioModifie = biblios.Get(biblio.Id);
                biblioModifie.Nom = "Modifié";
                var actual = biblios.Update(biblioModifie);

                Assert.IsTrue(biblio.Nom.Equals("Modifié"));


            }
        }
        [TestMethod]
        public void TestUpdate1BiblioFalse()
        {
            //Arrange
            var baseName = "UpdateFalseBiblio";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var biblio = new UneBibliotheque { Nom = "biblio" };

            using (var biblios = new Bibliotheques(connexionString, ensureCreated: true))
            {
                //Act
                biblios.Add(biblio);

                var biblioModifie = new UneBibliotheque { Nom = "Modifié" };
                var actual = biblios.Update(biblioModifie);

                Assert.IsFalse(actual);


            }
        }
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            var baseName = "GetAll";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var biblio1 = new UneBibliotheque { Nom = "biblio1" };
            var biblio2 = new UneBibliotheque { Nom = "biblio2" };

            using (var biblios = new Bibliotheques(connexionString, ensureCreated: true))
            {
                //Act
                biblios.Add(biblio1);
                biblios.Add(biblio2);

                var actual = biblios.GetAll().Count();
                var expected = 2;


                Assert.AreEqual(expected, actual);

            }
        }
    }//class
}//namespace
