using System.Windows;
using SimpleMVVMExample.Helper_Classes;

namespace SimpleMVVMExample.Staff
{
    /// <summary>
    /// Interaction logic for DetailStaffView.xaml
    /// </summary>
    public partial class DetailStaffView : Window, ICloseable
    {
        private StaffModel _staffModel;

        public DetailStaffView()
        {
            InitializeComponent();
            _staffModel = new StaffModel();
        }

        // For editing new Customer
        public DetailStaffView(StaffModel staffModel)
        {
            _staffModel = staffModel;
            InitializeComponent();
            DataContext = staffModel;
        }
    }
}
