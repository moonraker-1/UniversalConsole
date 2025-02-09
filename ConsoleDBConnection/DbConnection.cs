using System;
using Microsoft.Data.SqlClient;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
namespace ConsoleDBConnection
{
    internal class DbConnectionLocal
    {
        private string? connectionString = Environment.GetEnvironmentVariable("connectionStringConsole");

        public SqlConnection Connect()
        {
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error establishing database connection: {ex.Message}");
                throw;
            }
        }
    }
    internal static class DbConnection
    {
        public static async System.Threading.Tasks.Task Connect()
        {

        }
    }

    /// <summary>
    /// Testing connection
    /// </summary>
    public static class TestConnection
    {

        public static void TestLocal()
        {
            DbConnectionLocal lc = new DbConnectionLocal();

            try
            {
                using (var connection = lc.Connect())
                {
                    string query = "INSERT INTO tblTestConnection DEFAULT VALUES;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteReader();
                        Console.WriteLine("Successful connection");
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());   
            }

        }

        public static void TestReal()
        {
            DbConnection.Connect();
        }
    }
}
