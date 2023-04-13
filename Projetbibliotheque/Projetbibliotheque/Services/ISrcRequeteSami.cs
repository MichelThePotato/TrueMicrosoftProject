using LesBibiliotheque.Data.Models;

namespace Projetbibliotheque.Data.Services
{
    public interface ISrcRequeteSami
    {
        List<Usager> TrierLesBibliothequeParNom(List<Usager> liste);


        List<Usager> Recherche(string Nom, int biblioId);

        List<Tuple<string, int>> Agregation();
    }
}
