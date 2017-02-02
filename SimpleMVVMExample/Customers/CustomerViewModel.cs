using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Utility;

namespace SimpleMVVMExample.Customers
{
    public class CustomerViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _customerId;
        private ObservableCollection<CustomerModel> _customers = new ObservableCollection<CustomerModel>();
        private CustomerModel _selectedCustomer;
        private ICommand _createCustomerCommand;
        private ICommand _openDetailCustomerCommand;
        private ICommand _deleteCustomerCommand;
        private ICommand _deRegisterCustomerCommand;
        private ICommand _searchCustomersCommand;

        #endregion

        public CustomerViewModel()
        {
            Customers = new ObservableCollection<CustomerModel>();
        }

        #region Properties/Commands

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (value == _selectedCustomer || value == null) return;
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers ?? (_customers = new ObservableCollection<CustomerModel>()); }
            set
            {
                if (value == null) return;
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        public string Name => "Customers";

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value == _customerId) return;
                _customerId = value;
                OnPropertyChanged("CustomerId");
            }
        }

        public ICommand OpenDetailCustomerCommand
        {
            get
            {
                return _openDetailCustomerCommand ?? (_openDetailCustomerCommand = new RelayCommand(
                           param => ShowWindow(),
                           param => (SelectedCustomer != null)
                       ));
            }
        }

        public ICommand CreateCustomerCommand
        {
            get
            {
                return _createCustomerCommand ?? (_createCustomerCommand = new RelayCommand(
                           param => CreateCustomer()
                       ));
            }
        }

        public ICommand DeleteCustomerCommand
        {
            get
            {
                return _deleteCustomerCommand ?? (_deleteCustomerCommand = new RelayCommand(
                           param => DeleteCustomer(),
                           param => (SelectedCustomer != null)
                       ));
            }
        }

        public ICommand DeRegisterCustomerCommand
        {
            get
            {
                return _deRegisterCustomerCommand ?? (_deRegisterCustomerCommand = new RelayCommand(
                           param => DeRegisterCustomer(),
                           param => (SelectedCustomer != null)
                       ));
            }
        }

        public ICommand SearchCustomersCommand
        {
            get
            {
                return _searchCustomersCommand ?? (_searchCustomersCommand = new RelayCommand(
                           param => getCustomers()
                       ));
            }
        }

        #endregion

        #region Methods

        private void DeRegisterCustomer()
        {
            if (SelectedCustomer == null) return;
            SelectedCustomer.BLNACTIVE = false;
            MessageBox.Show("Customer has been successfully deregistered.");
        }

        private void CreateCustomer()
        {
            var detailForm = new DetailCustomerView();
            detailForm.ShowDialog();
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer == null) return;
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
            MessageBox.Show("Successfully deleted Customer.");
        }

        private void getCustomers()
        {
            using (var cmd = DC.getOpenConnection().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM frmCustomerView";
                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;
                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                Customers = new ObservableCollection<CustomerModel>(dataTable.DataTableToList<CustomerModel>());
            }
        }

        private void ShowWindow()
        {
            // Just as an exammple, here I just show a MessageBox
            Debug.WriteLine(SelectedCustomer);
            var detailForm = new DetailCustomerView(SelectedCustomer);
            detailForm.ShowDialog();
        }

        #endregion
    }
}
