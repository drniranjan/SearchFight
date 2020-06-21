using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Compare
{
    interface ISearchConfiguration
    {
        string Url { get; set; }
        string BaseNode { get; set; }
        string RegexForCount { get; set; }
        string GetConfig(string searchEngine, string configKey);
    }
}
