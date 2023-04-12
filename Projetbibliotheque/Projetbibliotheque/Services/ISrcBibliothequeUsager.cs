using LesBibiliotheque.Data.Models;

namespace BibiliothequeAdminData.Services
{
    internal interface ISrcBibliothequeUsager
    {
        IEnumerable<Usager> GetAllUsagersWithBibliotheque();
        IEnumerable<Usager> GetAllUsagersWithBibliotheque(int? bibId);

        bool Add(int bibId, int usagerId);
        bool Update(int bibId, int previousUsagerId, int newUsagerId);
        bool Delete(int usagerId);
    }

}
