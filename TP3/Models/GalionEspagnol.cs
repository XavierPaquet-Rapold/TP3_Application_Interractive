namespace TP3.Models
{
    /// <summary>
    /// Sous-classe de la classe NavireNPC qui defini 
    /// les proprietes propres au galion espagnole
    /// </summary>
    class GalionEspagnol : NavireNPC
    {
        #region Proprietes
        /// <summary>Nombre de pieces d'or contenu dans le bateau au niveau 1</summary>
        private const int NbOR = 350;
        /// <summary>Nombre d'equipage du bateau au niveau 1</summary>
        private const int Equipage = 200;
        /// <summary>Nombre de canons sur le cote du bateau</summary>
        private const int CanonCote = 36;
        /// <summary>Nombre de canons a l'arriere du bateau</summary>
        private const int CanonArriere = 2;
        /// <summary>La vitesse du bateau si nous les faisons bouger</summary>
        private const double Vitesse = 1.5;
        /// <summary>La vitesse de recharge des canons du bateau au niveau 1</summary>
        private const int Recharge = 15;
        /// <summary>Le nombre de vie de la coque du bateau</summary>
        private const int VieCoque = 300;
        #endregion

        #region Constructeur
        /// <summary>
        /// Cree un galion espagnole et ajuste les proprietes selon le niveau
        /// </summary>
        /// <param name="niveau">Le niveau de la partie</param>
        public GalionEspagnol(int niveau) : base(NavireNPC.CalculeProprietesPositif(Equipage, niveau), 
            NavireNPC.CalculeProprietesPositif(VieCoque, niveau), Vitesse, NavireNPC.CalculeProprietesNegatif(Recharge, niveau), 
            NavireNPC.CalculeProprietesPositif(NbOR, niveau), CanonCote, CanonArriere)
        {

        }
        #endregion
    }
}
