using System.Windows;
using System.Windows.Controls;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Printdlg = new PrintDialog();
            if (Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                dgCustomers.Measure(pageSize);
                dgCustomers.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(dgCustomers, "Customers");
            }
        }
    }
}
