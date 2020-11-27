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

        private void Abordage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
    }
}
