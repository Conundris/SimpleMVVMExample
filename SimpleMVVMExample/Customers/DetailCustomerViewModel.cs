using System; 
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Helper_Classes;
using MessageBox = System.Windows.MessageBox;

// ReSharper disable ExplicitCallerInfoArgument

namespace SimpleMVVMExample.Customers
{
    class DetailCustomerViewModel : ValidatableViewModelBase
    {
        private CustomerModel _selectedCustomer;
        private ICommand _deRegisterCustomerCommand;

        public DetailCustomerViewModel(CustomerModel selectedCustomer)
        {
            _selectedCustomer = selectedCustomer;
            SaveAndCloseCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(SaveAndCloseCustomer);
            DeRegisterCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(DeRegisterCustomer);
        }

        public ICommand SaveAndCloseCustomerCommand { get; set; }
        public ICommand DeRegisterCustomerCommand { get; set; }
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

        private async void SaveAndCloseCustomer(ICloseable window)
        {
            await _selectedCustomer.ValidateAsync();

            if (_selectedCustomer.IsValid != null && _selectedCustomer.IsValid.Value)
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
            else
            {
                MessageBox.Show(_selectedCustomer.ValidationErrorsString);
            }
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


        private void DeRegisterCustomer(ICloseable window)
        {
            if (_selectedCustomer == null) return;

            var dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure that you want to deregister this Customer?", "Deactivate Customer", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {
                    _selectedCustomer.BLNACTIVE = '0';

                    cmd.CommandText = "UPDATE tblCustomer " +
                                      "SET blnActive = '0' " +
                                      "WHERE intCustomerID = :intCustomerID";

                    cmd.Parameters.Add(new OracleParameter("intCustomerID", OracleDbType.Int32, _selectedCustomer.INTCUSTOMERID, ParameterDirection.Input));

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    System.Windows.Forms.MessageBox.Show("Customer has been successfully deregistered.");
                    window?.Close();
                }
            }
            else
            {
                return;
            }
        }
    }
}
