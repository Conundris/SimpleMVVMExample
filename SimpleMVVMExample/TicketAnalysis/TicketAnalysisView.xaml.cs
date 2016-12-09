using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace SimpleMVVMExample.TicketAnalysis
{
    /// <summary>
    /// Interaction logic for TicketAnalysisView.xaml
    /// </summary>
    public partial class TicketAnalysisView : UserControl
    {
        public TicketAnalysisView()
        {
            InitializeComponent();
        }

        private void btnRunAnalysis_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Generating Ticket Analysis");

            dgTicketAnalysis.Items.Add(new TicketAnalysisModel { Type = "Tickets Closed", Value = "5" });
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Printing Ticket Analysis");
        }
    }

    public class TicketAnalysisModel
    {
        public string Type;
        public string Value;
    }
}
