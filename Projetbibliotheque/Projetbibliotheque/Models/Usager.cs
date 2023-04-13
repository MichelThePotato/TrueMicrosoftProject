using BibiliothequeProjet.Data.Models;

namespace LesBibiliotheque.Data.Models
{
    public class Usager
    {
        public int Id { get; set; }

        public string? Nom { get; set; }

        public int? IdBibliotheque { get; set; }


        public List<UneBibliotheque>? Bibiliotheques { get; set; }

        public int CompareTo(Usager other)
        {
            if (other == null)
            {
                return 1;
            }

            // Compare Usagers by their Nom property, ignoring case
            return string.Compare(Nom, other.Nom, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Usager? other)
        {
            return string.Equals(Nom, other?.Nom, StringComparison.OrdinalIgnoreCase);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Usager);
        }
    }
}
