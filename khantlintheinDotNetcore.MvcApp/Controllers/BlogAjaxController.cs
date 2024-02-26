
using khantlintheinDotNetcore.MVCApp;
using khantlintheinDotNetcore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemonDotNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        public readonly AppDbContext _appDbContext;

        public BlogAjaxController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.Blogs
                .OrderByDescending(x => x.Blog_ID)
                .ToListAsync();
            return View(lst);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel requestModel)
        {
            await _appDbContext.Blogs.AddAsync(requestModel);
            int result = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Saving Successful" : "Saving failed." });
        }
        public async Task<IActionResult> EditAsync(int id)
        {

            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_ID == id);
            if (item is null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Update(int id, BlogDataModel requestModel)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_ID == id);
            if (item is null)
            {
                return Json(new { Message = "No data found." });
            }

            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Name = requestModel.Blog_Name;
            item.Blog_Content = requestModel.Blog_Content;
            item.Blog_Category = requestModel.Blog_Category;

            int result = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Updating Successful." : "Updating Failed." });
        }
        public async Task<IActionResult> DeleteAsync(BlogDataModel requestModel)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_ID == requestModel.Blog_ID);
            if (item is null)
            {
                return Json(new { Message = "No data found." });
            }
            _appDbContext.Remove(item);
            int result = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Deleting Successful." : "Deleting Failed." });
        }
    }
}
