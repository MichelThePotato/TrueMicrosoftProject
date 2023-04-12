using LesBibiliotheque.Data.Models;

namespace LesBibiliotheque.Data.Services
{
    public interface ISrcLivre
    {
        IEnumerable<Livre?> GetAll();
        Livre? Get(int id);
    }
}
