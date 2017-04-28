using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMVVMExample.Staff
{
    /// <summary>
    /// Interaction logic for StaffView.xaml
    /// </summary>
    public partial class StaffView : UserControl
    {
        public StaffView()
        {
            InitializeComponent();
        }

        private void PrintButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var Printdlg = new PrintDialog();
            if (Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                dgStaff.Measure(pageSize);
                dgStaff.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(dgStaff, "Staff");
                dgStaff.UpdateLayout();
                dgStaff.UpdateDefaultStyle();
            }
        }
    }
}
