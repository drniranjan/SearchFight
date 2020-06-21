using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Compare
{
    public interface ISearch
    {
        List<(string SearchString, string SearchEngine, double SearchCount)> SearchResults { get; set; }
        List<string> SearchEngineList { get; set; }
        List<string> SearchStrings { get; set; }

        double GetSearchCount(string searchString, SearchConfiguration config);
        ISearch SearchAll(List<string> SearchStrings);
        ISearch PrintAllResults();
        ISearch PrintSearchEngineWinner();
        ISearch PrintOverAllWinner();
    }
}
