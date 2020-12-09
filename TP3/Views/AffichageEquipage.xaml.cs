using System.ComponentModel;
using System.Runtime.CompilerServices;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageEquipage.xaml
    /// </summary>
    public partial class AffichageEquipage : INotifyPropertyChanged
    {
        /// <summary>
        /// Nombre d'equipage courant du bateau du joueur
        /// </summary>
        private int _nombreMembreEquipage = BatailleNavale.ListeNavire[0].NombreEquipageCourant;
        public int NombreMembreEquipage
        {
            get { return _nombreMembreEquipage; }
            set
            {
                if (_nombreMembreEquipage != value)
                {
                    _nombreMembreEquipage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Affiche le nombre de membre de l'équipage du bateau
        /// </summary>
        public AffichageEquipage()
        {
            InitializeComponent();
            DataContext = this;
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
    }
}
