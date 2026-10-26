using Microsoft.Data.SqlClient;
using System.Data;


string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

using(SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connection opened");

    SqlCommand command = connection.CreateCommand();

    //    command.CommandText = @"CREATE TABLE files
    //(
    //	id INT IDENTITY PRIMARY KEY,
    //	[file_name] VARCHAR(50) NOT NULL,
    //	[title] VARCHAR(50) NOT NULL,
    //	[file] VARBINARY(MAX)
    //)";

    //command.CommandText = @"INSERT INTO files VALUES (@file_name, @title, @file)";
    //command.Parameters.Add("@file_name", SqlDbType.NVarChar, 50);
    //command.Parameters.Add("@title", SqlDbType.NVarChar, 50);

    //string filePath = @"D:\ada.jpg";

    //string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
    //string title = "Ada Lavlase";

    //byte[] file;
    //using(FileStream stream = new(filePath, FileMode.Open))
    //{
    //    file = new byte[stream.Length];
    //    stream.Read(file, 0, file.Length);
    //    command.Parameters.Add("@file", SqlDbType.VarBinary, file.Length);
    //}

    //command.Parameters["@file_name"].Value = fileName;
    //command.Parameters["@title"].Value = title;
    //command.Parameters["@file"].Value = file;

    //command.ExecuteNonQuery();
    List<Image> images = new List<Image>();

    command.CommandText = @"SELECT * FROM files";
    using(SqlDataReader reader = command.ExecuteReader())
    {
        while(reader.Read())
        {
            int id = reader.GetInt32(0);
            string fileName = reader.GetString(1);
            string title = reader.GetString(2);
            byte[] file = (byte[])reader.GetValue(3);

            Image image = new Image()
            {
                Id = id,
                FileName = fileName,
                Title = title,
                File = file
            };

            images.Add(image);
        }
    }

    if (images.Count <= 0) return;
    
    Image img = images.FirstOrDefault()!;

    using(FileStream stream = new FileStream(img.FileName, FileMode.OpenOrCreate))
    {
        stream.Write(img.File);
    }
}


class Image
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string Title { get; set; }
    public byte[] File { get; set; }
}