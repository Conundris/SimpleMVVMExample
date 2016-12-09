using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleMVVMExample.Staff
{
    /// <summary>
    /// Interaction logic for DetailStaffView.xaml
    /// </summary>
    public partial class DetailStaffView : Window
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_staffModel != null
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
