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

        private double CalculerCoefVitesse(double VBateauX, double VBateauY)
        {
            //return Math.Round(vitesseBoulets / (Math.Sqrt(VBateauX) + Math.Sqrt(VBateauY)));
            return 5;
        }

        public void CalculerDirection(double VBateauX, double VBateauY, bool cote) 
        {
            if(cote)
            {
                VelociteX = -VBateauY * CalculerCoefVitesse(VBateauX, VBateauY);
                VelociteY = VBateauX * CalculerCoefVitesse(VBateauX, VBateauY);
            } else
            {
                VelociteX = VBateauY * CalculerCoefVitesse(VBateauX, VBateauY);
                VelociteY = -VBateauX * CalculerCoefVitesse(VBateauX, VBateauY);
            }
        }

        public void CalculerAngle()
        {
            RotationImage.Angle = Math.Round(Math.Atan(VelociteY / VelociteX) * 180 / Math.PI);
        }

        public void CalculerDistance()
        {
           
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
