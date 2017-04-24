using System; 
using System.Data;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Helper_Classes;
// ReSharper disable ExplicitCallerInfoArgument

namespace SimpleMVVMExample.Customers
{
    class DetailCustomerViewModel : ValidatableViewModelBase
    {
        private CustomerModel _selectedCustomer;

        public DetailCustomerViewModel(CustomerModel selectedCustomer)
        {
            _selectedCustomer = selectedCustomer;
            SaveAndCloseCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(SaveAndCloseCustomer);
            
        }

        public ICommand SaveAndCloseCustomerCommand { get; set; }
        public CustomerModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (value == _selectedCustomer || value == null) return;
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        private void SaveAndCloseCustomer(ICloseable window)
        {
            // Insert (Create new Customer)
            if (_selectedCustomer.INTCUSTOMERID == 0)
            {
                CreateCustomer();
            }
            else // Update Customer
            {
                UpdateCustomer();
            }
            window?.Close();
        }

        private void UpdateCustomer()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                cmd.CommandText =
                    "UPDATE tblCustomer " +
                    "SET strSurname = :strSurname, " +
                    "strForename = :strForename, " +
                    "strCompany = :strCompany, " +
                    "strPhone = :strPhone, " +
                    "DATDATEOFBIRTH = :DATDATEOFBIRTH, " +
                    "strStreet = :strStreet, " +
                    "strTown = :strTown, " +
                    "strCountry = :strCountry, " +
                    "strCounty = :strCounty " +
                    "WHERE intCustomerID = :intCustomerID";

                cmd.Parameters.Add(new OracleParameter("strSurname", _selectedCustomer.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedCustomer.STRFORENAME));
                cmd.Parameters.Add(new OracleParameter("strCompany", _selectedCustomer.STRCOMPANY));
                cmd.Parameters.Add(new OracleParameter("strPhone", _selectedCustomer.STRPHONE));
                cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date, _selectedCustomer.DATDATEOFBIRTH,
                    ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("strStreet", _selectedCustomer.STRSTREET));
                cmd.Parameters.Add(new OracleParameter("strTown", _selectedCustomer.STRTOWN));
                cmd.Parameters.Add(new OracleParameter("strCountry", _selectedCustomer.STRCOUNTRY));
                cmd.Parameters.Add(new OracleParameter("strCounty", _selectedCustomer.STRCOUNTY));
                cmd.Parameters.Add(new OracleParameter("intCustomerID", OracleDbType.Int32, _selectedCustomer.INTCUSTOMERID,
                    ParameterDirection.Input));

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        private void CreateCustomer()
        {
            Console.Out.WriteLine(_selectedCustomer.IsValid);

            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO tblCustomer (strSurname, strForename, strCompany, strPhone, DATDATEOFBIRTH, strStreet, strTown, strCountry, strCounty)" +
                    " VALUES (:strSurname, :strForename, :strCompany, :strPhone, :DATDATEOFBIRTH, :strStreet, :strTown, :strCountry, :strCounty)";

                cmd.Parameters.Add(new OracleParameter("strSurname", _selectedCustomer.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedCustomer.STRFORENAME));
                cmd.Parameters.Add(new OracleParameter("strCompany", _selectedCustomer.STRCOMPANY));
                cmd.Parameters.Add(new OracleParameter("strPhone", _selectedCustomer.STRPHONE));
                cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date,
                    _selectedCustomer.DATDATEOFBIRTH,
                    ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("strStreet", _selectedCustomer.STRSTREET));
                cmd.Parameters.Add(new OracleParameter("strTown", _selectedCustomer.STRTOWN));
                cmd.Parameters.Add(new OracleParameter("strCountry", _selectedCustomer.STRCOUNTRY));
                cmd.Parameters.Add(new OracleParameter("strCounty", _selectedCustomer.STRCOUNTY));

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
