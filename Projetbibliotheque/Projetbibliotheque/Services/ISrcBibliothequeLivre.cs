using LesBibiliotheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibiliothequeAdmin.Services
{
    public interface ISrcBibliothequeLivre
    {
        IEnumerable<Livre>? GetAllLivresWithBibliotheque();
        IEnumerable<Livre>? GetAllLivresWithBibliotheque(int bibId);

        bool Add(int bibId, int livreId);
        bool Update(int bibId, int previousLivreId,int newLivreId);
        bool Delete( int livreId);

    }
}
