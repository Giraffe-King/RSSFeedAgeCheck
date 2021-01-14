using System;
using System.Collections.Generic;
using System.Linq;
using CodeHollow.FeedReader;

namespace RSSFeedAgeCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var feed = FeedReader.ReadAsync("https://feeds.megaphone.fm/ADL9840290619").Result;
            var mostRecent = feed.Items.Max(x => x.PublishingDate)?.Date;
            var today = DateTime.Now.Date;
            var dateDiff = today - mostRecent;


            return;
        }

        /// <summary>
        /// 
        /// </summary>
        static List<string> GetStaleCompanies(Dictionary<string, List<string>> companyFeedDictionary, int daysUntilStale)
        {

            return null;
        }
    }
}
