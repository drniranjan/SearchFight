using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchFight.Compare
{
    public class SearchEngines : ISearch
    {
        public List<(string SearchString, string SearchEngine, double SearchCount)> SearchResults { get; set; }
        public List<string> SearchEngineList { get; set; }
        public List<string> SearchStrings { get; set; }
        public SearchEngines(List<string> SearchEngines)
        {
            this.SearchEngineList = SearchEngines;
            SearchResults = new List<(string SearchString, string SearchEngine, double SearchCount)>();
        }

        public SearchEngines()
        {
        }

        public ISearch SearchAll(List<string> SearchStrings)
        {
            (string SearchString, string SearchEngine, double SearchCount) SearchInfo;
            foreach (var searchEngine in SearchEngineList)
            {
                SearchConfiguration config = new SearchConfiguration(searchEngine);
                if(string.IsNullOrEmpty(config.Url) || string.IsNullOrEmpty(config.BaseNode) || string.IsNullOrEmpty(config.RegexForCount))
                {
                    Console.WriteLine($"Config not found for {searchEngine}");
                    continue;
                }
                foreach (var searchString in SearchStrings)
                {
                    double count = GetSearchCount(searchString, config);
                    SearchInfo = (SearchString: searchString, SearchEngine: searchEngine, SearchCount: count);
                    SearchResults.Add(SearchInfo);
                }
            }
            return this;
        }
        public double GetSearchCount(string searchString, SearchConfiguration config)
        {
            //HttpClient doesn't generate the csrf token for Google search and so Google didn't return the count.
            // So, the below code didn't work well for Google.
            //using (HttpClient client = new HttpClient())
            //{
            //        var result = await client.GetStringAsync(url);
            //}
            double total = 0;
            try
            {
                var doc = new HtmlWeb().Load(config.Url + searchString);
                var div = doc.DocumentNode.SelectSingleNode(config.BaseNode);
                var text = div.InnerText;
                var matches = Regex.Matches(text, config.RegexForCount);
                total = Convert.ToDouble(matches[0].Groups[1].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading the search results for {config.Url+searchString}. Stack: {ex.StackTrace}");
            }
            
            return total;
        }

        public ISearch PrintAllResults()
        {
            if (SearchResults != null)
            {
                var result = SearchResults.ToLookup(p => p.SearchString);
                foreach (var res in result)
                {
                    Console.Write($"{ res.Key} : ");
                    foreach (var item in result[res.Key])
                    {
                        Console.Write($"{item.SearchEngine} : {item.SearchCount} ");
                    }
                    Console.WriteLine();
                }
            }
            return this;
        }

        public ISearch PrintOverAllWinner()
        {
            if (SearchResults != null)
            {
                Console.WriteLine($"Overall Winner: {SearchResults.OrderByDescending(a => a.SearchCount).First().SearchString}");
            }
            return this;
        }

        public ISearch PrintSearchEngineWinner()
        {
            if (SearchResults != null)
            {
                var result = SearchResults.ToLookup(p => p.SearchEngine);
                foreach (var res in result)
                {

                    Console.WriteLine($"{res.Key} winner: {res.OrderByDescending(a => a.SearchCount).First().SearchString}");
                }
            }
            return this;
        }


    }
}
