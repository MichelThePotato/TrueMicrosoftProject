﻿using LesBibiliotheque.Data.Data;
using LesBibiliotheque.Data.Models;
using LesBibiliotheque.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BibiliothequeAdminData.Services
{
    public class RequetesMichel : ISrcRequetesMichel , IDisposable
    {
        // private readonly LivresEnMemoire dbLivres;
        private readonly BibliothequeAdminDbContext db;

        public RequetesMichel(string connexion, bool ensureCreated = false)
        {
            this.db = new BibliothequeAdminDbContext(connexion);
            if (ensureCreated)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
        public RequetesMichel()
        {
            
        }
     

        public List<Livre> Recherche(string titreDuLivre, int numDeLaBiblio)
        {
            /* var listeFiltrer =db.Livres;
             listeFiltrer.Where(x => x.Titre != null
                        && x.Titre.Contains(champ1));*/
            //var listeFiltrer = db.Livres.Include(l => l.Bibliotheques).Where(l => l.Titre.Contains(titreDuLivre) && l.Bibliotheques.Any(b => b.Nom.Contains(nomDeLaBiblio)));
            //var listeFiltrer = db.Livres.Where(l => l.Titre.Contains(titreDuLivre) && l.Bibliotheques.Any(b => b.Nom.Contains(nomDeLaBiblio)));
            //var listeFiltrer = db.Livres
            //.Where(l => l.Titre.Contains(titreDuLivre) && l.Bibliotheques.Any(b => b.Nom == nomDeLaBiblio));
            var listeFiltrer = db.Livres.Where(l => l.Titre.Contains(titreDuLivre) && l.IdBibliotheque == numDeLaBiblio);
   

            return listeFiltrer.ToList();

        }

        public List<Livre> TrierLesBibliothequeParNom(List<Livre> liste)
        {
            ISrcLivre livres = new LivresEnMemoire(liste);

            var result = livres.GetAll().OrderBy(s => s.Titre).ThenByDescending(s => s.AnneePublication).Cast<Livre>().ToList();

            return result;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<Tuple<string, int>> Agrege()
        {
            /* var result = db.Livres
             .GroupBy(l => l.IdBibliotheque)
             .Select(g => Tuple.Create(g.Key, g.Count()))
             .ToList();*/

            var result = db.Livres
            .Join(db.Bibliotheques, l => l.IdBibliotheque, b => b.Id, (l, b) => new { Livre = l, Bibliotheque = b })
            .GroupBy(x => x.Bibliotheque.Nom)
            .Select(g => Tuple.Create(g.Key, g.Count()))
            .ToList();
            return result;
        }
    }
}