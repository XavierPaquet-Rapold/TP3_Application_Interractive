using System;
using System.Windows.Controls;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for BouletsCanon.xaml
    /// </summary>
    public partial class BouletsCanon : UserControl
    {
        /// <summary>
        /// Distance parcourue par le boulet
        /// </summary>
        public double DistanceParcourue = 0;
        /// <summary>
        /// Vitesse du boulet
        /// </summary>
        public double vitesseBoulets = 5;

        /// <summary>
        /// Volocite du boulet dans l'axe X
        /// </summary>
        public double VelociteX { get; set; } = 0;
        /// <summary>
        /// Volocite du boulet dans l'axe Y
        /// </summary>
        public double VelociteY { get; set; } = 0;

        /// <summary>
        /// Initialisation des boulets de canon
        /// </summary>
        public BouletsCanon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calcule la drection dans laquelle doivent aller les boulets de canon.
        /// </summary>
        /// <param name="VBateauX">Velocite du navire dans l'axe X</param>
        /// <param name="VBateauY">Velocite du navire dans l'axe Y</param>
        /// <param name="cote">cote du navire duquel le boulet est tire</param>
        public void CalculerDirection(double VBateauX, double VBateauY, bool cote) 
        {
            if(cote)
            {
                VelociteX = -VBateauY * vitesseBoulets;
                VelociteY = VBateauX * vitesseBoulets;
            } else
            {
                VelociteX = VBateauY * vitesseBoulets;
                VelociteY = -VBateauX * vitesseBoulets;
            }
        }

        /// <summary>
        /// Calcule l'angle des boulets de canon par rapport a l'angle du bateau.
        /// </summary>
        public void CalculerAngle()
        {
            RotationImage.Angle = Math.Round(Math.Atan(VelociteY / VelociteX) * 180 / Math.PI);
        }

        /// <summary>
        /// Stop la vitesse du boulet de canon en x.
        /// </summary>
        public void StopX()
        {
            VelociteX = 0;
        }

        /// <summary>
        /// Stop la vitesse du boulet de canon en y.
        /// </summary>
        public void StopY()
        {
            VelociteY = 0;
        }

        /// <summary>
        /// Stop la vitesse du boulet de canon en x et y.
        /// </summary>
        public void Stop()
        {
            StopX();
            StopY();
        }
    }
}
