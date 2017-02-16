using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Utility;
using SimpleMVVMExample.WindowFactory;

namespace SimpleMVVMExample.Customers
{
    public class CustomerViewModel : ValidatableViewModelBase, IPageViewModel
    {
        #region Fields

        private int _customerId;
        private readonly IWindowFactory windowFactory;
        private ObservableCollection<CustomerModel> _customers = new ObservableCollection<CustomerModel>();
        private CustomerModel _selectedCustomer;
        private ICommand _printCustomersCommand;
        private ICommand _createCustomerCommand;
        private ICommand _openDetailCustomerCommand;
        private ICommand _deleteCustomerCommand;
        private ICommand _deRegisterCustomerCommand;
        private ICommand _searchCustomersCommand;

        #endregion

        public CustomerViewModel() : this(new DetailCustomerViewProductionFactory()) { }

        public CustomerViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;
            Customers = new ObservableCollection<CustomerModel>();
            CreateCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(CreateCustomer);
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

        public ICommand PrintCustomersCommand
        {
            get
            {
                return _printCustomersCommand ?? (_printCustomersCommand = new RelayCommand(
                           param => PrintCustomers()));
            }
        }

        public ICommand OpenDetailCustomerCommand
        {
            get
            {
                return _openDetailCustomerCommand ?? (_openDetailCustomerCommand = new RelayCommand(
                           param => CreateDetailWindow(),
                           param => (SelectedCustomer != null)
                       ));
            }
        }

        public ICommand CreateCustomerCommand { get; set; }

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

        private void PrintCustomers()
        {
            throw new System.NotImplementedException();
        }

        private void DeRegisterCustomer()
        {
            if (SelectedCustomer == null) return;
            SelectedCustomer.BLNACTIVE = false;
            MessageBox.Show("Customer has been successfully deregistered.");
        }

        private void CreateCustomer()
        {
            windowFactory.CreateNewWindow(new CustomerModel());
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
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;
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

        private void CreateDetailWindow()
        {
            // Create and display detail Window
            windowFactory.CreateNewWindow(_selectedCustomer);
        }

        #endregion
    }
}
