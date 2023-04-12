using BibiliothequeProjet.Data.Models;

namespace LesBibiliotheque.Data.Models
{
    public class Usager
    {
        public int Id { get; set; }

        public string? Nom { get; set; }

        public int? IdBibliotheque { get; set; }


        public List<UneBibliotheque>? Bibiliotheques { get; set; }


    }
}
