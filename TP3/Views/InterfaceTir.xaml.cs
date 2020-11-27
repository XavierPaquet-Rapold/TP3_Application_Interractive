using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for InterfaceJoueur.xaml
    /// </summary>
    public partial class InterfaceTir : UserControl
    {
        private int TpsRechargement = 10;

        private DispatcherTimer _horlogeDroite;
        private DispatcherTimer _horlogeGauche;

        public bool TirDroitActif = false;
        public bool TirGaucheActif = false;

        public InterfaceTir()
        {
            InitializeComponent();
            _horlogeDroite = new DispatcherTimer();
            _horlogeGauche = new DispatcherTimer();

            _horlogeDroite.Tick += HorlogeDroiteAvance;
            _horlogeGauche.Tick += HorlogeGaucheAvance;

            _horlogeDroite.Interval = TimeSpan.FromSeconds(TpsRechargement);
            _horlogeGauche.Interval = TimeSpan.FromSeconds(TpsRechargement);

            _horlogeDroite.IsEnabled = true;
            _horlogeGauche.IsEnabled = true;
        }

        private void HorlogeDroiteAvance(object sender, EventArgs e)
        {
            TirDroite.IsEnabled = true;
            _horlogeDroite.Stop();
        }

        private void HorlogeGaucheAvance(object sender, EventArgs e)
        {
            TirGauche.IsEnabled = true;
            _horlogeGauche.Stop();
        }

        private void TirGauche_Click(object sender, RoutedEventArgs e)
        {
            Rechargement(TirGauche);
            TirGaucheActif = true;
        }

        private void Tirdroite_Click(object sender, RoutedEventArgs e)
        {
            Rechargement(TirDroite);
            TirDroitActif = true;
        }

        private void Rechargement(Button bouton)
        {
            bouton.IsEnabled = false;

            if(bouton.Equals(TirDroite))
            {
                _horlogeDroite.Start();
            } else if(bouton.Equals(TirGauche))
            {
                _horlogeGauche.Start();
            }
        }
    }
}