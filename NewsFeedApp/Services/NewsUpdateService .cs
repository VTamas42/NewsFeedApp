using NewsFeedApp.Models;

public class NewsUpdateService : BackgroundService
{
    private readonly ILogger<NewsUpdateService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private NewsCollector newsCollector = new NewsCollector();

    public NewsUpdateService(ILogger<NewsUpdateService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<NewsDBContext>();
                    UpdateNews(dbContext);
                }

                _logger.LogInformation("News updated at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("NewsUpdateService is stopping.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating news.");
            }
        }
    }

    private void UpdateNews(NewsDBContext dbContext)
    {
        newsCollector.GetLatestNews();
        _logger.LogInformation("Looking for news");
    }
}
