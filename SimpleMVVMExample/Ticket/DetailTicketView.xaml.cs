using System.Windows;

namespace SimpleMVVMExample.Ticket
{
    /// <summary>
    /// Interaction logic for DetailTicketView.xaml
    /// </summary>
    public partial class DetailTicketView : Window
    {
        private TicketModel _ticketModel;

        public DetailTicketView(TicketModel ticketModel)
        {
            _ticketModel = ticketModel;
            InitializeComponent();
            DataContext = ticketModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_ticketModel != null
                ? "Entry has successfully been created."
                : "Entry has Successfully been updated.");
            this.Close();
        }

        private void TicketNote_Click(object sender, RoutedEventArgs e)
        {
            new TicketNoteView().ShowDialog();
            lbTicketNotes.Items.Add("1930-01-01: Ticket Note Description");
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            new PaymentView().ShowDialog();
            txtPaymentReceived.Text = 45.ToString();
        }

        private void btnPrintTicket_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Printing Ticket.");
        }
    }
}
