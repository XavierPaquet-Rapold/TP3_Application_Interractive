using System;
using System.Windows.Controls;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for BouletsCanon.xaml
    /// </summary>
    public partial class BouletsCanon : UserControl
    {
        public double DistanceParcourue = 0;
        public double vitesseBoulets = 5;

        public double VelociteX { get; set; } = 0;
        public double VelociteY { get; set; } = 0;

        public BouletsCanon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calcule la drection dans laquelle doivent aller les boulets de canon.
        /// </summary>
        /// <param name="VBateauX"></param>
        /// <param name="VBateauY"></param>
        /// <param name="cote"></param>
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
