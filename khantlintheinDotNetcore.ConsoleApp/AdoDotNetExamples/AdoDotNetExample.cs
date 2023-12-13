using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection.Metadata;

namespace khantlintheinDotNetcore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private object name;

        public void Run()
        {
            //Read();
            //Create("Test title", "Test name", "Test content", "Test Category");
            //Edit(1);
            //Edit(13);
            //Update(1,"Movie", "Harry", "Like You", "Series");
            Delete(3);
        }

        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".", // server name
                InitialCatalog = "DotNetcore", // databse name
                UserID = "sa", // username
                Password = "sa@2655" // password
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            Console.WriteLine("Connection is opening...");
            connection.Open();
            Console.WriteLine("Connection is opened.");


            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog]
";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            sqlDataAdapter.Fill(datatable);


            Console.WriteLine("Connection is closing...");
            connection.Close();
            Console.WriteLine("Connection is closed.");



            foreach (DataRow dr in datatable.Rows)
            {
                Console.WriteLine($"ID => {dr["Blog_ID"]} ");
                Console.WriteLine($"Title => {dr["Blog_Title"]} ");
                Console.WriteLine($"Name => {dr["Blog_Name"]} ");
                Console.WriteLine($"Content => {dr["Blog_Content"]} ");
                Console.WriteLine($"Category => {dr["Blog_Category"]} ");
                Console.WriteLine("----------------------");
            }
        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".", // server name
                InitialCatalog = "DotNetcore", // databse name
                UserID = "sa", // username
                Password = "sa@2655" // password
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();


            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_ID", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            sqlDataAdapter.Fill(datatable);

            connection.Close();

            if (datatable.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found.");
                return;
            }


            DataRow dr = datatable.Rows[0];

            Console.WriteLine($"ID => {dr["Blog_ID"]} ");
            Console.WriteLine($"Title => {dr["Blog_Title"]} ");
            Console.WriteLine($"Name => {dr["Blog_Name"]} ");
            Console.WriteLine($"Content => {dr["Blog_Content"]} ");
            Console.WriteLine($"Category => {dr["Blog_Category"]} ");
            Console.WriteLine("----------------------");
        }

        private void Create(string title, string name, string content, string category)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".", // server name
                InitialCatalog = "DotNetcore", // databse name
                UserID = "sa", // username
                Password = "sa@2655" // password
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();


            string query = @"INSERT INTO[dbo].[Tb_Blog]
           ([Blog_Title]
           , [Blog_Name]
           , [Blog_Content]
           , [Blog_Category])
     VALUES
           (@Blog_Title,
            @Blog_Name,
            @Blog_Content,
            @Blog_Category)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Name", name);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            cmd.Parameters.AddWithValue("@Blog_Category", category);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Saving successful" : "Saving Failed.";         
            Console.WriteLine(message );
        }

        private void Update(int id, string title, string name, string content, string category)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".", // server name
                InitialCatalog = "DotNetcore", // databse name
                UserID = "sa", // username
                Password = "sa@2655" // password
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();


            string query = @"UPDATE [dbo].[Tb_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Name] = @Blog_Name
      ,[Blog_Content] = @Blog_Content
      ,[Blog_Category] = @Blog_Category
 WHERE Blog_ID = @Blog_ID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_ID", id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("Blog_Name", name);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            cmd.Parameters.AddWithValue("@Blog_Category", category);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating successful" : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".", // server name
                InitialCatalog = "DotNetcore", // databse name
                UserID = "sa", // username
                Password = "sa@2655" // password
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();


            string query = @"DELETE FROM [dbo].[Tb_Blog]
      WHERE Blog_ID = @Blog_ID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_ID", id);
            
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Deleting successful" : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
