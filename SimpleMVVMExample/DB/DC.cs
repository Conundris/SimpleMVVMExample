using System.CodeDom;
using System.Configuration;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.Exceptions;

namespace SimpleMVVMExample.DB
{
    class DC
    {
        public static DbConnection getOpenConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"];
            var providerName = connectionString.ProviderName;
            var factory = DbProviderFactories.GetFactory(providerName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString.ConnectionString;

            try
            {
                connection.Open();
            }
            catch (OracleException ex)
            {
                //12154 No Connection/ Connection couldn't be established.
                if (ex.Number == 12154)
                {
                    throw new CustomException(
                        "Connection couldn't be estahblished, please check your internet connection and try again.");
                }
            }
            return connection;
        }
    }
}
