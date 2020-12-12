namespace TP3.Models
{
    /// <summary>
    /// Classe mere contenant les fonctionnnalites de base de tous les navires
    /// </summary>
    class Navire
    {
        #region proprietes

        /// <summary>Contient le nombre d'equipage du navire lors de sa creation</summary>
        public int NombreEquipageMax { get; internal set; }
        /// <summary>Contient le nombre d'equipage restant apres les attaques</summary>
        public int NombreEquipageCourant { get; internal set; }
        /// <summary>Contient la vie de la coque du navire lors de sa creation</summary>
        public int VieCoqueMax { get; internal set; }
        /// <summary>Contient la vie de la coque restant apres les attaques</summary>
        public int VieCoqueCourant { get; internal set; }
        /// <summary>Contient la vitesse maximale actuelle du navire a tout moment de la partie</summary>
        public double VitesseNavire { get; internal set; }
        /// <summary>Degats infliges lorsque boulet touche le navire ennemi</summary>
        public int Degats { get; internal set; }
        public int VitesseRechargeMax { get; internal set; }
        /// <summary>Contient la vitesse de recharge maximale actuelle du navire a tout moment de la partie</summary>
        public int VitesseRechargeActuel { get; internal set; }
        /// <summary>Contient le nombre d'or actuel du navire a tout moment de la partie</summary>
        public int NbOr { get; internal set; }
        /// <summary>Contient le nombre de canons a l'arriere du navire</summary>
        public int NbCanonArriere { get; private set; }
        /// <summary>Contient le nombre de canons sur les cotes du navire</summary>
        public int NbCanonCote { get; private set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par defaut de la classe navire
        /// </summary>
        /// <param name="equipage">Le nombre d'equipage du navire</param>
        /// <param name="coque">le nombre de vie de la coque du navire</param>
        /// <param name="vitesse">la vitesse maximale du navire</param>
        /// <param name="vitesseRecharge">la vitesse de recharge des canons du navire</param>
        /// <param name="nbOr">le nombre d'or contenu dans le navire</param>
        /// <param name="nbCanonArriere">le nombre de canons a l'arriere du navire</param>
        /// <param name="nbCanonCote">le nombre de canons sur le cote du navire</param>
        public Navire(int equipage, int coque, double vitesse, int vitesseRecharge, int nbOr, int degats, int nbCanonCote = 0, int nbCanonArriere = 0)
        {
            NombreEquipageMax = equipage;
            NombreEquipageCourant = equipage;
            VieCoqueMax = coque;
            VieCoqueCourant = coque;
            VitesseNavire = vitesse;
            VitesseRechargeMax = vitesseRecharge;
            VitesseRechargeActuel = vitesseRecharge;
            NbOr = nbOr;
            NbCanonArriere = nbCanonArriere;
            NbCanonCote = nbCanonCote;
            Degats = degats;
        }

        #endregion

        #region methodes de calcule

        /// <summary>
        /// Methode qui calcule le pourcentage d'equipage restant
        /// </summary>
        /// <returns>Un double qui contient le pourcentage d'equipage restant dans le navire</returns>
        private double CalculeNbVieEquipage()
        {
            return (double)NombreEquipageCourant / (double)NombreEquipageMax;
        }
        /// <summary>
        /// Methode qui calcule le pourcentage de vie restant de la coque
        /// </summary>
        /// <returns>Un double qui contient le pourcentage de vie de la coque restant du navire</returns>
        public double CalculeNbVieCoque()
        {
            return (double)VieCoqueCourant / (double)VieCoqueMax;
        }

        #endregion

        #region methodes de modifications de proprietes publics

        /// <summary>
        /// Methode qui gere les evenements suivant le fait qu'un
        /// navire se fasse toucher par un boulet
        /// </summary>
        /// <param name="viePerdu">Le nombre de degats inflige par les boulets</param>
        public void DegatsBoulets(int viePerdu)
        {
            VieCoqueCourant -= viePerdu / 2;
            if (VieCoqueCourant < 0)
            {
                VieCoqueCourant = 0;
            }
            NombreEquipageCourant -= viePerdu / 2;
            if (NombreEquipageCourant < 0)
            {
                NombreEquipageCourant = 0;
            }
            CalculeVitesseNavire();
            CalculeVitesseRecharge();
        }

        /// <summary>
        /// Methode qui gere les evenements suivant une collision entre les
        /// navires
        /// </summary>
        /// <param name="viePerdu">Nombre de dommages a la coque</param>
        public void DegatsCoque(int viePerdu)
        {
            VieCoqueCourant -= viePerdu;
            if (VieCoqueCourant < 0)
            {
                VieCoqueCourant = 0;
            }
            CalculeVitesseNavire();
        }

        /// <summary>
        /// Methode qui fait les modification sur le nombre d'or d'un navire
        /// </summary>
        /// <param name="orUtilise">Or vole ou utilise par le navire</param>
        /// <returns></returns>
        public int UtilisationOr(int orUtilise)
        {
            NbOr -= (int)orUtilise;
            if (NbOr < 0)
            {
                NbOr = 0;
            }
            return NbOr;
        }

        #endregion

        #region methodes de modifications de proprietes prives

        /// <summary>
        /// Methode qui calcule la vitesse maximale du navire en fonction du pourcentage de
        /// membre d'equipage restant
        /// </summary>
        private void CalculeVitesseNavire()
        {
            VitesseNavire = (int)(VitesseNavire * CalculeNbVieCoque());
            if (CalculeNbVieCoque() <= 1 / 3)
            {
                VitesseNavire = 0;
            }
        }

        /// <summary>
        /// Methode qui calcule la vitesse de recharge maximale du navire en fonction du 
        /// pourcentage de vie de la coque restant
        /// </summary>
        private void CalculeVitesseRecharge()
        {
            VitesseRechargeActuel = (int)(VitesseRechargeActuel * CalculeNbVieEquipage());
            if (CalculeNbVieEquipage() <= 1 / 3)
            {
                VitesseRechargeActuel = 10000;
            }
        }

        #endregion
    }
}