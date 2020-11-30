using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageBarreVie.xaml
    /// </summary>
    public partial class AffichageBarreVie : INotifyPropertyChanged
    {
        private int _vieMax = 0;
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

        private int _nombreMembreEquipage = 0;
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

        public AffichageBarreVie()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
