using LesBibiliotheque.Data.Models;

namespace BibiliothequeAdminData.Services
{
    public interface ISrcRequetesMichel
    {
        List<Livre> TrierLesBibliothequeParNom(List<Livre> liste);


        List<Livre> RechercherTitreLivreDuneBiblio(string titreDuLivre, int numDeLaBiblio);

        List<Tuple<string?, int>> AgregeParBibliothequeEtNbTotalDeLivres();

    }
}
