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

        /// <summary>
        /// Stop de l'horloge et activation du bouton droit
        /// quand le joueur peut de nouveau tirer a droite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeDroiteAvance(object sender, EventArgs e)
        {
            TirDroite.IsEnabled = true;
            _horlogeDroite.Stop();
        }

        /// <summary>
        /// Stop de l'horloge et activation du bouton gauche 
        /// quand le joueur peut de nouveau tirer a gauche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeGaucheAvance(object sender, EventArgs e)
        {
            TirGauche.IsEnabled = true;
            _horlogeGauche.Stop();
        }

        /// <summary>
        /// Gestion du tir de gauche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TirGauche_Click(object sender, RoutedEventArgs e)
        {
            Rechargement(TirGauche);
            TirGaucheActif = true;
        }

        /// <summary>
        /// Gestion du tir de droite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tirdroite_Click(object sender, RoutedEventArgs e)
        {
            Rechargement(TirDroite);
            TirDroitActif = true;
        }

        /// <summary>
        /// Gestion du rechargement : lance une horloge si un bouton de tir est pressee.
        /// </summary>
        /// <param name="bouton"></param>
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