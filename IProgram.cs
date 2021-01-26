using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSFeedAgeCheck
{
    public interface IProgram
    {
        void Run();
        List<string> GetStaleCompanies(Dictionary<string, List<string>> companyFeedDictionary, int daysUntilStale);
        bool IsCompanyStale(string company, List<string> feeds, int daysUntilStale);
        bool IsFeedStale(string feed, int daysUntilStale);
        Feed ReadFeed(string feed);
    }
}
