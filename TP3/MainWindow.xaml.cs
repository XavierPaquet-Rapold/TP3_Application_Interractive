using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TP3.Models;

namespace TP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _horloge;

        public MainWindow()
        {
            InitializeComponent();
            _horloge = new DispatcherTimer();
            _horloge.Interval = TimeSpan.FromMilliseconds(20);
            _horloge.IsEnabled = true;
            _horloge.Tick += HorlogeAvance;
            _horloge.Start();
        }

        private void HorlogeAvance(object sender, EventArgs e)
        {
            DeplacerVaisseau();
            VerifierCollision();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();   // Quitte le jeu.
                    break;

                case Key.Up:
                    BateauPirate.Accelerer(Direction.Arriere);
                    break;

                case Key.W:
                    BateauPirate.Accelerer(Direction.Arriere);
                    break;

                case Key.Left:
                    BateauPirate.Accelerer(Direction.Tribord);
                    break;

                case Key.A:
                    BateauPirate.Accelerer(Direction.Tribord);
                    break;

                case Key.Down:
                    BateauPirate.Accelerer(Direction.Avant);
                    break;

                case Key.S:
                    BateauPirate.Accelerer(Direction.Avant);
                    break;

                case Key.Right:
                    BateauPirate.Accelerer(Direction.Babord);
                    break;

                case Key.D:
                    BateauPirate.Accelerer(Direction.Babord);
                    break;
            }
        }

        private void DeplacerVaisseau()
        {
            double nextX = Canvas.GetLeft(BateauPirate) + BateauPirate.VelociteX;
            if (nextX < 0)
            {
                BateauPirate.StopX();
                nextX = 0;
            }
            else if (nextX + BateauPirate.ActualWidth > Mer.ActualWidth)
            {
                nextX = Mer.ActualWidth - BateauPirate.ActualWidth;
            }

            Canvas.SetLeft(BateauPirate, nextX);

            double nextY = Canvas.GetTop(BateauPirate) + BateauPirate.VelociteY;
            if (nextY < 0)
            {
                BateauPirate.StopY();
                nextY = 0;
            }
            else if (nextY + BateauPirate.ActualHeight > Mer.ActualHeight)
            {
                nextY = Mer.ActualHeight - BateauPirate.ActualHeight;
            }

            Canvas.SetTop(BateauPirate, nextY);
        }

        private void VerifierCollision()
        {
            Rect HitBoxJoueur = new Rect(Canvas.GetLeft(BateauPirate), Canvas.GetTop(BateauPirate), BateauPirate.ActualWidth, BateauPirate.ActualHeight);
            Rect HitBoxPort = new Rect(Canvas.GetLeft(Port), Canvas.GetTop(Port), Port.ActualWidth, Port.ActualHeight);

            if(HitBoxJoueur.IntersectsWith(HitBoxPort))
            {
                Port.Background = Brushes.Gray;
            } else
            {
                Port.Background = Brushes.Transparent;
            }
        }
    }
}

