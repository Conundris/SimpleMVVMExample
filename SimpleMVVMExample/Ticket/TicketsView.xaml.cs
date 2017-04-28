using System.Windows;
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

        private void btnPrintTicket_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var Printdlg = new PrintDialog();
            if (Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                dgTickets.Measure(pageSize);
                dgTickets.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(dgTickets, "Tickets");
            }
        }
    }
}
