using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibiliothequeAdminData.Services
{
    public interface IsrcRequetes
    {
        List<Livre> TrierLesBibliothequeParNom(List<Livre> liste);


        List<Livre> Recherche(string titreDuLivre, int numDeLaBiblio);

        List<Tuple<int?, int>> Agrege();

    }
}
