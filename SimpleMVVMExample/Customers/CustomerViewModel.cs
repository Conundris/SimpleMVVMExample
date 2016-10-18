using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SimpleMVVMExample.Customers
{
    public class CustomerViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _customerId;
        private ObservableCollection<CustomerModel> _customers = new ObservableCollection<CustomerModel>();
        private CustomerModel _selectedCustomer;
        private ICommand _populateCustomersCommand;
        private ICommand _createCustomerCommand;
        private ICommand _openDetailCustomerCommand;
        private ICommand _deleteCustomerCommand;

        #endregion

        public CustomerViewModel()
        {
            Customers = new ObservableCollection<CustomerModel>();
        }

        #region Properties/Commands

        public CustomerModel SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
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
                if (value != null)
                {
                    _customers = value;
                    OnPropertyChanged("Customers");
                }
            }
        }

        public string Name => "Customers";

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value != _customerId)
                {
                    _customerId = value;
                    OnPropertyChanged("CustomerId");
                }
            }
        }

        public ICommand PopulateCustomersCommand
        {
            get
            {
                return _populateCustomersCommand ?? (_populateCustomersCommand = new RelayCommand(
                           param => InitializeCurrentCustomers()
                       ));
            }
        }

        public ICommand OpenDetailCustomerCommand
        {
            get
            {
                if (_openDetailCustomerCommand == null)
                {
                    _openDetailCustomerCommand = new RelayCommand(
                        param => ShowWindow(),
                        param => (SelectedCustomer != null)
                        );
                }
                return _openDetailCustomerCommand;
            }
        }

        public ICommand CreateCustomerCommand
        {
            get
            {
                if (_createCustomerCommand == null)
                {
                    _createCustomerCommand = new RelayCommand(
                        param => CreateCustomer()
                    );
                }
                return _createCustomerCommand;
            }
        }

        public ICommand DeleteCustomerCommand
        {
            get
            {
                if (_deleteCustomerCommand == null)
                {
                    _deleteCustomerCommand = new RelayCommand(
                        param => DeleteCustomer(),
                        param => (SelectedCustomer != null)
                    );
                }
                return _deleteCustomerCommand;
            }
        }

        #endregion

        #region Methods

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

        private void InitializeCurrentCustomers()
        {
            for (var i = 0; i < 5; i++)
            {
                Customers.Add(InitializeCustomer(i));
            }
        }

        private CustomerModel InitializeCustomer(int i)
        {
            var theObject = new CustomerModel()
            {
                CustomerId = i,
                Forename = "Test" + i,
                Surname = "TestSurname" + i,
                DateOfBirth = DateTime.Now
            };
            return theObject;
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
