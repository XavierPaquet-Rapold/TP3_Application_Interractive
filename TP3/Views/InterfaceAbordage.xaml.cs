using System.Windows.Controls;
using TP3.ViewModel;
using TP3.Models;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for InterfaceAbordage.xaml
    /// </summary>
    public partial class InterfaceAbordage : UserControl
    {
        /// <summary>
        /// Initialise l'interface de l'abordage
        /// </summary>
        public InterfaceAbordage()
        {
            InitializeComponent();
            Abordage.IsEnabled = false;
        }

        /// <summary>
        /// Gestion de l'abordage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Abordage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavireJoueur joueur = (NavireJoueur)BatailleNavale.ListeNavire[0];
            GalionEspagnol galion = (GalionEspagnol)BatailleNavale.ListeNavire[1];
            joueur.Abordage(galion.NombreEquipageCourant, galion.NbOr);
            galion.Abordage();
            BatailleNavale.ListeNavire[0] = joueur;
            BatailleNavale.ListeNavire[1] = galion;
            Abordage.IsEnabled = false;
        }

        /// <summary>
        /// Verifie si l'abordage est possible
        /// </summary>
        public void Abordage_Actif()
        {
            if (BatailleNavale.ListeNavire[1].NombreEquipageCourant > 0 && BatailleNavale.ListeNavire[1].CalculeNbVieCoque() < 0.5)
            {
                Abordage.IsEnabled = true;
            }
            else
            {
                Abordage.IsEnabled = false;
            }
        }
    }
}
