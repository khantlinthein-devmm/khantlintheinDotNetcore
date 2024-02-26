using khantlintheinDotNetcore.MVCApp.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace khantlintheinDotNetcore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.Blogs.OrderByDescending(x => x.Blog_ID).ToListAsync();
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Save(BlogDataModel requestModel)
        {
           await _appDbContext.Blogs.AddAsync(requestModel);
           await _appDbContext.SaveChangesAsync();
           return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_ID == id);
            if (item is null)
                return RedirectToAction("Index");

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel requestModel)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_ID == id);
            if (item is null)

                return RedirectToAction("Index");

            item.Blog_Title = requestModel.Blog_Title;
            item.Blog_Name = requestModel.Blog_Name;
            item.Blog_Content = requestModel.Blog_Content;
            item.Blog_Category = requestModel.Blog_Category;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task <IActionResult> Delete(int id)
        {
            var item = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_ID == id);
            if (item is null)
            
                return RedirectToAction("Index");

                _appDbContext.Blogs.Remove(item);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            
        }
    }
}
