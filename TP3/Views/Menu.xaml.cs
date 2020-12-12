using System.Windows;
using System.Windows.Controls;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        private bool _jeuCommence = false;

        public bool JeuCommence { get { return _jeuCommence; } private set { _jeuCommence = value; } }

        public Menu()
        {
            InitializeComponent();
            Tutoriel.Opacity = 0;
        }

        private void Tutoriel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Tutoriel.Opacity == 0)
            {
                Tutoriel.Opacity = 1;
            } else Tutoriel.Opacity = 0;
        }

        private void Jouer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BatailleNavale.InitialiserJeu();
            _jeuCommence = true;
        }

        private void Quitter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
