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

            AmeliorationTemplate Reparation = new AmeliorationTemplate("Reparation");
            Reparation.LienImage = "Images/reparer.png";
            Reparation.PrixAmelioration = 500;
            Reparation.NomAmelioration = "Réparer";
            Fenetre.Children.Add(Reparation);

            AmeliorationTemplate Recruter = new AmeliorationTemplate("Recruter");
            Recruter.LienImage = "Images/recruter.png";
            Recruter.PrixAmelioration = 500;
            Recruter.NomAmelioration = "Recruter";
            Fenetre.Children.Add(Recruter);

            AmeliorationTemplate AmeliorationResistance = new AmeliorationTemplate("Resistance");
            AmeliorationResistance.LienImage = "Images/armure.png";
            AmeliorationResistance.PrixAmelioration = 500;
            AmeliorationResistance.NomAmelioration = "+ Résistance";
            Fenetre.Children.Add(AmeliorationResistance);

            AmeliorationTemplate AmeliorationVitesse = new AmeliorationTemplate("Vitesse");
            AmeliorationVitesse.LienImage = "Images/vitesse.png";
            AmeliorationVitesse.PrixAmelioration = 800;
            AmeliorationVitesse.NomAmelioration = "+ Vitesse";
            Fenetre.Children.Add(AmeliorationVitesse);

            AmeliorationTemplate AmeliorationCadenceTir = new AmeliorationTemplate("CadenceTir");
            AmeliorationCadenceTir.LienImage = "Images/canon.png";
            AmeliorationCadenceTir.PrixAmelioration = 800;
            AmeliorationCadenceTir.NomAmelioration = "+ Cadence";
            Fenetre.Children.Add(AmeliorationCadenceTir);

            AlignerInterfaces();
        }

        /// <summary>
        /// Aligne toutes les interfaces d'amelioration crees.
        /// </summary>
        private void AlignerInterfaces()
        {
            int positionX = 20;
            int positionY = 20;
            foreach (var x in Fenetre.Children.OfType<AmeliorationTemplate>())
            {
                Canvas.SetLeft(x, positionX);
                Canvas.SetTop(x, positionY);
                positionX += 250;
            }
        }
    }
}
