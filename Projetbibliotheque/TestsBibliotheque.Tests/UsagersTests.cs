using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;

namespace Bibliotheque.Tests
{
    [TestClass]
    public class UsagersTests
    {

        [TestMethod]
        public void TestDelete1UsagerAndAdd1Usager()
        {
            //Arrange
            var baseName = "DeleteUnUsager";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var usager = new Usager { Nom = "" };

            //Ajoute un usager
            using (var usagers = new Usagers(connexionString, ensureCreated: true))
            {
                //Act
                usagers.Add(usager);

            }
            //Delete un usager
            using (var usagers = new Usagers(connexionString))
            {
                //Act
                var actual = usagers.Delete(usager.Id);

                //Assert
                Assert.IsTrue(actual);

            }

        }
        [TestMethod]
        public void TestDelete1UsagerInEmptyDb()
        {
            //Arrange
            var baseName = "DeletingNonExistingUsager";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var usager = new Usager { Nom = "" };

            using (var usagers = new Usagers(connexionString, ensureCreated: true))
            {
                //Act
                var actual = usagers.Delete(usager.Id);

                //Assert
                Assert.IsFalse(actual);

            }
        }
        [TestMethod]
        public void TestUpdate1Usager()
        {
            //Arrange
            var baseName = "UpdateUsager";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var usager = new Usager { Nom = "" };


            using (var usagers = new Usagers(connexionString, ensureCreated: true))
            {
                //Act
                usagers.Add(usager);

                var UsagerModifie = usagers.Get(usager.Id);
                UsagerModifie.Nom = "Modifié";
                var actual = usagers.Update(UsagerModifie);

                Assert.IsTrue(usager.Nom.Equals("Modifié"));


            }
        }
        [TestMethod]
        public void TestUpdate1UsagerFalse()
        {
            //Arrange
            var baseName = "UpdateFalseUsager";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var usager = new Usager { Nom = "" };

            using (var usagers = new Usagers(connexionString, ensureCreated: true))
            {
                //Act
                usagers.Add(usager);

                var UsagerModifie = new Usager { Nom = "Modifié" };
                var actual = usagers.Update(UsagerModifie);

                Assert.IsFalse(actual);


            }
        }
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            var baseName = "GetAll";
            var connexionString = $"Server=(localdb)\\mssqllocaldb;Database={baseName};Trusted_Connection=true;";
            var usager1 = new Usager { Nom = "" };
            var usager2 = new Usager { Nom = "" };

            using (var usagers = new Usagers(connexionString, ensureCreated: true))
            {
                //Act
                usagers.Add(usager1);
                usagers.Add(usager2);

                var actual = usagers.GetAll().Count();
                var expected = 2;


                Assert.AreEqual(expected, actual);

            }
        }
    }
}
