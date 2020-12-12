using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for InterfaceJoueur.xaml
    /// </summary>
    public partial class InterfaceTir : UserControl
    {
        /// <summary>Temps de rechargement du tir</summary>
        public static int TpsRechargement { get; set; } = 0;

        /// <summary>
        /// Horloge des canons a droite
        /// </summary>
        private DispatcherTimer _horlogeDroite;
        /// <summary>
        /// horloge des canons a gauche
        /// </summary>
        private DispatcherTimer _horlogeGauche;

        public bool _tirDroitActif = false;
        public bool _tirGaucheActif = false;

        /// <summary>
        /// Initialise l'interface de tir
        /// </summary>
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
            _horlogeGauche.Interval = TimeSpan.FromSeconds(TpsRechargement);
            Rechargement(TirGauche);
            _tirGaucheActif = true;
        }

        /// <summary>
        /// Gestion du tir de droite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tirdroite_Click(object sender, RoutedEventArgs e)
        {
            _horlogeDroite.Interval = TimeSpan.FromSeconds(TpsRechargement);
            Rechargement(TirDroite);
            _tirDroitActif = true;
        }

        /// <summary>
        /// Gestion du rechargement : lance une horloge si un bouton de tir est pressee.
        /// </summary>
        /// <param name="bouton">Bouton touche</param>
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

        /// <summary>
        /// Change la vitesse de chargement des minuteries
        /// </summary>
        public void Changement_Minuterie()
        {
            TpsRechargement = BatailleNavale.ListeNavire[0].VitesseRechargeActuel;
        }
    }
}