using System.Windows.Controls;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for AffichageArgent.xaml
    /// </summary>
    public partial class AffichageArgent : UserControl
    {
        private int argentCourant = 350;

        public AffichageArgent()
        {
            InitializeComponent();
            RafraichirAffichageArgent();
        }

        public void RafraichirAffichageArgent()
        {
            TextArgent.Text = argentCourant.ToString();
        }
    }
}
