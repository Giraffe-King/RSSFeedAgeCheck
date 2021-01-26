using System;
using System.Collections.Generic;
using System.Linq;
using CodeHollow.FeedReader;

namespace RSSFeedAgeCheck
{
    public class Program : IProgram
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            Dictionary<string, List<string>> testDictionary = new Dictionary<string, List<string>>();

            testDictionary.Add("company 1", new List<string>
            {
                "https://feeds.megaphone.fm/ADL9840290619",
                "https://feeds.npr.org/510312/podcast.xml"
            });
            testDictionary.Add("company 2", new List<string>
            {
                "https://feeds.megaphone.fm/unlocking-us"
            });
            testDictionary.Add("stale company", new List<string>
            {
                "http://joeroganexp.joerogan.libsynpro.com/rss"
            });

            var staleCompanies = GetStaleCompanies(testDictionary, 1);
            foreach (var staleCompany in staleCompanies)
            {
                Console.WriteLine($"{staleCompany} was stale.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> GetStaleCompanies(Dictionary<string, List<string>> companyFeedDictionary, int daysUntilStale)
        {
            return companyFeedDictionary
                    .Select(x => x.Key)
                    .Where(x => IsCompanyStale(x, companyFeedDictionary[x], daysUntilStale))
                    .ToList();
        }

        public bool IsCompanyStale(string company, List<string> feeds, int daysUntilStale)
        {
            return feeds.All(x => IsFeedStale(x, daysUntilStale));
        }

        public bool IsFeedStale(string feed, int daysUntilStale)
        {
            var rss = ReadFeed(feed);
            if (rss.Items.Count <= 0)
            {
                return true;
            }
            var mostRecent = rss.Items.Max(x => x.PublishingDate).Value.Date;
            var today = DateTime.Now.Date;
            var dateDiff = today - mostRecent;

            return dateDiff.Days > daysUntilStale;
        }

        public virtual Feed ReadFeed(string feed)
        {
            return FeedReader.ReadAsync(feed).Result;
        }
    }
}
