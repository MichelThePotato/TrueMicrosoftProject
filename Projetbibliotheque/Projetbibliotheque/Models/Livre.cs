using BibiliothequeProjet.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBibiliotheque.Data.Models
{
    public class Livre : IEquatable<Livre>, IComparable<Livre>
    {
        public int Id { get; set; }

        public string? Titre { get; set; }

        public int AnneePublication { get; set; }

        //clef étrangaire pour directuer
        public int? IdBibliotheque { get; set; }
        public List<UneBibliotheque?> Bibliotheques { get; set; }

        public int CompareTo(Livre? other)
        {
            if (other is null || Titre is null) return -1;

            return Titre?.CompareTo(other?.Titre) != 0 ?
                Titre!.CompareTo(other?.Titre)
                : AnneePublication.CompareTo(other?.AnneePublication);
        }

        public bool Equals(Livre? other)
        {
            return Id.Equals(other?.Id);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as Livre);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
    }
