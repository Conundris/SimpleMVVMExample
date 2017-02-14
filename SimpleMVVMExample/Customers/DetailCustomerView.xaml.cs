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
            _customerModel = new CustomerModel();
            DataContext = _customerModel;
        }

        // For editing new Customer
        public DetailCustomerView(CustomerModel customerModel)
        {
            _customerModel = customerModel;
            InitializeComponent();
            DataContext = customerModel;
        }
        private void Insert()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
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
    }
}
