using khantlintheinDotNetcore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace khantlintheinDotNetcore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
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
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Get);
            var response = await client.ExecuteAsync(request);

            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
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

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);

            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
                Console.WriteLine(item.Blog_ID);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Name);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine(item.Blog_Category);
            }
            else
            {
                Console.WriteLine(response.Content!);
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

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
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

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);

        }



        public async Task Patch(int id, string title, string name)
        {
            var blog = new BlogDataModel()
            {
                Blog_ID = id,
                Blog_Title = title,
                Blog_Name = name,
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Patch);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        #endregion
    }
}
