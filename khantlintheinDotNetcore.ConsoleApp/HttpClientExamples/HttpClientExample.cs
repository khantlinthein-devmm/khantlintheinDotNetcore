using khantlintheinDotNetcore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace khantlintheinDotNetcore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        private string _blogEndpoint = "https://localhost:7221/api/blog";
        

        public async Task Run()
        {

            //await Read();
            //await Edit(1);
            //await Edit(1007);
            //await Create("Test title", "Test name", "Test content", "Test Category");
            //await Update(8, "Movie", "Harry", "Like You", "Series");
            //await Patch(1, "Mama", "Jame");
            //await Delete(30);

        }

        #region Read
        public async Task Read()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_ID);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Name);
                    Console.WriteLine(item.Blog_Content);
                    Console.WriteLine(item.Blog_Category);
                }
            }
        }

        #endregion

        #region Edit
        public async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
                Console.WriteLine(item.Blog_ID);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Name);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine(item.Blog_Category);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        #endregion


        #region Create

        public async Task Create(string title, string name, string content, string category)
        {

            var blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Name = name,
                Blog_Content = content,
                Blog_Category = category
            };

            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpcontent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(_blogEndpoint, httpcontent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        #endregion

        #region Update
        public async Task Update(int id, string title, string name, string content, string category)
        {
            var blog = new BlogDataModel()
            {
                Blog_ID = id,
                Blog_Title = title,
                Blog_Name = name,
                Blog_Content = content,
                Blog_Category = category
            };
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpcontent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync($"{_blogEndpoint}/{id}", httpcontent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        

        public async Task Patch(int id, string title, string name)
        {
            var blog = new BlogDataModel()
            {
                Blog_ID = id,
                Blog_Title = title,
                Blog_Name = name,
            };
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpcontent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PatchAsync($"{_blogEndpoint}/{id}", httpcontent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"{_blogEndpoint}/{id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        #endregion
    }
}
