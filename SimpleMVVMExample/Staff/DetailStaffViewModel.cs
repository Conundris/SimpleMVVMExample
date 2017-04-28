using System;
using System.Data;
using System.Windows.Forms;
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
            SaveAndCloseStaffCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(SaveAndCloseCustomer);
            DeactivateStaffCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<ICloseable>(DeactivateStaff);

            if (_selectedStaff.INTSTAFFID != 0)
            {
                _selectedStaff.Validate();
            }
        }

        public ICommand SaveAndCloseStaffCommand { get; set; }
        public ICommand DeactivateStaffCommand { get; set; }
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

        private async void SaveAndCloseCustomer(ICloseable window)
        {
            await _selectedStaff.ValidateAsync();

            if (_selectedStaff.IsValid != null && _selectedStaff.IsValid.Value)
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
            else
            {
                MessageBox.Show(_selectedStaff.ValidationErrorsString);
            }
        }

        private void UpdateCustomer()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                cmd.CommandText =
                    "UPDATE TBLSTAFF " +
                    "SET STRFORENAME = :strForename, " +
                    "STRSURNAME = :strSurname, " +
                    "STREMAIL = :strEmail " +
                    "WHERE INTSTAFFID = :intStaffID";

                cmd.Parameters.Add(new OracleParameter("strSurname", _selectedStaff.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedStaff.STRFORENAME));
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
                    "INSERT INTO tblCustomer (strSurname, strForename, strEmail)" +
                    " VALUES (:strSurname, :strForename, :strEmail)";

                cmd.Parameters.Add(new OracleParameter("strSurname", _selectedStaff.STRSURNAME));
                cmd.Parameters.Add(new OracleParameter("strForename", _selectedStaff.STRFORENAME));
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
        private void DeactivateStaff(ICloseable window)
        {
            var dialogResult = MessageBox.Show("Are you sure that you want to deactivate this Staff?", "Deactivate Staff", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {
                    if (cmd.Connection.State != ConnectionState.Open) return;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE TBLSTAFF BLNACTIVE = '0' WHERE INTSTAFFID = " + _selectedStaff.INTSTAFFID;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Staff has been successfully deactivated.");
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
