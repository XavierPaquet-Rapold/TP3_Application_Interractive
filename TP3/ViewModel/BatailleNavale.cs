﻿using System.Collections.Generic;
using TP3.Models;

namespace TP3.ViewModel
{
    /// <summary>
    /// Classe qui fait le lien entre le front-end et le back-end
    /// </summary>
    static class BatailleNavale
    {
        /// <summary>Nombre de bateaux du jeu</summary>
        private const int NombreBateaux = 5;
        /// <summary>Le niveau actuel du joueur</summary>
        private static int _niveau = 1;
        /// <summary>La liste de tous les navires en jeu</summary>
        private static List<Navire> _listeNavire;
        /// <summary>
        /// Propriete qui retourne la liste des navires en jeu
        /// </summary>
        public static List<Navire> ListeNavire { get { return _listeNavire; } set { _listeNavire = value; } }
        /// <summary>
        /// Propriete qui retourne le niveau actuel du joueur
        /// </summary>
        public static int Niveau { get { return _niveau; }  set { _niveau = value; } }

        /// <summary>
        /// Methode qui remplit le tableau des navires au debut du jeu
        /// </summary>
        public static void InitialiserJeu()
        {
            Niveau = 1;
            ListeNavire = new List<Navire>();
            ListeNavire.Clear();
            for (int i = 0; i < NombreBateaux; i++)
            {
                switch (i)
                {
                    case 0:
                        ListeNavire.Add(new NavireJoueur());
                        break;
                    case 1:
                        ListeNavire.Add(new GalionEspagnol(_niveau));
                        break;
                    default:
                        ListeNavire.Add(new NavireEscorte(_niveau));
                        break;
                }
            }
        }

        /// <summary>
        /// Methode qui initialise un nouveau niveau avec de nouveaux navires
        /// ennemis
        /// </summary>
        public static void InitialiserNiveau()
        {
            var temp = ListeNavire[0];
            ListeNavire = new List<Navire>();
            for (int i = 0; i < NombreBateaux; i++)
            {
                switch (i)
                {
                    case 0:
                        ListeNavire.Add(temp);
                        break;
                    case 1:
                        ListeNavire.Add(new GalionEspagnol(_niveau));
                        break;
                    default:
                        ListeNavire.Add(new NavireEscorte(_niveau));
                        break;
                }
            }
        }

        /// <summary>
        /// Verifie si les bateaux ennemis sont morts
        /// </summary>
        /// <returns>Retourne si les ennemis sont morts</returns>
        public static bool VerificationFinNiveau()
        {
            int nombreBateauxNPC = NombreBateaux - 1;
            int bateauxMorts = 0;
            for (int i = 0; i < nombreBateauxNPC; i++)
            {
                if(_listeNavire[i + 1].VieCoqueCourant == 0)
                {
                    bateauxMorts++;
                }
            }
            if(bateauxMorts == nombreBateauxNPC)
            {
                return true;
            }
            return false;
        }
    }
}