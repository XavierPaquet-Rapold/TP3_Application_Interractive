using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TP3.Models;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for BateauPirate.xaml
    /// </summary>
    public partial class BateauPirate : UserControl
    {
        public double VitesseMax { get; set; } = BatailleNavale.ListeNavire[0].VitesseNavire;

        public double VelociteX { get; set; } = 0;
        public double VelociteY { get; set; } = 0;

        public BateauPirate()
        {
            InitializeComponent();
        }

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

        public void StopX()
        {
            VelociteX = 0;
        }

        public void StopY()
        {
            VelociteY = 0;
        }

        public void Stop()
        {
            StopX();
            StopY();
        }
    }
}
