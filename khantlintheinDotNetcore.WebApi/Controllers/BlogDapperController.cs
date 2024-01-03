using Dapper;
using khantlintheinDotNetcore.RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace khantlintheinDotNetcore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {

        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".", // server name
            InitialCatalog = "DotNetcore", // databse name
            UserID = "sa", // username
            Password = "sa@2655" // password
        };

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog]";


            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query)
                .ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id )
        {
            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_ID = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBLog(BlogDataModel blog)
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

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving successful" : "Saving Failed.";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            #region Get By Id

            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";

            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_ID = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("NO data is not found");
            }

            #endregion

            #region Check required Fields


            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog title is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Name))
            {
                return BadRequest("Blog author is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog content is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Category))
            {
                return BadRequest("Blog category is required.");
            }

            #endregion 

            string queryUpdate = @"UPDATE [dbo].[Tb_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Name] = @Blog_Name
      ,[Blog_Content] = @Blog_Content
      ,[Blog_Category] = @Blog_Category
 WHERE Blog_ID = @Blog_ID";

            int result = db.Execute(queryUpdate, blog);
            string message = result > 0 ? "Updaing successful" : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            #region Get By Id

            string query = @"SELECT [Blog_ID]
      ,[Blog_Title]
      ,[Blog_Name]
      ,[Blog_Content]
      ,[Blog_Category]
  FROM [dbo].[Tb_Blog] where Blog_ID = @Blog_ID";

            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_ID = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("NO data is not found");
            }

            #endregion

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @"[Blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Name))
            {
                conditions += @"[Blog_Author] = @Blog_Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += @"[Blog_Content] = @Blog_Content, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Category))
            {
                conditions += @"[Blog_Category] = @Blog_Category";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid request!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string queryUpdate = $@"UPDATE [dbo].[Tb_Blog]
   SET {conditions}
 WHERE Blog_ID = @Blog_ID";

            int result = db.Execute(queryUpdate, blog);
            string message = result > 0 ? "Updaing successful" : "Updating Failed.";
            return Ok(message);
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteBLog(int id)
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
            return Ok(message);
        }
    }
}
