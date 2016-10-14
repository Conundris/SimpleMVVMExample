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

        private void dgTickets_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgCustomers.SelectedItem == null) return;
            var selectedTicket = dgCustomers.SelectedItem as CustomerModel;

            new DetailCustomerView(selectedTicket).ShowDialog();

            //MessageBox.Show($"The Person you double clicked on is - ID: {selectedTicket.TicketId}, Title: {selectedTicket.TicketTitle}");
        }
    }
}
