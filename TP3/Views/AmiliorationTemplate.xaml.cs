using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AmiliorationArmure.xaml
    /// </summary>
    public partial class AmiliorationTemplate : INotifyPropertyChanged
    {
        private string _lienImage = String.Empty;
        public string LienImage
        {
            get { return _lienImage; }
            set
            {
                _lienImage = value;
                Image.Source = new BitmapImage(new Uri(_lienImage, UriKind.Relative));
            }
        }


        private int _prixAmilioration = 0;
        public int PrixAmilioration
        {
            get { return _prixAmilioration; }
            set
            {
                _prixAmilioration = value;

                foreach (var x in Prix.Children.OfType<AffichageArgent>())
                {
                    x.ArgentCourant = _prixAmilioration;
                }
            }
        }

        private string _nomAmilioration = String.Empty;
        public string NomAmilioration
        {
            get { return _nomAmilioration; }
            set
            {
                if (_nomAmilioration != value)
                {
                    _nomAmilioration = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AmiliorationTemplate()
        {
            InitializeComponent();
            DataContext = this;
            Prix.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Prix.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Prix.Children.Add(new AffichageArgent());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrixAmilioration += 5;
        }
    }
}
