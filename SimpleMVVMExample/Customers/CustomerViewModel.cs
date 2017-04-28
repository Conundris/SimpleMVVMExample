using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Utility;
using SimpleMVVMExample.WindowFactory;
using PrintDialog = System.Windows.Controls.PrintDialog;

namespace SimpleMVVMExample.Customers
{
    public class CustomerViewModel : ValidatableViewModelBase, IPageViewModel
    {
        #region Fields

        private int _customerId;
        private readonly IWindowFactory _windowFactory;
        private ObservableCollection<CustomerModel> _customers = new ObservableCollection<CustomerModel>();
        private CustomerModel _selectedCustomer;
        private ICommand _openDetailCustomerCommand;

        #endregion

        public CustomerViewModel() : this(new DetailCustomerViewProductionFactory()) { }

        public CustomerViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            Customers = new ObservableCollection<CustomerModel>();
            CreateCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(CreateCustomer);
            SearchCustomersCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(GetCustomers);
            Get100Customers();
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
                           param => CreateDetailWindow(),
                           param => (SelectedCustomer != null)
                       ));
            }
        }

        public ICommand CreateCustomerCommand { get; set; }

        public ICommand SearchCustomersCommand { get; set; }

        #endregion

        #region Methods

        // Open new DetailCustomerView with an empty CustomerModel
        private async void CreateCustomer()
        {
            // Create and wait for Window to close.
            var complete = await _windowFactory.CreateNewWindow(new CustomerModel());
            // Refresh Datagrid with new Data
            GetCustomers();
        }

        // Get ALL Customers
        private void GetCustomers()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPCUSTOMERVIEW";
                cmd.Parameters.Add(new OracleParameter("STRSEARCHSTRING", OracleDbType.Varchar2, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("CURSOR_", OracleDbType.RefCursor, ParameterDirection.Output));

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;

                var dataTable = new DataTable();
                dataTable.Load(dr);

                Customers = new ObservableCollection<CustomerModel>(dataTable.DataTableToList<CustomerModel>());
            }
        }

        // Load newest 100 Customers, Gets called at Startup
        private void Get100Customers()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM frmCustomerView WHERE ROWNUM <= 100 ORDER BY INTCUSTOMERID DESC";
                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;
                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                Customers = new ObservableCollection<CustomerModel>(dataTable.DataTableToList<CustomerModel>());
            }
        }
        // Creates new DetailCustomerView with selected Customer in the Listview
        private async void CreateDetailWindow()
        {
            // Create and wait for Window to close.
            var complete = await _windowFactory.CreateNewWindow(_selectedCustomer);
            // Refresh Datagrid with new Data
            GetCustomers();
        }

        #endregion
    }
}
