using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

using(SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connection opened");

    SqlCommand command = connection.CreateCommand();
    //command.CommandText = @"CREATE TABLE cities
    //                        (
    //                         id INT IDENTITY,
    //                         title VARCHAR(50) NOT NULL,
    //                         CONSTRAINT PK_cities PRIMARY KEY (id),
    //                         CONSTRAINT UQ_cities_title UNIQUE (title)
    //                        )";

    //command.CommandText = @"INSERT INTO cities
    //                        VALUES
    //                        ('Moscow'),
    //                        ('St. Petersburg'),
    //                        ('Kazan'),
    //                        ('Tula'),
    //                        ('Novosobirsk')";

    //command.CommandText = @"UPDATE cities
    //                     SET title = 'Kaluga'
    //                     WHERE id = 6";

    //command.CommandText = @"DELETE FROM cities
	   //                     WHERE id = 6";

    int result = command.ExecuteNonQuery();
    Console.WriteLine($"Result: {result}");
}