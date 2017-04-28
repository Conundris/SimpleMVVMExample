using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Helper_Classes;
using SimpleMVVMExample.Staff;
using SimpleMVVMExample.Utility;

// ReSharper disable ExplicitCallerInfoArgument

namespace SimpleMVVMExample.Ticket
{
    class DetailTicketViewModel : ValidatableViewModelBase
    {
        #region fields

        private TicketModel _selectedTicket;
        private ObservableCollection<CustomerModel> _customers = new ObservableCollection<CustomerModel>();
        private ObservableCollection<StaffModel> _staff = new ObservableCollection<StaffModel>();

        #endregion


        public DetailTicketViewModel(TicketModel selectedTicket)
        {
            _selectedTicket = selectedTicket;
            SaveAndCloseTicketCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(SaveAndCloseTicket);
            CloseTicketCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(CloseTicket);
            getActiveCustomers();
            getActiveStaff();
        }

        #region Properties

        public ICommand SaveAndCloseTicketCommand { get; set; }
        public ICommand CloseTicketCommand { get; set; }
        public ICommand AddPaymentCommand { get; set; }
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

        public ObservableCollection<StaffModel> Staff
        {
            get { return _staff ?? (_staff = new ObservableCollection<StaffModel>()); }
            set
            {
                if (value != null)
                {
                    _staff = value;
                    OnPropertyChanged("Staff");
                }
            }
        }

        private async void SaveAndCloseTicket(ICloseable window)
        {
            await _selectedTicket.ValidateAsync();

            if (_selectedTicket.IsValid != null && _selectedTicket.IsValid.Value)
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
            else
            {
                MessageBox.Show(_selectedTicket.ValidationErrorsString);
            }
        }

        #endregion

        #region Methods

        private void UpdateCustomer()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                _selectedTicket.STRSTATUS = _selectedTicket.INTASSIGNEDTO != 0 ? "In Progress" : "Not Assigned";

                cmd.CommandText =
                    "UPDATE tblTicket " +
                    "SET strTitle = :strTitle, " +
                    "strDescription = :strDescription, " +
                    "intAssignedTo = :intAssignedTo, " +
                    "intRequestBy = :intRequestBy, " +
                    "strStatus = :strStatus " +
                    "WHERE intTicketID = :intTicketID";

                cmd.Parameters.Add(new OracleParameter("strTitle", _selectedTicket.STRTITLE));
                cmd.Parameters.Add(new OracleParameter("strDescription", _selectedTicket.STRDESCRIPTION));
                // If Assigned To is null, we give DBNull as a parameter
                cmd.Parameters.Add(_selectedTicket.INTASSIGNEDTO == 0
                    ? new OracleParameter("intAssignedTo", DBNull.Value)
                    : new OracleParameter("intAssignedTo", OracleDbType.Int32, (int)_selectedTicket.INTASSIGNEDTO, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("intRequestedBy", OracleDbType.Int32, _selectedTicket.INTREQUESTBY, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("strStatus", _selectedTicket.STRSTATUS));
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
                _selectedTicket.STRSTATUS = _selectedTicket.INTASSIGNEDTO != 0 ? "In Progress" : "Not Assigned";

                cmd.CommandText =
                    "INSERT INTO tblTicket (strTitle, strDescription, intAssignedTo, intRequestBy, strStatus)" +
                    " VALUES (:strTitle, :strDescription, :intAssignedTo, :intRequestBy, :strStatus)";

                cmd.Parameters.Add(new OracleParameter("strTitle", _selectedTicket.STRTITLE));
                cmd.Parameters.Add(new OracleParameter("strDescription", _selectedTicket.STRDESCRIPTION));
                // If Assigned To is null, we give DBNull as a parameter
                cmd.Parameters.Add(_selectedTicket.INTASSIGNEDTO == 0
                    ? new OracleParameter("intAssignedTo", DBNull.Value)
                    : new OracleParameter("intAssignedTo", OracleDbType.Int32, (int)_selectedTicket.INTASSIGNEDTO, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("intRequestBy", _selectedTicket.INTREQUESTBY));
                cmd.Parameters.Add(new OracleParameter("strStatus", _selectedTicket.STRSTATUS));

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

        private void CloseTicket()
        {
            var dialogResult = MessageBox.Show("Are you sure that you want to close this ticket?", "Close Ticket", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {

                    //Validation

                    cmd.CommandText =
                        "UPDATE tblTicket " +
                        "SET " +
                        "strStatus = 'Closed' " +
                        "WHERE intTicketID = :intTicketID";

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
            else if (dialogResult == DialogResult.No)
            {
                //do something else
                //close window
            }
        }

        private void getActiveCustomers()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM TBLCUSTOMER WHERE BLNACTIVE = '1'";

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;

                var dataTable = new DataTable();
                dataTable.Load(dr);

                Customers = new ObservableCollection<CustomerModel>(dataTable.DataTableToList<CustomerModel>());
            }
        }

        private void getActiveStaff()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM TBLSTAFF WHERE BLNACTIVE = '1'";

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;

                var dataTable = new DataTable();
                dataTable.Load(dr);

                Staff = new ObservableCollection<StaffModel>(dataTable.DataTableToList<StaffModel>());
            }
        }

        #endregion
    }
}
