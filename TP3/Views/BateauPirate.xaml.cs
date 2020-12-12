using System;
using System.Windows.Controls;
using TP3.Models;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for BateauPirate.xaml
    /// </summary>
    public partial class BateauPirate : UserControl
    {
        /// <summary>
        /// Stock la vitesse maximale du navire
        /// </summary>
        public double VitesseMax { get; set; } = BatailleNavale.ListeNavire[0].VitesseNavire;

        /// <summary>
        /// Stock la velocite dans l'axe X actuel du navire
        /// </summary>
        public double VelociteX { get; set; } = 0;
        /// <summary>
        /// Stock la velocite dans l'axe Y actuel du navire
        /// </summary>
        public double VelociteY { get; set; } = 0;

        /// <summary>
        /// Initialise le bateau pirate pour qu'il apparaisse dans le jeu
        /// </summary>
        public BateauPirate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calcule de la direction du navire
        /// </summary>
        /// <param name="direction">Direction de l'acceleration</param>
        public void Accelerer(Direction direction)
        {
            if (direction == Direction.Avant && VelociteY < VitesseMax)
            {
                VelociteY += 0.2;
            } else if (direction == Direction.Arriere && VelociteY > -VitesseMax)
            {
                VelociteY -= 0.2;
            } else if (direction == Direction.Babord && VelociteX < VitesseMax)
            {
                VelociteX += 0.2;
            } else if (direction == Direction.Tribord && VelociteX > -VitesseMax)
            {
                VelociteX -= 0.2;
            }
        }

        /// <summary>
        /// Calcule l'angle du navire
        /// </summary>
        /// <returns>l'angle du navire</returns>
        public double CalculerAngle()
        {
            double angle = 0;

            if(VelociteX > 0 && VelociteY >= 0)
            {
                angle = 90 + Math.Round(Math.Atan(VelociteY / VelociteX) * 180 / Math.PI);
            } else if(VelociteX >= 0 && VelociteY < 0)
            {
                angle = - Math.Round(Math.Atan(VelociteX / VelociteY) * 180 / Math.PI);
            } else if(VelociteX <= 0 && VelociteY > 0)
            {
                angle = 180 - Math.Round(Math.Atan(VelociteX / VelociteY) * 180 / Math.PI);
            } else if(VelociteX < 0 && VelociteY <= 0)
            {
                angle = 270 + Math.Round(Math.Atan(VelociteY / VelociteX) * 180 / Math.PI);
            } else
            {
                angle = 0;
            }
            return angle;
        }

        /// <summary>
        /// Methode qui arrete le navire dans l'axe X
        /// </summary>
        public void StopX()
        {
            VelociteX = 0;
        }

        /// <summary>
        /// Methode qui arrete le navire dans l'axe Y
        /// </summary>
        public void StopY()
        {
            VelociteY = 0;
        }

        /// <summary>
        /// Stop le navire completement
        /// </summary>
        public void Stop()
        {
            StopX();
            StopY();
        }
    }
}
