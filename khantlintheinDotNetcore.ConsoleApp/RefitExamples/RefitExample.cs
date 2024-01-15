using khantlintheinDotNetcore.ConsoleApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khantlintheinDotNetcore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7221");
        public async Task Run()
        {
           // await Read();
            //await Edit(9);
            //await Edit(7);
            await Create("new title", "new name ", "new content", "new category");
            await Update(6, "MinMin", "James", "Mniofd", "Series");
        }

        public async Task Read()
        {
            var lst = await _blogApi.GetBlogs();
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


        public async Task Edit(int id)
        {
            try
            {
                var item = await _blogApi.GetBlog(id);
                Console.WriteLine(item.Blog_ID);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Name);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine(item.Blog_Category);
                Console.WriteLine("-----END------");
            }
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.ToString());
            //}
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.ReasonPhrase!.ToString());
                Console.WriteLine(ex.Content!.ToString());
            }
            
            
        }

        public async Task Create(string title, string name, string content, string category)
        {
            var message = await _blogApi.CreateBlog(new BlogDataModel
            {
                Blog_Title = title,
                Blog_Name = name,
                Blog_Content = content,
                Blog_Category = category    
            });
            await Console.Out.WriteLineAsync(message);

        }

        public async Task Update(int id, string title, string name, string content, string category)
        {
            try
            {
                var message = await _blogApi.UpdateBlog(id, new BlogDataModel
                {
                    Blog_ID = id,
                    Blog_Title = title,
                    Blog_Name = name,
                    Blog_Content = content,
                    Blog_Category = category
                });
                Console.WriteLine(message);
            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.Content!.ToString());
                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }
            
        }

        public async Task Delete(int id)
        {
            try
            {
                var message = await _blogApi.DeleteBlog(id);
                Console.WriteLine(message);
            }
            catch (Refit.ApiException ex)
            {

                Console.WriteLine(ex.ReasonPhrase?.ToString());
            }
            
        }
}
