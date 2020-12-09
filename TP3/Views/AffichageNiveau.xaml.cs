using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageNiveau.xaml
    /// </summary>
    public partial class AffichageNiveau : UserControl
    {
        /// <summary>Enregistre le niveau du jeu</summary>
        private int _niveau = BatailleNavale.Niveau;
        /// <summary>
        /// Getter et setter de niveau
        /// </summary>
        public int Niveau
        {
            get { return _niveau; }
            set
            {
                if (_niveau != value)
                {
                    _niveau = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Affichage des niveaux
        /// </summary>
        public AffichageNiveau()
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
