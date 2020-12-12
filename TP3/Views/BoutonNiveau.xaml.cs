using System.Windows.Controls;
using TP3.ViewModel;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for BoutonNiveau.xaml
    /// </summary>
    public partial class BoutonNiveau : UserControl
    {
        private bool _niveauCommence = false;

        public bool NiveauCommence { get { return _niveauCommence; } set { _niveauCommence = value; } }

        public BoutonNiveau()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gestion du changement de niveau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangerNiveau_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BatailleNavale.Niveau++;
            BatailleNavale.InitialiserNiveau();
            ChangerNiveau.Opacity = 0;
            ChangerNiveau.IsEnabled = false;
            _niveauCommence = true;
        }
        /// <summary>
        /// Decide si le bouton de changement de niveau est actif ou non
        /// </summary>
        public void BoutonNiveauActif()
        {
            if (BatailleNavale.VerificationFinNiveau())
            {
                ChangerNiveau.Opacity = 0.5;
                ChangerNiveau.IsEnabled = true;
            }
            else
            {
                ChangerNiveau.Opacity = 0;
                ChangerNiveau.IsEnabled = false;
            }
        }
    }
}
