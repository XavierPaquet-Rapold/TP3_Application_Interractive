using System.Collections.Generic;
using TP3.Models;
using TP3.Views;

namespace TP3.ViewModel
{
    static class BatailleNavale
    {
        private const int NombreBateaux = 3;
        private static int _niveau = 1;
        private static List<Navire> _listeNavire;
        public static List<Navire> ListeNavire { get { return _listeNavire; } set { _listeNavire = value; } }

        public static void InitialiserJeu()
        {
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
    }
}