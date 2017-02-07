using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;

namespace SimpleMVVMExample.Customers
{
    /// <summary>
    /// Interaction logic for DetailTicketView.xaml
    /// </summary>
    public partial class DetailCustomerView : Window
    {
        private CustomerModel _customerModel;

        // For creating new Customer
        public DetailCustomerView()
        {
            InitializeComponent();
            DataContext = new CustomerModel();
        }

        // For editing new Customer
        public DetailCustomerView(CustomerModel customerModel)
        {
            _customerModel = customerModel;
            InitializeComponent();
            DataContext = customerModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_customerModel.INTCUSTOMERID == 0)
            {
                Insert();
            }
            else
            {
                Update();
            }

            MessageBox.Show(_customerModel != null
                ? "Entry has successfully been created."
                : "Entry has Successfully been updated.");
            Close();
        }

        private void Update()
        {
            throw new NotImplementedException();

            using (var cmd = DC.getOpenConnection().CreateCommand())
            {

            }
        }

        private void Insert()
        {
            using (var cmd = DC.getOpenConnection().CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO tblCustomer (strSurname, strForename, strCompany, strPhone, DATDATEOFBIRTH, strStreet, strTown, strCountry, strCounty)" +
                    " VALUES (:strSurname, :strForename, :strCompany, :strPhone, :DATDATEOFBIRTH, :strStreet, :strTown, :strCountry, :strCounty)";

                cmd.Parameters.Add(new OracleParameter("strSurname", txtSurname.Text));
                cmd.Parameters.Add(new OracleParameter("strForename", txtSurname.Text));
                cmd.Parameters.Add(new OracleParameter("strCompany", txtCompany.Text));
                cmd.Parameters.Add(new OracleParameter("strPhone", txtPhone.Text));
                cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date, dpDOB.DisplayDate,
                    ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("strStreet", txtStreet.Text));
                cmd.Parameters.Add(new OracleParameter("strTown", txtTown.Text));
                cmd.Parameters.Add(new OracleParameter("strCountry", txtCountry.Text));
                cmd.Parameters.Add(new OracleParameter("strCounty", txtCounty.Text));

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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
