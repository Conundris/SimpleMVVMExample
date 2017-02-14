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

            if (CanConnect(connection))
            {
                connection.Open();
                return connection;
            }
            else
            {
                MessageBox.Show("Connection to Database, couldn't be established.");
                return connection;
            }
        }

        private static DbConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"];
            var providerName = connectionString.ProviderName;
            var factory = DbProviderFactories.GetFactory(providerName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString.ConnectionString;
            return connection;
        }

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
            }
            finally
            {
                conn.Close();
            }

            return false;
        }
    }
}
