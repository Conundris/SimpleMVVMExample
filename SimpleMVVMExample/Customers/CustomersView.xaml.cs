using System.Windows;
using System.Windows.Controls;
using DetailCustomerView = SimpleMVVMExample.Customers.DetailCustomerView;

namespace SimpleMVVMExample
{
    /// <summary>
    /// Interaction logic for TicketsView.xaml
    /// </summary>
    public partial class CustomersView : UserControl
    {
        public CustomersView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Printing List of Customers.");
        }
    }
}
