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
    class DetailTicketViewModel : ValidatableViewModelBase
    {
        private TicketModel _selectedTicket;

        public DetailTicketViewModel(TicketModel selectedTicket)
        {
            _selectedTicket = selectedTicket;
            SaveAndCloseCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(SaveAndCloseCustomer);
        }

        public ICommand SaveAndCloseCustomerCommand { get; set; }
        public TicketModel SelectedTicket
        {
            get { return _selectedTicket; }
            set
            {
                if (value == _selectedTicket || value == null) return;
                _selectedTicket = value;
                OnPropertyChanged("SelectedTicket");
            }
        }

        private void SaveAndCloseCustomer(ICloseable window)
        {
            // Insert (Create new Customer)
            if (_selectedTicket.INTTICKETID == 0)
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
                    "UPDATE tblTicket " +
                    "SET strTitle = :strTitle, " +
                    "strDescription = :strDescription, " +
                    "intAssignedTo = :intAssignedTo, " +
                    "intRequestBy = :intRequestBy " +
                    "WHERE intTicketID = :intTicketID";

                cmd.Parameters.Add(new OracleParameter("strTitle", _selectedTicket.STRTITLE));
                cmd.Parameters.Add(new OracleParameter("strDescription", _selectedTicket.STRTICKETDESCRIPTION));
                cmd.Parameters.Add(new OracleParameter("intAssignedTo", OracleDbType.Int32, _selectedTicket.INTASSIGNEDTO));
                cmd.Parameters.Add(new OracleParameter("intRequestedBy", OracleDbType.Int32, _selectedTicket.INTREQUESTEDBY));
                cmd.Parameters.Add(new OracleParameter("intTicketID", OracleDbType.Int32, _selectedTicket.INTTICKETID,
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
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO tblTicket (strSurname, strForename, strCompany, strPhone, DATDATEOFBIRTH, strStreet, strTown, strCountry, strCounty)" +
                    " VALUES (:strSurname, :strForename, :strCompany, :strPhone, :DATDATEOFBIRTH, :strStreet, :strTown, :strCountry, :strCounty)";

                /*cmd.Parameters.Add(new OracleParameter("strSurname", _selectedTicket.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedTicket.STRFORENAME));
                cmd.Parameters.Add(new OracleParameter("strCompany", _selectedTicket.STRCOMPANY));
                cmd.Parameters.Add(new OracleParameter("strPhone", _selectedTicket.STRPHONE));
                cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date,
                    _selectedTicket.DATDATEOFBIRTH,
                    ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("strStreet", _selectedTicket.STRSTREET));
                cmd.Parameters.Add(new OracleParameter("strTown", _selectedTicket.STRTOWN));
                cmd.Parameters.Add(new OracleParameter("strCountry", _selectedTicket.STRCOUNTRY));
                cmd.Parameters.Add(new OracleParameter("strCounty", _selectedTicket.STRCOUNTY));*/

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
