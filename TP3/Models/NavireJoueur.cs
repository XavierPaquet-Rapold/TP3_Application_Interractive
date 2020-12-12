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
        private static double Vitesse = 2;
        /// <summary>La vitesse de recharge des canons du bateau</summary>
        private static int VitesseRecharge = 5;
        /// <summary>Nombre d'equipage du bateau</summary>
        private static int Equipage = 80;
        /// <summary>Nombre de pieces d'or contenu dans le bateau</summary>
        private static int NombreOr = 0;
        /// <summary>Nombre de degats infliges a l'ennemi</summary>
        private static int Degats = 20;
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
            base(Equipage, VieCoque, Vitesse, VitesseRecharge, NombreOr, Degats, CanonCote)
        {

        }
        #endregion

        #region Achats
        /// <summary>
        /// Methode qui ajoute de la vie à la coque lorsque le joueur l'achete
        /// </summary>
        /// <param name="prix">Le prix de l'achat</param>
        /// <returns></returns>
        public void AjoutCoque(int prix)
        {
            if(prix <= this.NbOr && prix > 0){
                this.VieCoqueMax += 50;
                this.UtilisationOr(prix);
            }
        }

        /// <summary>
        /// Methode qui repare la coque lorsque le joueur l'achete
        /// </summary>
        /// <param name="prix">Le prix de l'achat</param>
        public void ReparationCoque(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.VieCoqueCourant = this.VieCoqueMax;
                this.UtilisationOr(prix);
            }
        }
        /// <summary>
        /// Methode qui ajoute des membres d'equipage lorsque le joueur l'achete
        /// </summary>
        /// <param name="prix">Le prix de l'achat</param>
        public void AjoutEquipage(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.NombreEquipageMax += 50;
                this.NombreEquipageCourant = NombreEquipageMax;
                this.UtilisationOr(prix);
            }
        }
        /// <summary>
        /// Methode qui ajoute de la vietesse au bateau lorsque le joueur l'achete
        /// </summary>
        /// <param name="prix">Le prix de l'achat</param>
        public void AjoutVitesse(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                this.VitesseNavire += 0.5;
                this.UtilisationOr(prix);
            }
        }

        /// <summary>
        /// Methode qui ajoute des degats a chaque boulets lorsque le joueur l'achete
        /// </summary>
        /// <param name="prix">Le prix de l'achat</param>
        public void AjoutDegats(int prix)
        {
            if (prix <= this.NbOr && prix > 0)
            {
                Degats += 25;
                this.UtilisationOr(prix);
            }
        }

        /// <summary>
        /// Methode qui augmente la cadence de tire lorsque le joueur l'achete
        /// </summary>
        /// <param name="prix">Le prix de l'achat</param>
        /// <returns></returns>
        public void AjoutCadence(int prix)
        {
            if (prix <= this.NbOr && prix > 0 && VitesseRechargeActuel != 0 && VitesseRechargeMax != 0)
            {
                this.VitesseRechargeActuel -= 1;
                this.VitesseRechargeMax -= 1;
                this.UtilisationOr(prix);
            }
        }
        #endregion

        #region Actions
        /// <summary>
        /// Methode qui definit les actions lors de l'abordage
        /// </summary>
        /// <param name="equipage">Nombre de membre de l'equipage ennemi</param>
        /// <param name="or">Le nombre d'or du bateau ennemi</param>
        public void Abordage(int equipage, int or)
        {
            if(this.NombreEquipageCourant + equipage / 2 < this.NombreEquipageMax)
            {
                this.NombreEquipageCourant += equipage / 2;
            }
            else
            {
                this.NombreEquipageCourant = this.NombreEquipageMax;
            }
            
            this.NbOr += or;
        }
        #endregion
    }
}