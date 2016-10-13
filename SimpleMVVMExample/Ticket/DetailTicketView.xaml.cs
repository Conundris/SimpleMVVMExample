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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
