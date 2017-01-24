using System;
using Oracle.ManagedDataAccess.Client;

namespace SimpleMVVMExample.DB
{
    class DC
    {
        public void test()
        {
            const string conString = "User Id=jbr; password=Crocket20; Data Source=helpdesksys.cqzld7hmi4qv.eu-west-1.rds.amazonaws.com:1521/orcl; Pooling=false;";

            var con = new OracleConnection
            {
                ConnectionString = conString
            };

            con.Open();

            Console.WriteLine("State: {0}", con.State);
            Console.WriteLine("ConnectionString: {0}", con.ConnectionString);

            var cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM tblCustomer";

            var DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                Console.WriteLine("ID: " + DR.GetInt32(0) + " Name: " + DR.GetString(1));
            } 
        }
    }
}
