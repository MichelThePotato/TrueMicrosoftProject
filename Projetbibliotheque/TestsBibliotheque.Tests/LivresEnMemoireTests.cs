using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibliotheque.Tests
{
    [TestClass]
    public class LivresEnMemoireTests
    {
        
        /* Test unitaire qui cree une liste en memoire avec un livre dans la liste et verifie
        qu'il n'est pas null */
        [TestMethod]
        public void TestGetExistingId()
        {
            //Arrange 
            ISrcLivre srce = new LivresEnMemoire(new List<Livre>()
            {
                    new Livre() { Id = 0},
            });
            Assert.IsTrue(srce.Get(0) is not null);
        }


        /* Test unitaire qui cree une liste de livre en memoire avec aucun livre et verifie
         * si la liste est vide
         */
        [TestMethod]
        public void TestGetNonExistingId()
        {
            ISrcLivre srce = new LivresEnMemoire(new List<Livre?>());

            var actual = srce.Get(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TestGetAllFromEmptyList()
        {
            //Arrange
           var arrangeList = new List<Livre?>();
           /*  {
             new Livre(){ Id = 1, Titre = "naruto", AnneePublication = 1940},
             new Livre(){ Id = 2, Titre = "onepiece", AnneePublication = 1940},
             new Livre(){ Id = 3, Titre = "onepiece", AnneePublication = 2023},

             };

            var expectedList = new List<Livre?>()
            {
             new Livre() { Id = 3, Titre = "onepiece", AnneePublication = 2023 },
             new Livre() { Id = 2, Titre = "onepiece", AnneePublication = 1940 },
             new Livre() { Id = 1, Titre = "naruto", AnneePublication = 1940 },
             };*/



            ISrcLivre srce = new LivresEnMemoire(arrangeList);

            //Act 
            var actual = srce.GetAll();

            //actualClients.ToList().ForEach(Console.WriteLine);


            //Assert
             CollectionAssert.AreEquivalent(actual.ToList(), arrangeList);
            // Assert.IsTrue(actual.IsNullOrEmpty());
            //Assert.AreEqual( arrangeList, actual?.ToList());
        }
        
    }
}