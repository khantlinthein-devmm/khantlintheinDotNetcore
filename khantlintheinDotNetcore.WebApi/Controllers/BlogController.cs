using khantlintheinDotNetcore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace khantlintheinDotNetcore.RestApi.Controllers
{
    //https://localhost:5000/api/blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            
            var lst = _dbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlog(int pageNo, int pageSize)
        {
            // pageNo = 1 [1-10]
            // pageNo = 2 [11-20]
            // end row no = pageNo * pageSize
            // start row = end row no - pageSize;

            var lst = _dbContext.Blogs
                .Skip((pageNo - 1) * pageSize)  // 2-1 = 1* 10 = 10
                .Take(pageSize)
                .ToList();
            var rowCount = _dbContext.Blogs.Count();
            var pageCount = rowCount / pageSize; 
            if(rowCount % pageSize == 0)
            {
                pageCount++;
            }
            return Ok(new {isEndOfPage = pageCount >= pageNo, PageCount = pageCount, PageNo = pageNo, PageSize = pageSize, Data = lst});
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBLog(BlogDataModel blog)
        {
            _dbContext.Blogs.Add(blog);
            var result = _dbContext.SaveChanges();
            var message = result > 0 ? "Saving Successful" : "Saving failed";
            return Ok(message);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);

            if (item is null)
            {
                return NotFound("NO data is not found");
            }

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

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Name = blog.Blog_Name;
            item.Blog_Content = blog.Blog_Content;
            item.Blog_Category = blog.Blog_Category;

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            return Ok(message);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBLog(int id, BlogDataModel blog)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);
            if (item == null)
            {
                return NotFound("No data is not found!");
            }
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Name))
            {
                item.Blog_Name = blog.Blog_Name;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Category))
            {
                item.Blog_Category = blog.Blog_Category;
            }
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBLog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);
            if (item == null)
            {
                return BadRequest("Data is't found");
            }
            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Delete successful" : "Delete failed";
            return Ok(message);
        }


    }
}
