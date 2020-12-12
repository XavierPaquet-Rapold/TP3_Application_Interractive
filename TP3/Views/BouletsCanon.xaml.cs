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
        public double _distanceParcourue = 0;
        /// <summary>
        /// Vitesse du boulet
        /// </summary>
        public double _vitesseBoulet = 5;

        /// <summary>
        /// Velocite du boulet dans l'axe X
        /// </summary>
        public double _velociteX { get; set; } = 0;
        /// <summary>
        /// Volocite du boulet dans l'axe Y
        /// </summary>
        public double _velociteY { get; set; } = 0;

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
        /// <param name="vBateauX">Velocite du navire dans l'axe X</param>
        /// <param name="vBateauY">Velocite du navire dans l'axe Y</param>
        /// <param name="cote">cote du navire duquel le boulet est tire</param>
        public void CalculerDirection(double vBateauX, double vBateauY, bool cote) 
        {
            if(cote)
            {
                _velociteX = -vBateauY * _vitesseBoulet;
                _velociteY = vBateauX * _vitesseBoulet;
            } else
            {
                _velociteX = vBateauY * _vitesseBoulet;
                _velociteY = -vBateauX * _vitesseBoulet;
            }
        }

        /// <summary>
        /// Calcule l'angle des boulets de canon par rapport a l'angle du bateau.
        /// </summary>
        public void CalculerAngle()
        {
            RotationImage.Angle = Math.Round(Math.Atan(_velociteY / _velociteX) * 180 / Math.PI);
        }

        /// <summary>
        /// Stop la vitesse du boulet de canon en x.
        /// </summary>
        public void StopX()
        {
            _velociteX = 0;
        }

        /// <summary>
        /// Stop la vitesse du boulet de canon en y.
        /// </summary>
        public void StopY()
        {
            _velociteY = 0;
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
