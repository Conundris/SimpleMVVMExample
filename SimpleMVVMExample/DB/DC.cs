using System.Configuration;
using System.Data.Common;
using System.Windows;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.Exceptions;

namespace SimpleMVVMExample.DB
{
    class DC
    {
        public static DbConnection GetOpenConnection()
        {
            var connection = GetConnection();

            //Check Connection
            if (CanConnect(connection))
            {
                connection.Open();
                return connection;
            }
            else
            {
                MessageBox.Show("Connection to Database, couldn't be established.");
                // Close Application since no Connection can be made
                Application.Current.Shutdown();
                return connection;
            }
        }

        // Get Connection Object
        private static DbConnection GetConnection()
        {
            // Get ConnectionString from app.config
            var connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"];
            var providerName = connectionString.ProviderName;
            var factory = DbProviderFactories.GetFactory(providerName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString.ConnectionString;
            return connection;
        }

        // Check if Application can Connect to the Database
        public static bool CanConnect(DbConnection conn)
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (OracleException ex)
            {
                new CustomException(ex);
                return false;
            }
            finally
            {
                // Close Connection
                conn.Close();
            }
        }
    }
}
