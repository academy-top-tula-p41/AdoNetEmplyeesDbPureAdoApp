using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetEmplyeesDbPureAdoApp
{
    static public class Examples
    {
        public static void CreateTablesExmles(SqlCommand command)
        {
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

            //    command.CommandText = @"CREATE TABLE positions
            //(
            //	id INT IDENTITY,
            //	title VARCHAR(50) NOT NULL,
            //	CONSTRAINT PK_positions
            //		PRIMARY KEY (id),
            //	CONSTRAINT UQ_positions_title
            //		UNIQUE (title)
            //)

            //INSERT INTO positions
            //VALUES
            //('Project Manager'),
            //('Client Manager'),
            //('Developer'),
            //('Tester'),
            //('Designer')";

            //command.CommandText = @"CREATE TABLE companies
            //        (
            //     id INT IDENTITY,
            //     [name] VARCHAR(100) NOT NULL,
            //     city_id INT NULL,
            //     CONSTRAINT PK_companies
            //      PRIMARY KEY (id),
            //     CONSTRAINT FK_companies_cities
            //      FOREIGN KEY (city_id)
            //      REFERENCES cities(id)
            //      ON DELETE SET NULL
            //    )

            //    INSERT INTO companies
            //    ([name], city_id)
            //    VALUES
            //    ('Yandex', 1),
            //    ('Ozon', 1),
            //    ('Mail Group', 1),

            //    ('PiterSoft', 2),
            //    ('Yuma', 2),
            //    ('Nevateka', 2),

            //    ('Nextica', 3),
            //    ('First Bit', 3),
            //    ('Knopik', 3),

            //    ('TechArt', 4),
            //    ('iCloude', 4),
            //    ('PVS Studio', 4),

            //    ('Axoft', 5),
            //    ('Nixys', 5)";

//            command.CommandText = @"CREATE TABLE employees
//(
//	id INT IDENTITY,
//	[name] VARCHAR(50) NOT NULL,
//	company_id INT NULL,
//	position_id INT NULL,
//	birth_date DATE NULL,
//	salary MONEY NULL DEFAULT (0)
//	CONSTRAINT PK_employees
//		PRIMARY KEY (id),
//	CONSTRAINT FK_employees_comapnies
//		FOREIGN KEY (company_id)
//		REFERENCES companies(id)
//		ON DELETE SET NULL,
//	CONSTRAINT FK_employees_positions
//		FOREIGN KEY (position_id)
//		REFERENCES positions(id)
//		ON DELETE SET NULL
//)";

            //    command.CommandText = @"INSERT INTO employees
            //([name], birth_date, company_id, position_id, salary)
            //VALUES
            //('Bobby', '2001-10-24', 1, 3, 250000),
            //('Tommy', '1997-4-17', 1, 1, 290000),
            //('Sammy', '1985-12-28', 1, 3, 210000),
            //('Jimmy', '2002-2-25', 1, 2, 180000),
            //('Poppy', '1995-7-18', 1, 4, 230000),
            //('Jonny', '2001-5-17', 1, 3, 220000),
            //('Mikky', '1996-9-4', 1, 4, 215000),
            //('Olly', '1999-11-21', 1, 5, 190000),

            //('Limmy', '1987-6-24', 2, 3, 200000),
            //('Donny', '1993-11-17', 2, 1, 185000),
            //('Willy', '1988-10-25', 2, 3, 215000),
            //('Lucas', '2001-8-25', 2, 2, 205000),
            //('Lenny', '1995-7-18', 2, 4, 220000),
            //('Jonny', '2002-6-19', 2, 4, 200000),
            //('Bobby', '1996-2-4', 2, 4, 195000),
            //('Kenny', '2000-3-21', 2, 5, 215000)";
        }
        public static void ExecuteReaderExample(SqlCommand command)
        {
            string tableName = "cities";
            command.CommandText = $@"SELECT * FROM {tableName}";

            //int result = command.ExecuteNonQuery();
            //Console.WriteLine($"Result: {result}");

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.HasRows) return;

                string columnIdName = reader.GetName(0);
                string columnTitleName = reader.GetName(1);

                Console.WriteLine($"{columnIdName}\t{columnTitleName}\n");

                //string header = "";
                //for (int i = 0; i < reader.FieldCount; i++)
                //    header += reader.GetName(i) + "\t";
                //Console.WriteLine($"{header}");

                while (reader.Read())
                {
                    //var id = reader["id"]; //reader.GetValue(0);
                    //var title = reader["title"];//reader.GetValue(1);

                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);

                    Console.WriteLine($"{id}\t{title}");

                    //string row = "";
                    //for (int i = 0; i < reader.FieldCount; i++)
                    //    row += reader.GetValue(i).ToString() + "\t";
                    //Console.WriteLine($"{row}");
                }
            }
        }
        public static void ExecuteReaderTypesValuesExample(SqlCommand command)
        {
            string tableName = "cities";
            command.CommandText = $@"SELECT e.name AS [name], 
	                               e.birth_date AS [birth date],
	                               c.name AS [company],
	                               p.title AS [position],
	                               e.salary AS [salary]
	                            FROM employees AS e
	                            JOIN companies AS c
		                            ON e.company_id = c.id
	                            JOIN positions AS p
		                            ON e.position_id = p.id";


            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.HasRows) return;

                string header = "";
                for (int i = 0; i < reader.FieldCount; i++)
                    header += reader.GetName(i) + "\t";
                Console.WriteLine($"{header}");

                while (reader.Read())
                {
                    string row = "";

                    string name = reader.GetString(0);
                    row += name + "\t";

                    DateTime birthDate = reader.GetDateTime(1);
                    row += birthDate.ToShortDateString() + "\t";

                    string company = reader.GetString(2);
                    row += company + "\t";

                    string position = reader.GetString(3);
                    row += position + "\t";
                    if (position.Length < 8) row += "\t";

                    decimal salary = reader.GetDecimal(4);
                    row += salary.ToString() + "\t";

                    Console.WriteLine(row);
                }
            }
        }
        public static void ExecuteScalarExample(SqlCommand command)
        {
            command.CommandText = @"SELECT MAX(birth_date) FROM employees";

            var result = command.ExecuteScalar();

            Console.WriteLine($"Count = {result}");
        }
        public static void ParametersExample(SqlCommand command)
        {
            Console.Write("Input city: ");
            string city = Console.ReadLine();

            command.CommandText = $@"INSERT INTO cities VALUES (@city)";

            SqlParameter cityParemeter = new SqlParameter("@city", city);
            command.Parameters.Add(cityParemeter);

            command.ExecuteNonQuery();
        }
        public static void StoredProceduresExamples(SqlCommand command)
        {
            //command.CommandText = @"CREATE PROCEDURE InsertCity
            //(
            //	@title VARCHAR(50)
            //)
            //AS
            //	INSERT INTO cities
            //	VALUES
            //	(@title)";

            //    command.CommandText = @"CREATE PROCEDURE GetCities
            //AS
            //	SELECT * FROM cities";

            //    command.ExecuteNonQuery();

            //Console.Write("Input city's title: ");
            //string cityTitle = Console.ReadLine();

            //string storedProcedure = "InsertCity";
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = storedProcedure;

            //SqlParameter parameter = new()
            //{
            //    ParameterName = "@title",
            //    Value = cityTitle,
            //};
            //command.Parameters.Add(parameter);

            //command.ExecuteNonQuery();

            command.CommandText = "GetCities";
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.HasRows) return;

                string header = $"{reader.GetName(0)}\t| {reader.GetName(1)}";
                Console.WriteLine(header);
                Console.WriteLine(new string('-', header.Length + 8));

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    Console.WriteLine($"{id}\t| {title}");
                }
            }
        }
        public static void TransactionsExample(SqlConnection connection, SqlCommand command)
        {
            SqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            int result;

            try
            {
                string cityTitle = "Orel";

                command.CommandText = $"INSERT INTO cities VALUES ('{cityTitle}')";
                result = command.ExecuteNonQuery();
                Console.WriteLine($"First result = {result}");

                cityTitle = "Ryazan";
                command.CommandText = $"INSERT INTO cities VALUES ('{cityTitle}')";
                result = command.ExecuteNonQuery();
                Console.WriteLine($"Second result = {result}");

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        public static void SelectWithJoinExample(SqlCommand command)
        {
            command.CommandText = @"SELECT e.name, 
	   e.birth_date,
	   e.salary,
	   cm.name AS company,
	   c.title AS city,
	   p.title AS position
	FROM employees AS e
	JOIN companies AS cm
		ON e.company_id = cm.id
	JOIN cities AS c
		ON cm.city_id = c.id
	JOIN positions AS p
		ON e.position_id = p.id";

            /*
             * name = 0
             * birth_date = 1
             * salary = 2
             * company = 3
             * city = 4
             * position = 5
            */

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.HasRows) return;

                string header = $"{reader.GetName(0)}\t";
                header += $"| {reader.GetName(1)}\t";
                header += $"| {reader.GetName(3)}\t";
                header += $"| {reader.GetName(4)}\t\t";
                header += $"| {reader.GetName(5)}\t\t";
                header += $"| {reader.GetName(2)}";
                Console.WriteLine(header);
                Console.WriteLine(new string('-', header.Length + 5 * 8));

                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    DateTime birthDate = reader.GetDateTime(1);
                    string company = reader.GetString(3);
                    string city = reader.GetString(4);
                    string position = reader.GetString(5);
                    decimal salary = reader.GetDecimal(2);

                    string row = $"{name}\t";
                    row += $"| {birthDate.ToShortDateString()}\t";
                    row += $"| {company}\t";
                    if (company.Length < 6) row += "\t";

                    row += $"| {city}\t";
                    row += $"| {position}\t";
                    if (position.Length < 14) row += "\t";

                    row += $"| {salary.ToString()}";
                    Console.WriteLine(row);
                }
            }
        }
    }
}
