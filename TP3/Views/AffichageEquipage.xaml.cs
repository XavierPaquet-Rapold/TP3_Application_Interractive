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

        public AffichageEquipage()
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
