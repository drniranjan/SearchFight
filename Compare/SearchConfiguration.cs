using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Compare
{
    public class SearchConfiguration : ISearchConfiguration
    {
        public string Url { get; set; }
        public string BaseNode { get; set; }
        public string RegexForCount { get; set; }
        public SearchConfiguration(string SearchEngine)
        {
            this.Url = GetConfig(SearchEngine, "Url");
            this.BaseNode = GetConfig(SearchEngine, "ContainerId");
            this.RegexForCount = GetConfig(SearchEngine, "RegexToSearch");
        }

        public SearchConfiguration()
        {
        }

        public string GetConfig(string searchEngine, string configKey)
        {
            string configValue = string.Empty;
            try
            {
                configValue = ConfigurationManager.AppSettings.Get(searchEngine + configKey);
            }
            catch (Exception)
            {
                //Don't want to break the process for one of the missing config. error will be logged in the calling method.
            }
            return configValue;
        }
    }
}
