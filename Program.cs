using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] SearchStrings)
        {
            var environment = isEnvironmentValid(SearchStrings.ToList());
            if (environment.isValid)
            {
                var result = new Compare.SearchEngines(environment.searchEngines)
                                        .SearchAll(SearchStrings.ToList())
                                        .PrintAllResults()
                                        .PrintSearchEngineWinner()
                                        .PrintOverAllWinner();
            }
            Console.ReadLine();
        }
        private static (bool isValid, List<string> searchEngines) isEnvironmentValid(List<string> searchStrings)
        {
            bool isValid = false;
            List<string> SearchEngineList = new List<string>();

            if (searchStrings.Count <= 0)
            {
                Console.WriteLine("Please provide the search strings as argument.");
                return (isValid, SearchEngineList);
            }

            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                    return (isValid, SearchEngineList);
                }

                SearchEngineList = appSettings.Get("SearchEngines").Split(',').ToList();
                isValid = true;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings.");
            }

            return (isValid, SearchEngineList);
        }

    }
}
