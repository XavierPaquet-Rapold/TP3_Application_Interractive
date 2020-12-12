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
        /// <summary>lien de l'image de l'amelioration a mettre dans le template</summary>
        private string _lienImage = String.Empty;
        /// <summary>
        /// Getter et setter du lien de l'image de l'amelioration a mettre dans le template
        /// </summary>
        public string LienImage
        {
            get { return _lienImage; }
            set
            {
                _lienImage = value;
                Image.Source = new BitmapImage(new Uri(_lienImage, UriKind.Relative));
            }
        }

        /// <summary>
        /// Enregistre le prix de l'amelioration a mettre dans le template
        /// </summary>
        private int _prixAmilioration = 0;

        /// <summary>
        /// Getter et setter du prix de l'amelioration a mettre dans le template
        /// </summary>
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

        /// <summary>
        /// Nom de l'amelioration a mettre dans le template
        /// </summary>
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

        /// <summary>
        /// Affichage du template
        /// </summary>
        /// <param name="message">Message a mettre dans le tag du bouton</param>
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

        /// <summary>
        /// Methode qui definit les actions de chaque boutons lorsqu'il
        /// est touche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            NavireJoueur temp = (NavireJoueur)BatailleNavale.ListeNavire[0];
            if (PrixAmelioration <= temp.NbOr) {
                switch (btn.Tag)
                {
                    case "Reparation":
                        temp.ReparationCoque(PrixAmelioration);
                        PrixAmelioration += 5;
                        break;
                    case "Recruter":
                        temp.AjoutEquipage(PrixAmelioration);
                        PrixAmelioration += 5;
                        break;
                    case "Resistance":
                        temp.AjoutCoque(PrixAmelioration);
                        PrixAmelioration += 5;
                        break;
                    case "Vitesse":
                        temp.AjoutVitesse(PrixAmelioration);
                        PrixAmelioration += 5;
                        break;
                    case "CadenceTir":
                        if(BatailleNavale.ListeNavire[0].VitesseRechargeMax > 0)
                        {
                            temp.AjoutCadence(PrixAmelioration);
                            PrixAmelioration += 5;
                        }
                        else{
                            btn.IsEnabled = false;
                        }
                        break;
                }
                BatailleNavale.ListeNavire[0] = temp;
            }
        }
    }
}
