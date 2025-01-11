using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsFeedApp.Models;

namespace NewsFeedApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration;
        private readonly NewsDBContext _context;
        private NewsCollector newsCollector = new NewsCollector();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, NewsDBContext newsDBContext)
        {
            _logger = logger;
            _configuration = configuration;
            _context = newsDBContext;
        }

        //Process data from news table and generate news pages
        public void OnGet()
        {
            string test = _configuration.GetConnectionString("DefaultConnection");
            newsCollector.GetLatestNews();
            
        }
    }
}
