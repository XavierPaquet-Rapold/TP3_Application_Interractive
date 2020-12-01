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
        private static int NbOr = 0;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur qui cree un bateau avec les ajouts achetes par le joueur
        /// </summary>
        /// <param name="coque">Nombre de vie de la coque</param>
        /// <param name="recrutement">Ajout au nombre de membre d'equipage</param>
        /// <param name="resistance">Ajout au nombre de vie de la coque</param>
        /// <param name="vitesse">Augmentation de la vitesse du navire</param>
        /// <param name="degat">Augmentation des degats du navire</param>
        /// <param name="cadence"></param>
        public NavireJoueur(int coque, int recrutement, int resistance, int vitesse, int degat, int cadence) :
            base(Equipage + (recrutement * 20), coque, Vitesse - (vitesse * 10), NbOr, VitesseRecharge - (cadence * 2), CanonCote)
        {

        }
        #endregion
    }
}