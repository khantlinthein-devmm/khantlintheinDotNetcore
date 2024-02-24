
using khantlintheinDotNetcore.MVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace khantlintheinDotNetcore.MVCApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}