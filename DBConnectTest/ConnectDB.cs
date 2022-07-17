using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DBConnectTest
{
	public static class ConnectDB
	{
        public static string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = "SAWASICKWIN10",
                IntegratedSecurity = true,
                InitialCatalog = "Test"
                //UserID = "(ユーザー名)",
                //Password = "(パスワード)"
            };

            return builder.ToString();
        }

        public static void Connect()
		{
            var table = new DataTable();
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの実行
                    command.CommandText = @"SELECT Id, Number, Name FROM MainTable";
                    //command.ExecuteNonQuery();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                    foreach(DataRow target in table.Rows)
					{
                        Console.WriteLine("DO");
                        Console.WriteLine(target["Id"]);
                        Console.WriteLine(target["Number"]);
                        Console.WriteLine(target["Name"]);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
                finally
                {
                    // データベースの接続終了
                    connection.Close();
                }
            }
        }
	}
}
