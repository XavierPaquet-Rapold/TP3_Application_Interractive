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
using TP3.ViewModel;
using System.Windows.Media;

namespace TP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        /// <summary>Horloge principale du jeu</summary>
        private DispatcherTimer _horloge;
        /// <summary>La musique du jeu</summary>
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        /// <summary>Cree un rectangle pour la hitbox du joueur</summary>
        private Rect HitBoxJoueur
        {
            get { return new Rect(Canvas.GetLeft(BateauPirate), Canvas.GetTop(BateauPirate), BateauPirate.ActualWidth, BateauPirate.ActualHeight); }
        }

        /// <summary>
        /// Cree un rectangle pour la hitbox du port
        /// </summary>
        private Rect HitBoxPort
        {
            get
            {
                return new Rect(Canvas.GetLeft(Port), Canvas.GetTop(Port), Port.ActualWidth, Port.ActualHeight);
            }
        }

        /// <summary>Stock l'angle du bateau</summary>
        private double _angleBateau;
        /// <summary>
        /// Getter et setter de l'angle du bateau
        /// </summary>
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

        /// <summary>
        /// Implementation de INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Initialise la fenetre principale
        /// </summary>
        public MainWindow()
        {
            BatailleNavale.InitialiserJeu();
            InitializeComponent();
            //_mediaPlayer.Open(new Uri(@"../../bensound-ukulele.mp3", UriKind.RelativeOrAbsolute));
            //_mediaPlayer.Play();
            DataContext = this;
            _horloge = new DispatcherTimer();
            _horloge.Interval = TimeSpan.FromMilliseconds(20);
            _horloge.IsEnabled = true;
            _horloge.Tick += HorlogeAvance;
            _horloge.Start();
        }

        /// <summary>
        /// Methodes appeles a chaque fois que l'horloge avance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvance(object sender, EventArgs e)
        {
            DeplacerBateau();
            AnglerBateau();
            VerifierCollisionJoueurPort();
            VerifierCollisionBouletsJoueur();
            VerifierTir();
            DeplacerTir();
            ChangerProprietes();
        }

        /// <summary>
        /// Changer les proprietes du bateau du joueur a chaque fois que l'horloge avance
        /// </summary>
        public void ChangerProprietes()
        {
            Or.ArgentCourant = BatailleNavale.ListeNavire[0].NbOr;
            Equipage.NombreMembreEquipage = BatailleNavale.ListeNavire[0].NombreEquipageCourant;
            BarreVieJoueur.VieMax = BatailleNavale.ListeNavire[0].VieCoqueMax;
            BarreVieJoueur.NombreMembreEquipage = BatailleNavale.ListeNavire[0].VieCoqueCourant;
            BarreVieJoueur.VieCourante = BatailleNavale.ListeNavire[0].VieCoqueCourant;
            BateauPirate.VitesseMax = BatailleNavale.ListeNavire[0].VitesseNavire;
            BoutonsTirer.Changement_Minuterie();
            BoutonsAborder.Abordage_Actif();
            Niveau.Niveau = BatailleNavale.Niveau;
        }

        /// <summary>
        /// Les actions faites a chaque fois qu'une touche est activee 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Deplace le bateau a chaque fois que l'horloge avance
        /// </summary>
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

        /// <summary>
        /// Calcule l'angle du bateau
        /// </summary>
        private void AnglerBateau()
        {
            AngleBateau = BateauPirate.CalculerAngle();
        }

        /// <summary>
        /// Verifie les collisions du joueur avec le port
        /// </summary>
        private void VerifierCollisionJoueurPort()
        {
            if(HitBoxJoueur.IntersectsWith(HitBoxPort))
            {
                _horloge.Stop();
                OuvrirFenetreBoutique();
            } 
        }

        /// <summary>
        /// Verifie la collision de boulets avec le joueur
        /// </summary>
        private void VerifierCollisionBouletsJoueur()
        {
            foreach (var x in Mer.Children.OfType<BouletsCanon>())
            {
                Rect HitBoxBoulets = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.ActualWidth, x.ActualHeight);

                if(HitBoxBoulets.IntersectsWith(HitBoxJoueur))
                {
                    //BatailleNavale.ListeNavire[0].DegatsBoulets(20);
                }
            }
        }

        /// <summary>
        /// Deplace les boulets
        /// </summary>
        /// <param name="x"></param>
        private void DeplacerBoulets(BouletsCanon x)
        {
            double nextX = Canvas.GetLeft(x) + x.VelociteX;
            Canvas.SetLeft(x, nextX);

            double nextY = Canvas.GetTop(x) + x.VelociteY;
            Canvas.SetTop(x, nextY);
        }

        /// <summary>
        /// Verifie si un bouton pour tirer est touche et effectue les actions en consequences
        /// </summary>
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
                tir.Tag = "tirJoueur";
                Mer.Children.Add(tir);
            } else if(BoutonsTirer.TirGaucheActif) 
            {
                BoutonsTirer.TirGaucheActif = false;

                BouletsCanon tir = new BouletsCanon();
                Canvas.SetLeft(tir, Canvas.GetLeft(BateauPirate));
                Canvas.SetTop(tir, Canvas.GetTop(BateauPirate));

                tir.CalculerDirection(BateauPirate.VelociteX, BateauPirate.VelociteY, false);
                tir.CalculerAngle();
                tir.Tag = "tirJoueur";
                Mer.Children.Add(tir);
            }
        }

        /// <summary>
        /// Ouvre la fenetre de la boutique
        /// </summary>
        private void OuvrirFenetreBoutique()
        {
            FenetreBoutique boutique = new FenetreBoutique();
            boutique.Show();
            boutique.Activate();
            boutique.Closing += FermerFenetre();
            _horloge.Start();
        }

        /// <summary>
        /// Ferme la fenetre de la boutique
        /// </summary>
        /// <returns></returns>
        private CancelEventHandler FermerFenetre()
        {
            Canvas.SetLeft(BateauPirate, 500);
            Canvas.SetTop(BateauPirate, 500);
            return null;
        }

        /// <summary>
        /// Deplace tous les tirs
        /// </summary>
        public void DeplacerTir()
        {
            foreach (var x in Mer.Children.OfType<BouletsCanon>())
            {
                DeplacerBoulets(x);
            }
        }
    }
}

