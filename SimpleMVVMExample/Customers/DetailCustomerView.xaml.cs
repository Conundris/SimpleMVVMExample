using System.Windows;
using SimpleMVVMExample.Helper_Classes;

namespace SimpleMVVMExample.Customers
{
    /// <summary>
    /// Interaction logic for DetailTicketView.xaml
    /// </summary>
    public partial class DetailCustomerView : Window, ICloseable
    {
        private CustomerModel _customerModel;

        // For creating new Customer
        public DetailCustomerView()
        {
            InitializeComponent();
            _customerModel = new CustomerModel();
            DataContext = new DetailCustomerViewModel(_customerModel);
        }

        // For editing new Customer
        public DetailCustomerView(CustomerModel customerModel)
        {
            _customerModel = customerModel;
            InitializeComponent();
            DataContext = new DetailCustomerViewModel(customerModel);
        }
    }
}
