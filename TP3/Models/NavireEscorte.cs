namespace TP3.Models
{
    /// <summary>
    /// Sous-classe de la classe NavireNPC qui defini 
    /// les proprietes propres au navire d'escorte
    /// </summary>
    class NavireEscorte : NavireNPC
    {
        #region Proprietes
        /// <summary>Nombre de pieces d'or contenu dans le bateau au niveau 1</summary>
        private const int NbOR = 100;
        /// <summary>Nombre d'equipage du bateau au niveau 1</summary>
        private const int Equipage = 100;
        /// <summary>Nombre de canons sur le cote du bateau</summary>
        private const int CanonCote = 16;
        /// <summary>Nombre de canons a l'arriere du bateau</summary>
        private const int CanonArriere = 2;
        /// <summary>La vitesse du bateau si nous les faisons bouger</summary>
        private const double Vitesse = 0.5;
        /// <summary>La vitesse de recharge des canons du bateau au niveau 1</summary>
        private const int Recharge = 5;
        /// <summary>Le nombre de vie de la coque du bateau</summary>
        private const int VieCoque = 150;
        /// <summary>Nombre de degats infliges a l'ennemi</summary>
        private const int Degats = 20;
        #endregion

        #region Constructeur
        /// <summary>
        /// Cree un navire d'escorte et ajuste les proprietes selon le niveau
        /// </summary>
        /// <param name="niveau">Le niveau de la partie</param>
        public NavireEscorte(int niveau) : base(NavireNPC.CalculeProprietesPositif(Equipage, niveau),
            NavireNPC.CalculeProprietesPositif(VieCoque, niveau), Vitesse, NavireNPC.CalculeProprietesNegatif(Recharge, niveau),
            NavireNPC.CalculeProprietesPositif(NbOR, niveau), NavireNPC.CalculeProprietesPositif(Degats, niveau) ,CanonCote, CanonArriere)
        {

        }
        #endregion
    }
}