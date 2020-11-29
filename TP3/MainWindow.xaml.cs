using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TP3.Models;
using TP3.Views;

namespace TP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private DispatcherTimer _horloge;

        private Rect HitBoxJoueur
        {
            get { return new Rect(Canvas.GetLeft(BateauPirate), Canvas.GetTop(BateauPirate), BateauPirate.ActualWidth, BateauPirate.ActualHeight); }
        }

        private Rect HitBoxPort
        {
            get
            {
                return new Rect(Canvas.GetLeft(Port), Canvas.GetTop(Port), Port.ActualWidth, Port.ActualHeight);
            }
        }

        private double _angleBateau;
        public double AngleBateau
        {
            get { return _angleBateau; }
            set
            {
                if(_angleBateau != value)
                {
                    _angleBateau = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _horloge = new DispatcherTimer();
            _horloge.Interval = TimeSpan.FromMilliseconds(20);
            _horloge.IsEnabled = true;
            _horloge.Tick += HorlogeAvance;
            _horloge.Start();
        }

        private void HorlogeAvance(object sender, EventArgs e)
        {
            DeplacerBateau();
            AnglerBateau();
            VerifierCollisionJoueurPort();
            VerifierCollisionBouletsJoueur();
            VerifierTir();
            DeplacerTir();
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

        private void DeplacerBateau()
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

        private void AnglerBateau()
        {
            AngleBateau = BateauPirate.CalculerAngle();
        }

        private void VerifierCollisionJoueurPort()
        {
            if(HitBoxJoueur.IntersectsWith(HitBoxPort))
            {
                _horloge.Stop();
                OuvrirFenetreBoutique();
            } 
        }

        private void VerifierCollisionBouletsJoueur()
        {
            foreach (var x in Mer.Children.OfType<BouletsCanon>())
            {
                Rect HitBoxBoulets = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.ActualWidth, x.ActualHeight);

                if(HitBoxBoulets.IntersectsWith(HitBoxJoueur))
                {

                }
            }
        }


        private void DeplacerBoulets(BouletsCanon x)
        {
            double nextX = Canvas.GetLeft(x) + x.VelociteX;
            Canvas.SetLeft(x, nextX);

            double nextY = Canvas.GetTop(x) + x.VelociteY;
            Canvas.SetTop(x, nextY);
        }

        private void VerifierTir()
        {
            if(BoutonsTirer.TirDroitActif)
            {
                BoutonsTirer.TirDroitActif = false;

                BouletsCanon tir = new BouletsCanon();
                Canvas.SetLeft(tir, (Canvas.GetLeft(BateauPirate)));
                Canvas.SetTop(tir, Canvas.GetTop(BateauPirate));

                tir.CalculerDirection(BateauPirate.VelociteX, BateauPirate.VelociteY, true);
                tir.CalculerAngle();
                tir.Tag = "tirDroit";
                Mer.Children.Add(tir);
            } else if(BoutonsTirer.TirGaucheActif) 
            {
                BoutonsTirer.TirGaucheActif = false;

                BouletsCanon tir = new BouletsCanon();
                Canvas.SetLeft(tir, Canvas.GetLeft(BateauPirate));
                Canvas.SetTop(tir, Canvas.GetTop(BateauPirate));

                tir.CalculerDirection(BateauPirate.VelociteX, BateauPirate.VelociteY, false);
                tir.CalculerAngle();
                tir.Tag = "tirGauche";
                Mer.Children.Add(tir);
            }
        }

        private void OuvrirFenetreBoutique()
        {
            FenetreBoutique boutique = new FenetreBoutique();
            boutique.Show();
            boutique.Activate();
            boutique.Closing += fermerFenetre();
            _horloge.Start();
        }

        private CancelEventHandler fermerFenetre()
        {
            Canvas.SetLeft(BateauPirate, 500);
            Canvas.SetTop(BateauPirate, 500);
            return null;
        }

        public void DeplacerTir()
        {
            foreach (var x in Mer.Children.OfType<BouletsCanon>())
            {
                DeplacerBoulets(x);
            }
        }
    }
}

