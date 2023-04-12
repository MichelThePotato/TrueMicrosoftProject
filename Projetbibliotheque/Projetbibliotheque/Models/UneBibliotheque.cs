using LesBibiliotheque.Data.Models;

namespace BibiliothequeProjet.Data.Models

{
    public class UneBibliotheque
    {
        public int Id { get; set; }

        public string? Nom { get; set; }

        public List<Usager>? Usagers { get; set; }




    }
}
