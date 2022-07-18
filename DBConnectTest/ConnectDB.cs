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
                DataSource = "SAWASICKWIN10", // 接続するサーバー名
                IntegratedSecurity = true, // Windows認証ならtrueにする
                InitialCatalog = "Test" // 接続するDB名
                //UserID = "(ユーザー名)",
                //Password = "(パスワード)"
            };

            return builder.ToString();
        }

        private static SqlConnection CreateConnection(string connection) => new SqlConnection(connection);

        private static string CreateCommandText() => @"SELECT Id, Number, Name FROM MainTable"; // stringBuilderで作って、stringで返すも良し

        public static DataTable Connect()
        {
            var connectionString = GetConnectionString();

            using (var connection = CreateConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの実行
                    command.CommandText = CreateCommandText();
                    //command.ExecuteNonQuery();


                    // SqlDataAdaperを使用(DataTableに取得データを格納)
                    var table = new DataTable();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                    //foreach (DataRow target in table.Rows)
                    //{
                    //var id = target["Id"].ToString();
                    //var number = target["Number"].ToString();
                    //var name = target["Name"].ToString();
                    //Console.WriteLine("DO");
                    //Console.WriteLine(target["Id"]);
                    //Console.WriteLine(target["Number"]);
                    //Console.WriteLine(target["Name"]);
                    //}
                    return table;
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
