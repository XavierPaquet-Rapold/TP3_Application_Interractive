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
using System.Collections.Generic;
using System.Windows.Media;

namespace TP3
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        /// <summary>Temps de rechargement des ennemis</summary>
        private int _tempsRechargeEnnemis = 10;
        /// <summary>Vitesse des tirs ennemis</summary>
        private int _vitesseTirEnnemis = 6;
        /// <summary>Liste qui contient les elements a detruire</summary>
        private List<Object> _recyclage = new List<object>();

        /// <summary>Horloge du menu</summary>
        private DispatcherTimer _horlogeMenu;

        /// <summary>Horloge principale du jeu</summary>
        private DispatcherTimer _horloge;

        /// <summary>Horloge pour le tir des ennemis</summary>
        private DispatcherTimer _horlogeEnnemis;

        /// <summary>La musique du jeu</summary>
        //private MediaPlayer _mediaPlayer = new MediaPlayer();

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
                if (_angleBateau != value)
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
            _horlogeMenu = new DispatcherTimer();
            _horlogeMenu.Interval = TimeSpan.FromMilliseconds(20);
            _horlogeMenu.IsEnabled = true;
            _horlogeMenu.Tick += HorlogeMenuAvance;

            BatailleNavale.InitialiserJeu();
            InitializeComponent();
            //_mediaPlayer.Open(new Uri(@"../../soundtrack.mp3", UriKind.Relative));
            //_mediaPlayer.Play();
            DataContext = this;

            InitialiserMenu();

            DataContext = this;
        }

        /// <summary>
        /// Initialise l'affichage du menu de depart
        /// </summary>
        private void InitialiserMenu()
        {
            Views.Menu menu = new Views.Menu();
            Jeu.Children.Add(menu);
            _horlogeMenu.Start();
        }
        /// <summary>
        /// Initialise le jeu au depart
        /// </summary>
        private void InitialiserJeu()
        {
            _horloge = new DispatcherTimer();
            _horloge.Interval = TimeSpan.FromMilliseconds(20);
            _horloge.IsEnabled = true;
            _horloge.Tick += HorlogeAvance;
            _horloge.Start();

            _horlogeEnnemis = new DispatcherTimer();
            _horlogeEnnemis.Interval = TimeSpan.FromSeconds(_tempsRechargeEnnemis);
            _horlogeEnnemis.IsEnabled = true;
            _horlogeEnnemis.Tick += HorlogeEnnemisAvance;
            _horlogeEnnemis.Start();

            InitialiserNavires();
        }

        /// <summary>
        /// Verifie si le jeu commence a chaque fois que l'horloge du menu avance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeMenuAvance(object sender, EventArgs e)
        {
            VerifierCommencerJeu();
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
            VerifierCollisonBouletsEnnemis();
            VerifierCollisonJoueurEnnemis();
            VerifierTir();
            DeplacerTir();
            ChangerProprietes();
            VerifierSiMort();
            DetruireBoulets();
            DetruireRecyclage();
        }

        /// <summary>
        /// Fait tirer l'ennemis des qu'il en a la possibilite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeEnnemisAvance(object sender, EventArgs e)
        {
            FaireTirerEnnemis();
        }

        /// <summary>
        /// Si le joueur appuie sur le bouton commencer le jeu, quitte le menu
        /// </summary>
        private void VerifierCommencerJeu()
        {
            var menu = Jeu.Children.OfType<Views.Menu>().FirstOrDefault();
            if (menu.JeuCommence)
            {
                QuitterMenu();
            }
        }

        /// <summary>
        /// Quitte le menu de depart
        /// </summary>
        private void QuitterMenu()
        {
            _horlogeMenu.Stop();
            var menu = Jeu.Children.OfType<Views.Menu>().FirstOrDefault();
            Jeu.Children.Remove(menu);
            InitialiserJeu();
        }

        /// <summary>
        /// Initialise les navires au debut du jeu et a chaque niveau
        /// </summary>
        public void InitialiserNavires()
        {
            RotateTransform rotate = new RotateTransform(-90);

            GalionEspagnole Galion1 = new GalionEspagnole();
            Galion1.Tag = "1";
            Canvas.SetLeft(Galion1, 40);
            Canvas.SetTop(Galion1, 112);

            EscorteEspagnole Escorte1 = new EscorteEspagnole();
            Escorte1.Tag = "2";
            Canvas.SetLeft(Escorte1, 737);
            Canvas.SetTop(Escorte1, 176);
            Escorte1.RenderTransform = rotate;

            EscorteEspagnole Escorte2 = new EscorteEspagnole();
            Escorte2.Tag = "3";
            Canvas.SetLeft(Escorte2, 120);
            Canvas.SetTop(Escorte2, 500);

            EscorteEspagnole Escorte3 = new EscorteEspagnole();
            Escorte3.Tag = "4";
            Canvas.SetLeft(Escorte3, 1716);
            Canvas.SetTop(Escorte3, 663);

            Mer.Children.Add(Galion1);
            Mer.Children.Add(Escorte1);
            Mer.Children.Add(Escorte2);
            Mer.Children.Add(Escorte3);

            BarreVieGalion.Opacity = 1;
            BarreVieEscorte2.Opacity = 1;
            BarreVieEscorte3.Opacity = 1;
            BarreVieEscorte4.Opacity = 1;
        }

        /// <summary>
        /// Changer les proprietes du bateau du joueur et de l'affichage a chaque fois que l'horloge avance
        /// </summary>
        public void ChangerProprietes()
        {
            Or.ArgentCourant = BatailleNavale.ListeNavire[0].NbOr;
            Equipage.NombreMembreEquipage = BatailleNavale.ListeNavire[0].NombreEquipageCourant;
            BarreVieJoueur.VieMax = BatailleNavale.ListeNavire[0].VieCoqueMax;
            BarreVieJoueur.NombreMembreEquipage = BatailleNavale.ListeNavire[0].VieCoqueCourant;
            BarreVieJoueur.VieCourante = BatailleNavale.ListeNavire[0].VieCoqueCourant;
            BarreVieGalion.VieMax = BatailleNavale.ListeNavire[1].VieCoqueMax;
            BarreVieGalion.NombreMembreEquipage = BatailleNavale.ListeNavire[1].VieCoqueCourant;
            BarreVieGalion.VieCourante = BatailleNavale.ListeNavire[1].VieCoqueCourant;
            BarreVieEscorte2.VieMax = BatailleNavale.ListeNavire[2].VieCoqueMax;
            BarreVieEscorte2.NombreMembreEquipage = BatailleNavale.ListeNavire[2].VieCoqueCourant;
            BarreVieEscorte2.VieCourante = BatailleNavale.ListeNavire[2].VieCoqueCourant;
            BarreVieEscorte3.VieMax = BatailleNavale.ListeNavire[3].VieCoqueMax;
            BarreVieEscorte3.NombreMembreEquipage = BatailleNavale.ListeNavire[3].VieCoqueCourant;
            BarreVieEscorte3.VieCourante = BatailleNavale.ListeNavire[3].VieCoqueCourant;
            BarreVieEscorte4.VieMax = BatailleNavale.ListeNavire[4].VieCoqueMax;
            BarreVieEscorte4.NombreMembreEquipage = BatailleNavale.ListeNavire[4].VieCoqueCourant;
            BarreVieEscorte4.VieCourante = BatailleNavale.ListeNavire[4].VieCoqueCourant;
            BoutonsTirer.Changement_Minuterie();
            BoutonsAborder.Abordage_Actif();
            BoutonNiveau.BoutonNiveauActif();
            Niveau.Niveau = BatailleNavale.Niveau;
            if (BoutonNiveau.NiveauCommence)
            {
                InitialiserNavires();
                BoutonNiveau.NiveauCommence = false;
                if(_tempsRechargeEnnemis > 2)
                {
                    _tempsRechargeEnnemis--;
                }
            }
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
        /// Fait tirer les ennemis a chaque fois qu'ils peuvent
        /// </summary>
        private void FaireTirerEnnemis()
        {
            List<BouletsCanon> BouletsCanonsEnnemis = new List<BouletsCanon>();

            foreach(var x in Mer.Children.OfType<GalionEspagnole>())
            {
                BouletsCanon tirEnnemis = new BouletsCanon();

                switch (x.Tag)
                {
                    case "1":
                        tirEnnemis._velociteX = _vitesseTirEnnemis;
                        tirEnnemis._velociteY = 0;
                        break;
                }
                Canvas.SetLeft(tirEnnemis, (Canvas.GetLeft(x) + 75));
                Canvas.SetTop(tirEnnemis, Canvas.GetTop(x) + 100);
                tirEnnemis.CalculerAngle();
                tirEnnemis.Tag = "tirEnnemis";
                BouletsCanonsEnnemis.Add(tirEnnemis);
            }

            foreach (var x in Mer.Children.OfType<EscorteEspagnole>())
            {
                BouletsCanon tirEnnemis = new BouletsCanon();
                
                switch(x.Tag)
                {
                    case "2":
                        tirEnnemis._velociteX = 0;
                        tirEnnemis._velociteY = _vitesseTirEnnemis;
                        break;
                    case "3":
                        tirEnnemis._velociteX = _vitesseTirEnnemis;
                        tirEnnemis._velociteY = 0;
                        break;
                    case "4":
                        tirEnnemis._velociteX = -_vitesseTirEnnemis;
                        tirEnnemis._velociteY = 0;
                        break;
                }

                Canvas.SetLeft(tirEnnemis, (Canvas.GetLeft(x)));
                Canvas.SetTop(tirEnnemis, Canvas.GetTop(x));
                tirEnnemis.CalculerAngle();
                tirEnnemis.Tag = "tirEnnemis";
                BouletsCanonsEnnemis.Add(tirEnnemis);
            }

            foreach(var x in BouletsCanonsEnnemis)
            {
                Mer.Children.Add(x);
            }
        }

        /// <summary>
        /// Verifie les collisions du joueur avec le port
        /// </summary>
        private void VerifierCollisionJoueurPort()
        {
            if(HitBoxJoueur.IntersectsWith(HitBoxPort) && BatailleNavale.VerificationFinNiveau())
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

                if (HitBoxBoulets.IntersectsWith(HitBoxJoueur) && x.Tag.Equals("tirEnnemis"))
                {
                    BatailleNavale.ListeNavire[0].DegatsBoulets(BatailleNavale.ListeNavire[2].Degats);
                    _recyclage.Add(x);
                }
            }
        }

        private void VerifierCollisonBouletsEnnemis()
        {
            foreach (var x in Mer.Children.OfType<BouletsCanon>())
            {
                Rect HitBoxBoulets = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.ActualWidth, x.ActualHeight);

                if (x.Tag.Equals("tirJoueur"))
                {
                    foreach (var y in Mer.Children.OfType<EscorteEspagnole>())
                    {
                        Rect HitBoxEscorteEspagnole = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.ActualWidth, y.ActualHeight);

                        if (HitBoxEscorteEspagnole.IntersectsWith(HitBoxBoulets))
                        {
                            BatailleNavale.ListeNavire[int.Parse((string)y.Tag)].DegatsBoulets(BatailleNavale.ListeNavire[0].Degats);

                            _recyclage.Add(x);
                        }
                    }

                    foreach (var y in Mer.Children.OfType<GalionEspagnole>())
                    {
                        Rect HitBoxGalionEspagnol = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.ActualWidth, y.ActualHeight);

                        if (HitBoxGalionEspagnol.IntersectsWith(HitBoxBoulets))
                        {
                            BatailleNavale.ListeNavire[int.Parse((string)y.Tag)].DegatsBoulets(BatailleNavale.ListeNavire[0].Degats);
                            _recyclage.Add(x);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deplace les boulets
        /// </summary>
        /// <param name="x"></param>
        private void DeplacerBoulets(BouletsCanon x)
        {
            double nextX = Canvas.GetLeft(x) + x._velociteX;
            Canvas.SetLeft(x, nextX);

            double nextY = Canvas.GetTop(x) + x._velociteY;
            Canvas.SetTop(x, nextY);
        }

        /// <summary>
        /// Verifie si un bouton pour tirer est touche et effectue les actions en consequences
        /// </summary>
        private void VerifierTir()
        {
            if(BoutonsTirer._tirDroitActif)
            {
                BoutonsTirer._tirDroitActif = false;

                BouletsCanon tir = new BouletsCanon();
                Canvas.SetLeft(tir, (Canvas.GetLeft(BateauPirate)));
                Canvas.SetTop(tir, Canvas.GetTop(BateauPirate));

                tir.CalculerDirection(BateauPirate.VelociteX, BateauPirate.VelociteY, true);
                tir.CalculerAngle();
                tir.Tag = "tirJoueur";
                Mer.Children.Add(tir);

            } else if(BoutonsTirer._tirGaucheActif) 
            {
                BoutonsTirer._tirGaucheActif = false;

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
        /// <summary>
        /// Verifie si la collision du bateau du joueur avec celui des ennemis
        /// </summary>
        private void VerifierCollisonJoueurEnnemis()
        {
            foreach (var x in Mer.Children.OfType<GalionEspagnole>())
            {
                Rect HitBoxGalionEspagnol = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.ActualWidth, x.ActualHeight);

                if (HitBoxJoueur.IntersectsWith(HitBoxGalionEspagnol))
                {
                    BoutonsAborder.IsEnabled = true;
                }
                else
                {
                    BoutonsAborder.IsEnabled = false;
                }
            }
        }
        /// <summary>
        /// Verifie si des bateaux sont morts
        /// </summary>
        public void VerifierSiMort()
        {
            foreach (var x in Mer.Children.OfType<GalionEspagnole>())
            {
                if (BatailleNavale.ListeNavire[1].VieCoqueCourant == 0)
                {
                    _recyclage.Add(x);
                    BarreVieGalion.Opacity = 0;
                }
            }

            foreach (var x in Mer.Children.OfType<EscorteEspagnole>())
            {
                if (BatailleNavale.ListeNavire[int.Parse((string)x.Tag)].VieCoqueCourant == 0)
                {
                    _recyclage.Add(x);
                    switch (int.Parse((string)x.Tag))
                    {
                        case 2:
                            BarreVieEscorte2.Opacity = 0;
                            break;
                        case 3:
                            BarreVieEscorte3.Opacity = 0;
                            break;
                        case 4:
                            BarreVieEscorte4.Opacity = 0;
                            break;
                    }
                }
            }

            foreach (var x in Mer.Children.OfType<BateauPirate>())
            {
                if (BatailleNavale.ListeNavire[0].VieCoqueCourant == 0)
                {
                    _recyclage.Add(x);
                    InitialiserMenu();
                }
            }
        }

        /// <summary>
        /// Methode qui detruit des boulets de canons 
        /// qui on disparu de l'ecran du joueur
        /// </summary>
        private void DetruireBoulets()
        {
            foreach (var x in Mer.Children.OfType<BouletsCanon>())
            {
                if (Canvas.GetLeft(x) <= 0 || Canvas.GetLeft(x) >= Mer.ActualWidth)
                {
                    _recyclage.Add(x);
                } else if (Canvas.GetTop(x) <= 0 || Canvas.GetTop(x) >= Mer.ActualHeight) {
                    _recyclage.Add(x);
                }
            }
        }

        /// <summary>
        /// Methode qui enleve les elements de la liste recyclage du
        /// front-end et vidde la liste recyclage
        /// </summary>
        private void DetruireRecyclage()
        {
            for (int i = 0; i < _recyclage.Count; i++)
            {
                Mer.Children.Remove((UIElement)_recyclage[i]);
            }
            _recyclage.Clear();
        }
    }
}