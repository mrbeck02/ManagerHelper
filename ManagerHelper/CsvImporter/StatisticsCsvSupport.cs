using System;

namespace ManagerHelper.CsvImporter
{
    public class StatisticsCsvSupport
    {
        public DateTime Date { get; set; }
        public string Quarter { get; set; }
        public string Sprint { get; set; }
        public int Count { get; set; }

        public string JiraIssues { get; set; }
    }
}
