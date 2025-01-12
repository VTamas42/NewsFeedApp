using Microsoft.EntityFrameworkCore;
using NewsFeedApp.Pages;
using System.Configuration;
using NewsFeedApp.Models;

namespace NewsFeedApp.Models
{
    public class NewsDBContext : DbContext
    {
        readonly IConfiguration _configuration;
        public string connectionString = "Server=(localdb)\\mssqllocaldb;Database=NewsDB;Trusted_Connection=True;";

        //public NewsDBContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<NewsFeedApp.Models.News> News { get; set; } = default!;
    }
}
