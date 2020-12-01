namespace TP3.Models
{
    /// <summary>
    /// Sous-classe de la classe Navire qui definit les
    /// proprietes propres aux navires non controlees par le joueur
    /// </summary>
    class NavireNPC : Navire
    {
        #region Constructeur
        /// <summary>
        /// Cree un navire NPC selon les proprietes en parametre
        /// </summary>
        /// <param name="equipage">Nombre de membre d'equipage</param>
        /// <param name="coque">nombre de vie de la coque du bateau</param>
        /// <param name="vitesse">La vitesse du bateau s'il est anime</param>
        /// <param name="vitesseRecharge">Vitesse de recharge des canons du bateau</param>
        /// <param name="nbOr">nombre d'or contenu dans le bateau</param>
        /// <param name="nombreCanonArriere">Nombre de canons a l'arriere du bateau</param>
        /// <param name="nbCanonCote">Nombre de canons sur le cote du bateau</param>
        public NavireNPC(int equipage, int coque, double vitesse, int vitesseRecharge, int nbOr, int nbCanonCote = 0, int nombreCanonArriere = 0) :
            base(equipage, coque, vitesse, vitesseRecharge, nbOr, nbCanonCote, nombreCanonArriere)
        {
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Methode qui calcule l'augmentation des proprietes selon le niveau
        /// </summary>
        /// <param name="propriete">le chiffre de la propriete</param>
        /// <param name="niveau">le niveau de la partie</param>
        /// <returns>La propriete augmente selon le niveau</returns>
        internal static int CalculeProprietesPositif(int propriete, int niveau)
        {
            return propriete * (1 + (niveau / 10));
        }

        /// <summary>
        /// Methode qui calcule la baise des proprietes selon le niveau
        /// </summary>
        /// <param name="propriete">le chiffre de la propriete</param>
        /// <param name="niveau">le niveau de la partie</param>
        /// <returns>La propriete augmente selon le niveau</returns>
        internal static int CalculeProprietesNegatif(int propriete, int niveau)
        {
            return propriete * (1 - (niveau / 10));
        }
        #endregion
    }
}