using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

using(SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connection opened");

    SqlCommand command = connection.CreateCommand();

    
    Console.Write("Input city: ");
    string city = Console.ReadLine();

    command.CommandText = $@"INSERT INTO cities VALUES (@city)";

    SqlParameter cityParemeter = new SqlParameter("@city", city);
    command.Parameters.Add(cityParemeter);
    
    command.ExecuteNonQuery();
}