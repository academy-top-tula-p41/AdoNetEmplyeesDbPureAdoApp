using Microsoft.Data.SqlClient;
using System.Data;


string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

using(SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connection opened");

    SqlCommand command = connection.CreateCommand();

    
}