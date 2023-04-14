using LesBibiliotheque.Data.Models;

namespace BibiliothequeAdminData.Services
{
    public interface ISrcRequetesMichel
    {
        List<Livre> TrierLesLivresParNomEtParAnnee(List<Livre> liste);


        List<Livre> RechercheUnLivreDansUneBibliotheque(string titreDuLivre, int numDeLaBiblio);

        List<Tuple<string?, int>> AgregegationEntreBibliothequeLivre();

    }
}
