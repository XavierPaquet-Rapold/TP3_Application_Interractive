using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for FenetreBoutique.xaml
    /// </summary>
    public partial class FenetreBoutique : Window
    {
        public FenetreBoutique()
        {
            InitializeComponent();

            AmiliorationTemplate Reparation = new AmiliorationTemplate();
            Reparation.LienImage = "Images/reparer.png";
            Reparation.PrixAmilioration = 500;
            Reparation.NomAmilioration = "Réparer";
            Fenetre.Children.Add(Reparation);

            AmiliorationTemplate Recruter = new AmiliorationTemplate();
            Recruter.LienImage = "Images/recruter.png";
            Recruter.PrixAmilioration = 500;
            Recruter.NomAmilioration = "Recruter";
            Fenetre.Children.Add(Recruter);

            AmiliorationTemplate AmiliorationResistance = new AmiliorationTemplate();
            AmiliorationResistance.LienImage = "Images/armure.png";
            AmiliorationResistance.PrixAmilioration = 500;
            AmiliorationResistance.NomAmilioration = "+ Résistance";
            Fenetre.Children.Add(AmiliorationResistance);

            AmiliorationTemplate AmiliorationVitesse = new AmiliorationTemplate();
            AmiliorationVitesse.LienImage = "Images/vitesse.png";
            AmiliorationVitesse.PrixAmilioration = 800;
            AmiliorationVitesse.NomAmilioration = "+ Vitesse";
            Fenetre.Children.Add(AmiliorationVitesse);

            AmiliorationTemplate AmiliorationDegat = new AmiliorationTemplate();
            AmiliorationDegat.LienImage = "Images/degat.png";
            AmiliorationDegat.PrixAmilioration = 800;
            AmiliorationDegat.NomAmilioration = "+ Degat";
            Fenetre.Children.Add(AmiliorationDegat);

            AmiliorationTemplate AmiliorationCadenceTir = new AmiliorationTemplate();
            AmiliorationCadenceTir.LienImage = "Images/canon.png";
            AmiliorationCadenceTir.PrixAmilioration = 800;
            AmiliorationCadenceTir.NomAmilioration = "+ Cadence";
            Fenetre.Children.Add(AmiliorationCadenceTir);

            AlignerInterfaces();
        }

        /// <summary>
        /// Aligne toutes les interfaces d'amilioration crees.
        /// </summary>
        private void AlignerInterfaces()
        {
            int positionX = 20;
            int positionY = 20;
            foreach (var x in Fenetre.Children.OfType<AmiliorationTemplate>())
            {
                Canvas.SetLeft(x, positionX);
                Canvas.SetTop(x, positionY);
                positionX += 250;
            }
        }
    }
}
