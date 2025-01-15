using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NewsFeedApp.Models;

namespace NewsFeedApp.Pages
{
    public class IndexModel : PageModel
    {
        public Pager Pager { get; set; }
        private readonly NewsDBContext _context = new NewsDBContext();
        private NewsCollector newsCollector = new NewsCollector();
        public List<News> news { get; set; } = new List<News>();
        public List<News> data { get; set; } = new List<News>();

        public IndexModel(NewsDBContext newsDBContext)
        {
            _context = newsDBContext;
        }

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }

        public void OnGet(int pg = 1)
        {
            newsCollector.GetLatestNews();
            int pageSize = 9;

            news = _context.News.OrderBy(e => e.PubDate).ToList();
            if (!searchString.IsNullOrEmpty())
            {

                news = news.Where(s => s.Title.Contains(searchString)).ToList();
                pageSize = news.Count;
            }

            int recsCount = news.Count;

            Pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            news = news.Skip(recSkip).Take(pageSize).ToList();
        }
    }
}
