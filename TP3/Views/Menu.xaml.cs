using System.Windows;
using System.Windows.Controls;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public bool _jeuCommence = false;

        public Menu()
        {
            _jeuCommence = false;
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
            _jeuCommence = true;
        }

        private void Quitter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
