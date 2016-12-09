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

        private void dgTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgCustomers.SelectedItem == null) return;
            var selectedStaff = dgCustomers.SelectedItem as StaffModel;

            new DetailStaffView(selectedStaff).ShowDialog();
        }
    }
}
