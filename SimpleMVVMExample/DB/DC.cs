using System.CodeDom;
using System.Configuration;
using System.Data.Common;
using System.Windows.Forms.VisualStyles;
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
                throw new CustomException(ex);
            }
            return connection;
        }
    }
}
