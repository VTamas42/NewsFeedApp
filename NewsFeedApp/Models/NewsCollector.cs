using System.Net;
using System.Text;
using System.Xml.Linq;

namespace NewsFeedApp.Models
{
    /// <summary>
    /// Collecting news from rss channels and add to database
    /// </summary>
    public class NewsCollector
    {
        public int refreshInterval;
        private string bbc = "https://feeds.bbci.co.uk/news/world/rss.xml";
        private string nyt = "https://rss.nytimes.com/services/xml/rss/nyt/World.xml";
        private string myteu = "https://rss.nytimes.com/services/xml/rss/nyt/Europe.xml";
        private string skynews = "https://feeds.skynews.com/feeds/rss/world.xml";

        public void GetLatestNews()
        {
            List<string> newsSourceList = new List<string>();
            newsSourceList.Add(bbc);
            newsSourceList.Add(nyt);
            newsSourceList.Add(myteu);
            newsSourceList.Add(skynews);

            foreach (var source in newsSourceList)
            {
                try
                {
                    WebClient wc = new WebClient();
                    XDocument xDoc = new XDocument();
                    byte[] xmlsource = wc.DownloadData(source);
                    string encodedSource = Encoding.ASCII.GetString(xmlsource);
                    xDoc = XDocument.Parse(encodedSource);
                    IEnumerable<XElement> xElementList = xDoc.Descendants("item");

                    foreach (XElement node in xElementList)
                    {
                        News news = new News();
                        news.Title = @"" + node.Element("title")?.Value.ToString();
                        news.Description = @"" + node.Element("description")?.Value.ToString();
                        news.ArcticleLink = node.Element("link")?.Value;
                        news.GUID = node.Element("guid")?.Value;
                        news.PubDate = DateTime.Parse(node.Element("pubDate").Value);

                        if (node.Elements().FirstOrDefault(e => e.Name.LocalName == "thumbnail") != null)
                        {
                            news.MediaContent = node.Elements()?.FirstOrDefault(e => e.Name.LocalName == "thumbnail")?.Attribute("url")?.Value.ToString();
                        }
                        else if (node.Elements().FirstOrDefault(e => e.Name.LocalName == "content") != null)
                        {
                            news.MediaContent = node.Elements().FirstOrDefault(e => e.Name.LocalName == "content")?.Attribute("url")?.Value.ToString();
                        }
                        else
                        {
                            news.MediaContent = xDoc.Descendants()?
                                .Where(element => element.Name.LocalName == "url" && element.Parent != null && element.Parent.Name.LocalName == "image")?
                                .FirstOrDefault()?.Value.ToString();
                        }

                        using (var context = new NewsDBContext())
                        {
                            if (context.News.Any(x => x.GUID == news.GUID)) { continue; }
                            context.News.Add(news);
                            context.SaveChanges();
                        }
                    }

                }
                catch (System.Xml.XmlException e)
                {
                    Console.WriteLine("Error occurred while saving the content: " + e.Message);
                }
            }
        }
    }
}
