﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using TP3.ViewModel;
namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageArgent.xaml
    /// </summary>
    public partial class AffichageArgent : INotifyPropertyChanged
    {
        /// <summary>Argent courant du navire du joueur</summary>
        private int _argentCourant = BatailleNavale.ListeNavire[0].NbOr;

        /// <summary>
        /// setter et getter de argent courant
        /// </summary>
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

        /// <summary>
        /// Affichage du nombre d'or du joueur
        /// </summary>
        public AffichageArgent()
        {
            InitializeComponent();
            DataContext = this;
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
    }
}
