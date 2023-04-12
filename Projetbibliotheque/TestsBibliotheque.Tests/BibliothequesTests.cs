using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibliotheque.Tests
{
    [TestClass]
    public class BibliothequesTests
    {
        [TestMethod]
        public void TestAdd1Bibliotheque()
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
                //Ac
                var actual = usagers.Delete(usager.Id);

                //Assert
                Assert.IsTrue(actual);

            }
        }


    }//class
}//namespace
