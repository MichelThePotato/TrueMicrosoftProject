using LesBibiliotheque.Data.Models;

namespace BibiliothequeAdmin.Services
{
    public interface ISrcBibliothequeLivre
    {
        IEnumerable<Livre>? GetAllLivresWithBibliotheque();
        IEnumerable<Livre>? GetAllLivresWithBibliotheque(int bibId);

        bool Add(int bibId, int livreId);
        bool Update(int bibId, int previousLivreId, int newLivreId);
        bool Delete(int livreId);

    }
}
