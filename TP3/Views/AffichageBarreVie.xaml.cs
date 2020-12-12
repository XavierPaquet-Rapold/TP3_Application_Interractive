using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageBarreVie.xaml
    /// </summary>
    public partial class AffichageBarreVie : INotifyPropertyChanged
    {
        /// <summary>La vie maximale du joueur</summary>
        private int _vieMax = 0;
        /// <summary>
        /// Setter et getter de la vie maximum
        /// </summary>
        public int VieMax
        {
            get { return _vieMax; }
            set
            {
                if (_vieMax != value)
                {
                    _vieMax = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Vie courante de la coque affiche comme nombre
        /// </summary>
        private int _maxCourante = 0;
        public int VieCourante
        {
            get { return _maxCourante; }
            set
            {
                if (_maxCourante != value)
                {
                    _maxCourante = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>Vie de la coque courant affiche pour barre de progres</summary>
        private int _vieCoqueCourant = 0;
        /// <summary>
        /// Setter et getter de vie coque courant
        /// </summary>
        public int NombreMembreEquipage
        {
            get { return _vieCoqueCourant; }
            set
            {
                if (_vieCoqueCourant != value)
                {
                    _vieCoqueCourant = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Affichage de la barre de vie sur le jeu
        /// </summary>
        public AffichageBarreVie()
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
