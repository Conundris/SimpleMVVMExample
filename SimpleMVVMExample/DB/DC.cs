using System.Configuration;
using System.Data.Common;

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
            connection.Open();
            return connection;
        }
    }
}
