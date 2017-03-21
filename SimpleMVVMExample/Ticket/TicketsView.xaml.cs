using System.Windows.Controls;
using SimpleMVVMExample.Ticket;

namespace SimpleMVVMExample
{
    /// <summary>
    /// Interaction logic for TicketsView.xaml
    /// </summary>
    public partial class TicketsView : UserControl
    {
        public TicketsView()
        {
            InitializeComponent();
        }

        private void dgTickets_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem == null) return;
            var selectedTicket = dgTickets.SelectedItem as TicketModel;

            new DetailTicketView(selectedTicket).ShowDialog();

            //MessageBox.Show($"The Person you double clicked on is - ID: {selectedTicket.INTTICKETID}, Title: {selectedTicket.STRTICKETTITLE}");
        }
    }
}
