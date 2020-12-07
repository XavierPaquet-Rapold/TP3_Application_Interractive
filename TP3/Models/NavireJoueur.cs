using TP3;
namespace TP3.Models
{
    /// <summary>
    /// Sous-classe de la classe navire qui definit les proprietes propres au navire du joueur
    /// </summary>
    class NavireJoueur : Navire
    {
        #region Proprietes
        /// <summary>Nombre de canons sur le cote du bateau</summary>
        private static int CanonCote = 5;
        /// <summary>Le nombre de vie de la coque du bateau</summary>
        private static int VieCoque = 100;
        /// <summary>La vitesse du bateau</summary>
        private static double Vitesse = 1;
        /// <summary>La vitesse de recharge des canons du bateau</summary>
        private static int VitesseRecharge = 10;
        /// <summary>Nombre d'equipage du bateau</summary>
        private static int Equipage = 80;
        /// <summary>Nombre de pieces d'or contenu dans le bateau</summary>
        private static int NombreOr = 10000;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur qui cree un bateau avec les ajouts achetes par le joueur
        /// </summary>
        /// <param name="coque">Reparation de la coque</param>
        /// <param name="recrutement">Ajout au nombre de membre d'equipage</param>
        /// <param name="resistance">Ajout au nombre de vie de la coque</param>
        /// <param name="vitesse">Augmentation de la vitesse du navire</param>
        /// <param name="degat">Augmentation des degats du navire</param>
        /// <param name="cadence"></param>
        public NavireJoueur() :
            base(Equipage, VieCoque, Vitesse, VitesseRecharge, NombreOr, CanonCote)
        {

        }
        #endregion

        public int AjoutCoque(int prix)
        {
            if(prix <= this.NbOr && prix > 0){
                this.VieCoqueMax += 50;
                this.UtilisationOr(prix);
            }
            return this.VieCoqueMax;
        }

        public int ReparationCoque(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.VieCoqueCourant = this.VieCoqueMax;
                this.UtilisationOr(prix);
            }
            return this.VieCoqueCourant;
        }

        public int AjoutEquipage(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.NombreEquipageMax += 50;
                this.NombreEquipageCourant = NombreEquipageMax;
                this.UtilisationOr(prix);
            }
            return this.NombreEquipageMax;
        }

        public double AjoutVitesse(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.VitesseNavire += 0.5;
                this.UtilisationOr(prix);
            }
            return this.VitesseNavire;
        }

        public int AjoutDegats(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.Degats += 25;
                this.UtilisationOr(prix);
            }
            return this.Degats;
        }
        
        public int AjoutCadence(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.VitesseRechargeMax -= 1;
                this.UtilisationOr(prix);
            }
            return this.Degats;
        }
    }
}