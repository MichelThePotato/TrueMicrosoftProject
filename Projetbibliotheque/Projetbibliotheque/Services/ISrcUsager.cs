using LesBibiliotheque.Data.Models;

namespace LesBibiliotheque.Data.Services
{
    public interface ISrcUsager
    {
        IEnumerable<Usager?> GetAll();
        Usager? Get(int? id);
        void Add(Usager nouveauUsager);
        bool Delete(int? id);
        bool Update(Usager usagerModifié);
    }
}
