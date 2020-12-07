using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TP3.Models;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AmiliorationArmure.xaml
    /// </summary>
    public partial class AmeliorationTemplate : INotifyPropertyChanged
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
        public int PrixAmelioration
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
        public string NomAmelioration
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

        public AmeliorationTemplate(string message)
        {
            InitializeComponent();
            DataContext = this;
            Prix.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Prix.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Bouton.Tag = message;
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
            Button btn = (Button)sender;
            NavireJoueur temp = (NavireJoueur)BatailleNavale.ListeNavire[0];
            if (PrixAmelioration <= temp.NbOr) {
                switch (btn.Tag)
                {
                    case "Reparation":
                        temp.ReparationCoque(PrixAmelioration);
                        break;
                    case "Recruter":
                        temp.AjoutEquipage(PrixAmelioration);
                        break;
                    case "Resistance":
                        temp.AjoutCoque(PrixAmelioration);
                        break;
                    case "Vitesse":
                        temp.AjoutVitesse(PrixAmelioration);
                        break;
                    case "CadenceTir":
                        temp.AjoutCadence(PrixAmelioration);
                        break;
                }
                BatailleNavale.ListeNavire[0] = temp;
                PrixAmelioration += 5;
            }
        }
    }
}
