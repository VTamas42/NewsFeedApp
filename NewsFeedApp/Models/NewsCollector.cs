using System;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;

namespace NewsFeedApp.Models
{
    /// <summary>
    /// Collecting news from rss channels and add to database
    /// </summary>
    public class NewsCollector
    {
        public int refreshInterval;
        public List<News> freshNews = new List<News>();
        private string bbc = "https://feeds.bbci.co.uk/news/world/rss.xml";
        private string euronews = "https://www.euronews.com/rss?format=mrss&level=theme&name=news";
        private string politico = "https://www.politico.com/rss/Top10Blogs.xml";
        private string theguardian = "https://www.theguardian.com/us-news/rss";
        private string skynews = "https://feeds.skynews.com/feeds/rss/world.xml";

        public void GetLatestNews()
        {
            List<string> newsSourceList = new List<string>();
            newsSourceList.Add(bbc);
            //hibás source?
            //newsSourceList.Add(euronews);
            //newsSourceList.Add(politico);
            newsSourceList.Add(theguardian);
            newsSourceList.Add(skynews);

            this.freshNews.Clear();

            foreach (var source in newsSourceList)
            {
                try
                {
                    WebClient wc = new WebClient();
                    byte[] xmlsource = wc.DownloadData(source);
                    string encodedSource = Encoding.ASCII.GetString(xmlsource);

                    XDocument XDoc = new XDocument();
                    XDoc = XDocument.Parse(encodedSource);
                    IEnumerable<XElement> xElementList = XDoc.Descendants("item");

                    foreach (XElement node in xElementList)
                    {
                        News news = new News();
                        news.Title = node.Element("title").Value;
                        news.Description = node.Element("description").Value;
                        news.ArcticleLink = node.Element("link").Value;
                        news.GUID = node.Element("guid").Value;
                        news.PubDate = DateTime.Parse(node.Element("pubDate").Value);
                        //hiányzó média content
                        //XNamespace ns = "media:";
                        //news.MediaContent = node.Descendants(ns + "thumbnail").FirstOrDefault().Value;
                        freshNews.Add(news);
                        // ", \"" + news.PubDate + "\", )";

                        using (var context = new NewsDBContext())
                        {
                            context.News.Add(news);
                            context.SaveChanges();
                        }
                    }
                }
                catch (System.Xml.XmlException e)
                {
                    Console.WriteLine();
                    throw;
                }
            }

            Console.WriteLine();

        }
    }

    /*
     https://feeds.bbci.co.uk/news/world/rss.xml bbc
https://www.euronews.com/rss?format=mrss&level=theme&name=news euronews
https://www.theguardian.com/us-news/rss theguardian
https://www.politico.com/rss/Top10Blogs.xml politico
https://feeds.skynews.com/feeds/rss/world.xml skynews

     */
}
