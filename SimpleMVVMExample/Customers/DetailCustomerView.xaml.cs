using System.Windows;

namespace SimpleMVVMExample.Customers
{
    /// <summary>
    /// Interaction logic for DetailTicketView.xaml
    /// </summary>
    public partial class DetailCustomerView : Window
    {
        private CustomerModel _customerModel;

        public DetailCustomerView(CustomerModel customerModel)
        {
            _customerModel = customerModel;
            InitializeComponent();
            DataContext = customerModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_customerModel != null
                ? "Entry has successfully been created."
                : "Entry has Successfully been updated.");
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
