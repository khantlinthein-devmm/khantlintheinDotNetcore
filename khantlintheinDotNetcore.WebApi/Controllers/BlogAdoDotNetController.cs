using Dapper;
using khantlintheinDotNetcore.RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlConnectionStringBuilder = Microsoft.Data.SqlClient.SqlConnectionStringBuilder;
using SqlDataAdapter = Microsoft.Data.SqlClient.SqlDataAdapter;

namespace khantlintheinDotNetcore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".", // server name
            InitialCatalog = "DotNetcore", // databse name
            UserID = "sa", // username
            Password = "sa@2655", // password
            TrustServerCertificate = true,
        };

        [HttpGet]
        public IActionResult GetBLogs()
        {
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Tbl_Blog]";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            List<BlogDataModel> lst = dt.AsEnumerable().Select(dr => new BlogDataModel
            {
                Blog_ID = Convert.ToInt32(dr["Blog_ID"]),
                Blog_Title = Convert.ToString(dr["Blog_Title"])!,
                Blog_Name = Convert.ToString(dr["Blog_Name"]),
                Blog_Content = Convert.ToString(dr["Blog_Content"]),
                Blog_Category = Convert.ToString(dr["Blog_Category"])
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBLog(int id)
        {
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_ID", id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return BadRequest("No data found");
            }

            BlogDataModel blogItem = new BlogDataModel()
            {
                Blog_ID = Convert.ToInt32(dt.Rows[0]["Blog_ID"]),
                Blog_Title = Convert.ToString(dt.Rows[0]["Blog_Title"])!,
                Blog_Name = Convert.ToString(dt.Rows[0]["Blog_Name"]),
                Blog_Content = Convert.ToString(dt.Rows[0]["Blog_Content"]),
                Blog_Category = Convert.ToString(dt.Rows[0]["Blog_Category"])
            };

            return Ok(blogItem);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
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
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Name", blog.Blog_Name);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            command.Parameters.AddWithValue("@Blog_Category", blog.Blog_Category);

            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Title is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Name))
            {
                return BadRequest("Blog Name is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog Content is required");
            }
            if(string.IsNullOrEmpty(blog.Blog_Category))
            {
                return BadRequest("Blog Category is required.");
            }

            int result = command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving successful" : "Saving failed!!!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";


            BlogDataModel? item = connection.Query<BlogDataModel>(query, new BlogDataModel { Blog_ID = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No data found");
            }

            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Title is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Name))
            {
                return BadRequest("Blog Name is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog Content is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Category))
            {
                return BadRequest("Blog Category is reqired.");
            }

            string queryUpdate = @"UPDATE [dbo].[Tb_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Name] = @Blog_Name
      ,[Blog_Content] = @Blog_Content
      ,[Blog_Category] = @Blog_Category
 WHERE Blog_ID = @Blog_ID";
            SqlCommand command = new SqlCommand(queryUpdate, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Name);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            command.Parameters.AddWithValue("@Blog_Category", blog.Blog_Category);
            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            BlogDataModel? item = connection.Query<BlogDataModel>(query, new BlogDataModel { Blog_ID = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No data found");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @"[Blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Name))
            {
                conditions += @"[Blog_Name] = @Blog_Name, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += @"[Blog_Content] = @Blog_Content, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Category))
            {
                conditions += @"[Blog_Category] = @Blog_Category,";
            }
            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Request");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.Blog_ID = id;

            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog]
                                   SET {conditions}
                                 WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(queryUpdate, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Name))
            {
                command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Name);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Category))
            {
                command.Parameters.AddWithValue("@Blog_Category,", blog.Blog_Category);
            }

            int result = command.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleteblog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE Blog_ID = @Blog_ID";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_ID", id);
            int result = command.ExecuteNonQuery();
            string message = result > 0 ? "Delete successful" : "Delete Fail";
            return Ok(message);
        }

        
    }
}
