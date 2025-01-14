using Microsoft.EntityFrameworkCore;

namespace NewsFeedApp.Models
{
    public class NewsDBContext : DbContext
    {
        public string connectionString = "Server=(localdb)\\mssqllocaldb;Database=NewsDB;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<NewsFeedApp.Models.News> News { get; set; } = default!;
    }
}
