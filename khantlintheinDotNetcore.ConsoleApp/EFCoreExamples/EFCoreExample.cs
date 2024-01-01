using khantlintheinDotNetcore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khantlintheinDotNetcore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(3);
            //Create("new title", "new name ", "new content", "new category");
            //Update(6,"MinMin", "James", "Mniofd", "Series");
            Delete(15);
        }

        public void Read()
        {
           var lst =  _dbContext.Blogs.ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_ID);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Name);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine(item.Blog_Category);
                Console.WriteLine("-----END------");
            }
        }

        private void Edit(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);
            if(item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.Blog_ID);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Name);
            Console.WriteLine(item.Blog_Content);
            Console.WriteLine(item.Blog_Category);
            Console.WriteLine("-----END------");
        }

        private void Create(string title, string name, string content,string category)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Name = name,
                Blog_Content = content,
                Blog_Category = category,
            };

            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saving successful" : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string name, string content, string category)
        {

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

                item.Blog_Title = title;
                item.Blog_Name = name;
                item.Blog_Content = content;
                item.Blog_Category = category;

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updating successful" : "Updating failed ";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_ID == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
