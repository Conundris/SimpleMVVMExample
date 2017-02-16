using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Utility;
using SimpleMVVMExample.WindowFactory;

namespace SimpleMVVMExample.Customers
{
    public class CustomerViewModel : ValidatableViewModelBase, IPageViewModel
    {
        #region Fields

        private int _customerId;
        private readonly IWindowFactory _windowFactory;
        private ObservableCollection<CustomerModel> _customers = new ObservableCollection<CustomerModel>();
        private CustomerModel _selectedCustomer;

        #endregion

        public CustomerViewModel() : this(new DetailCustomerViewProductionFactory()) { }

        public CustomerViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            Customers = new ObservableCollection<CustomerModel>();
            CreateCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(CreateCustomer);
            PrintCustomersCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(PrintCustomers);
            SearchCustomersCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(GetCustomers);
            OpenDetailCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(CreateDetailWindow);
            DeRegisterCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(DeRegisterCustomer);
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

        public ICommand PrintCustomersCommand { get; set; }

        public ICommand OpenDetailCustomerCommand { get; set; }

        public ICommand CreateCustomerCommand { get; set; }

        public ICommand DeRegisterCustomerCommand { get; set; }

        public ICommand SearchCustomersCommand { get; set; }

        #endregion

        #region Methods

        private void PrintCustomers()
        {
            throw new System.NotImplementedException();
        }

        private void DeRegisterCustomer()
        {
            if (SelectedCustomer == null) return;

            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                SelectedCustomer.BLNACTIVE = false;

                cmd.CommandText = "UPDATE tblCustomer " +
                                  "SET blnActive = '0' " +
                                  "WHERE intCustomerID = :intCustomerID";

                cmd.Parameters.Add(new OracleParameter("intCustomerID", OracleDbType.Int32, SelectedCustomer.INTCUSTOMERID, ParameterDirection.Input));

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException e)
                {
                    Console.WriteLine(e);
                    throw;
                }


                MessageBox.Show("Customer has been successfully deregistered.");
            }
        }

        private void CreateCustomer()
        {
            _windowFactory.CreateNewWindow(new CustomerModel());
        }

        private void GetCustomers()
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

        private void CreateNewCustomer()
        {
            
        }

        private void CreateDetailWindow()
        {
            // Create and display detail Window
            _windowFactory.CreateNewWindow(_selectedCustomer);
        }

        #endregion
    }
}
