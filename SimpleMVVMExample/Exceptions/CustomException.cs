using System.Windows;
using Oracle.ManagedDataAccess.Client;

namespace SimpleMVVMExample.Exceptions
{
    class CustomException
    {
        public CustomException() { }

        public CustomException(OracleException ex)
        {
            switch (ex.Number)
            {
                case 1:
                    MessageBox.Show("Error attempting to insert duplicate data.");
                    break;
                case 12560:
                    MessageBox.Show("The database is unavailable.");
                    break;
                default:
                    MessageBox.Show("Database error: " + ex.Message);
                    break;
            }
        }
    }
}
