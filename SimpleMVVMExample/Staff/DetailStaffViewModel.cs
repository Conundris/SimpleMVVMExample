using System;
using System.Data;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Helper_Classes;

// ReSharper disable ExplicitCallerInfoArgument

namespace SimpleMVVMExample.Staff
{
    class DetailStaffViewModel : ValidatableViewModelBase
    {
        private StaffModel _selectedStaff;

        public DetailStaffViewModel(StaffModel selectedStaff)
        {
            _selectedStaff = selectedStaff;
            SaveAndCloseCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(SaveAndCloseCustomer);
        }

        public ICommand SaveAndCloseCustomerCommand { get; set; }
        public StaffModel SelectedStaff
        {
            get { return _selectedStaff; }
            set
            {
                if (value == _selectedStaff || value == null) return;
                _selectedStaff = value;
                OnPropertyChanged("SelectedStaff");
            }
        }

        private void SaveAndCloseCustomer(ICloseable window)
        {
            // Insert (Create new Customer)
            if (_selectedStaff.INTSTAFFID == 0)
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
                    "UPDATE TBLSTAFF " +
                    "SET STRFORENAME = :strForename, " +
                    "STRSURNAME = :strSurname, " +
                    "STRUSERNAME = :strUsername, " +
                    "STRPASSWORD = :strPassword, " +
                    "STREMAIL = :strEmail " +
                    "WHERE INTSTAFFID = :intStaffID";

                cmd.Parameters.Add(new OracleParameter("strSurname", _selectedStaff.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedStaff.STRFORENAME));
                cmd.Parameters.Add(new OracleParameter("strUsername", _selectedStaff.STRUSERNAME));
                cmd.Parameters.Add(new OracleParameter("strPassword", _selectedStaff.STRPASSWORD));
                cmd.Parameters.Add(new OracleParameter("strEmail", _selectedStaff.STREMAIL));
                cmd.Parameters.Add(new OracleParameter("intStaffID", OracleDbType.Int32, _selectedStaff.INTSTAFFID,
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
                    "INSERT INTO tblCustomer (strSurname, strForename, strUsername, strPassword, strEmail)" +
                    " VALUES (:strSurname, :strForename, :strUsername, :strPassword, :strEmail)";

                cmd.Parameters.Add(new OracleParameter("strSurname", _selectedStaff.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedStaff.STRFORENAME));
                cmd.Parameters.Add(new OracleParameter("strUsername", _selectedStaff.STRUSERNAME));
                cmd.Parameters.Add(new OracleParameter("strPassword", _selectedStaff.STRPASSWORD));
                cmd.Parameters.Add(new OracleParameter("strEmail", _selectedStaff.STREMAIL));
                cmd.Parameters.Add(new OracleParameter("intStaffID", OracleDbType.Int32, _selectedStaff.INTSTAFFID,
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
    }
}
