using Dapper;
using khantlintheinDotNetcore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace khantlintheinDotNetcore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {

        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".", // server name
            InitialCatalog = "DotNetcore", // databse name
            UserID = "sa", // username
            Password = "sa@2655" // password
        };
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(14);
            //Create("Test title", "test name ", "test content", "test category");
            //Update(5,"MinMin", "James", "Mniofd", "Series");
            Delete(10);
        }

        private void Read()
        {
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog]";


            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Name);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine(item.Blog_Category);
                Console.WriteLine("-----END------");
            }
        }

        private void Edit(int id)
        {
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel {Blog_ID = id }).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Name);
            Console.WriteLine(item.Blog_Content);
            Console.WriteLine(item.Blog_Category);
            Console.WriteLine("-----END------");
        }

        private void Create(string title, string name, string content, string category)
        {
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

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Name = name,
                Blog_Content = content,
                Blog_Category = category,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);


            string message = result > 0 ? "Saving successful" : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string name, string content, string category)
        {
            string query = @"UPDATE [dbo].[Tb_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Name] = @Blog_Name
      ,[Blog_Content] = @Blog_Content
      ,[Blog_Category] = @Blog_Category
 WHERE Blog_ID = @Blog_ID";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_ID = id,
                Blog_Title = title,
                Blog_Name = name,
                Blog_Content = content,
                Blog_Category = category,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);


            string message = result > 0 ? "Updaing successful" : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id )
        {
            string query = @"DELETE FROM [dbo].[Tb_Blog]
      WHERE Blog_ID = @Blog_ID";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_ID = id,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);


            string message = result > 0 ? "Deleting successful" : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
