using System; 
using System.Data;
using System.Windows.Input;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;

namespace SimpleMVVMExample.Customers
{
    class DetailCustomerViewModel
    {
        private CustomerModel selectedCustomer;
        private ICommand _saveAndCloseCustomerCommand;

        public DetailCustomerViewModel(CustomerModel selectedCustomer)
        {
            this.selectedCustomer = selectedCustomer;
        }

        public ICommand CreateCustomerCommand
        {
            get
            {
                return _saveAndCloseCustomerCommand ?? (_saveAndCloseCustomerCommand = new RelayCommand(
                           param => SaveAndCloseCustomer()
                       ));
            }
        }

        private void SaveAndCloseCustomer()
        {
            // Insert (Create new Customer)
            if (selectedCustomer.INTCUSTOMERID == 0)
            {
                using (var cmd = DC.getOpenConnection().CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO tblCustomer (strSurname, strForename, strCompany, strPhone, DATDATEOFBIRTH, strStreet, strTown, strCountry, strCounty)" +
                        " VALUES (:strSurname, :strForename, :strCompany, :strPhone, :DATDATEOFBIRTH, :strStreet, :strTown, :strCountry, :strCounty)";

                    cmd.Parameters.Add(new OracleParameter("strSurname", selectedCustomer.STRSURNAME));
                    cmd.Parameters.Add(new OracleParameter("strForename", selectedCustomer.STRFORENAME));
                    cmd.Parameters.Add(new OracleParameter("strCompany", selectedCustomer.STRCOMPANY));
                    cmd.Parameters.Add(new OracleParameter("strPhone", selectedCustomer.STRPHONE));
                    cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date,
                        selectedCustomer.DATDATEOFBIRTH,
                        ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("strStreet", selectedCustomer.STRSTREET));
                    cmd.Parameters.Add(new OracleParameter("strTown", selectedCustomer.STRTOWN));
                    cmd.Parameters.Add(new OracleParameter("strCountry", selectedCustomer.STRCOUNTRY));
                    cmd.Parameters.Add(new OracleParameter("strCounty", selectedCustomer.STRCOUNTY));

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
            else // Update Customer
            {
                using (var cmd = DC.getOpenConnection().CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE tblCustomer (strSurname, strForename, strCompany, strPhone, DATDATEOFBIRTH, strStreet, strTown, strCountry, strCounty)" +
                        " VALUES (:strSurname, :strForename, :strCompany, :strPhone, :DATDATEOFBIRTH, :strStreet, :strTown, :strCountry, :strCounty)";

                    cmd.Parameters.Add(new OracleParameter("strSurname", selectedCustomer.STRSURNAME));
                    cmd.Parameters.Add(new OracleParameter("strForename", selectedCustomer.STRFORENAME));
                    cmd.Parameters.Add(new OracleParameter("strCompany", selectedCustomer.STRCOMPANY));
                    cmd.Parameters.Add(new OracleParameter("strPhone", selectedCustomer.STRPHONE));
                    cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date,
                        selectedCustomer.DATDATEOFBIRTH,
                        ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("strStreet", selectedCustomer.STRSTREET));
                    cmd.Parameters.Add(new OracleParameter("strTown", selectedCustomer.STRTOWN));
                    cmd.Parameters.Add(new OracleParameter("strCountry", selectedCustomer.STRCOUNTRY));
                    cmd.Parameters.Add(new OracleParameter("strCounty", selectedCustomer.STRCOUNTY));

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
}
