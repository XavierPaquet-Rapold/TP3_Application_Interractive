using System.Windows.Controls;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for InterfaceAbordage.xaml
    /// </summary>
    public partial class InterfaceAbordage : UserControl
    {
        public InterfaceAbordage()
        {
            InitializeComponent();
            Abordage.IsEnabled = false;
        }

        /// <summary>
        /// Gestion de l'abordage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Abordage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
    }
}
