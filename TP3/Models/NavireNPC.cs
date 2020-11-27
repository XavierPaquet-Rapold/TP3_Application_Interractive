namespace TP3.Models
{
    /// <summary>
    /// 
    /// </summary>
    class NavireNPC : Navire
    {
        public NavireNPC(int equipage, int coque, int vitesse, int vitesseRecharge, int nbOr, int nombreCanonArriere = 0, int nbCanonCote = 0) :
            base(equipage, coque, vitesse, vitesseRecharge, nbOr, nombreCanonArriere, nbCanonCote)
        {
        }

        public bool Attaque()
        {
            return false;
        }
    }
}