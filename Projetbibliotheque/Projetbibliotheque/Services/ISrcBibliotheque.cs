namespace LesBibiliotheque.Data.Services
{
    using BibiliothequeProjet.Data.Models;
    using System.Collections.Generic;

    namespace BlogAdmin.Data.Services
    {

        public interface ISrcBibliotheque
        {
            // Create
            void Add(UneBibliotheque bibliotheque);

            // Read
            UneBibliotheque Get(int? id);
            IEnumerable<UneBibliotheque> GetAll();

            // Update
            bool Update(UneBibliotheque bibliotheque);

            // Delete
            bool Delete(int? id);
        }
    }
}



