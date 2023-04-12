using LesBibiliotheque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBibiliotheque.Data.Services
{
    public class LivresEnMemoire : ISrcLivre
    {
        private readonly List<Livre?> livres = new();

        public LivresEnMemoire(List<Livre> livresEnMemoire)
        {
            this.livres.Clear();
            this.livres.AddRange(livresEnMemoire);
           //this.livres = livresEnMemoire;
        }

        public Livre? Get(int id)
        {
            var searchClient = new Livre() { Id = id };
            //IndexOf plus efficace qu'un foreach des lors que l'entite est IEquetable
            var searchIndex = livres.IndexOf(searchClient);
            return searchIndex != -1 ? livres[searchIndex] : null;
        }

        public IEnumerable<Livre?> GetAll()
        {
            return livres;
        }

       
    }
}
