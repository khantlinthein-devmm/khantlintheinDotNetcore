using khantlintheinDotNetcore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khantlintheinDotNetcore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".", // server name
                InitialCatalog = "DotNetcore", // databse name
                UserID = "sa", // username
                Password = "sa@2655", // password
                TrustServerCertificate = true
            };

            optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
