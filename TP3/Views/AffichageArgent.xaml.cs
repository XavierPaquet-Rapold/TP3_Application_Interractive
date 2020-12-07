using System.ComponentModel;
using System.Runtime.CompilerServices;
using TP3.ViewModel;
using TP3.Models;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageArgent.xaml
    /// </summary>
    public partial class AffichageArgent : INotifyPropertyChanged
    {
        private int _argentCourant = BatailleNavale.ListeNavire[0].NbOr;
        public int ArgentCourant
        {
            get { return _argentCourant; }
            set
            {
                if (_argentCourant != value)
                {
                    _argentCourant = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AffichageArgent()
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
