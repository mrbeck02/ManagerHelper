using CsvHelper;
using System.Globalization;

namespace ManagerHelper.CsvImporter
{
    public class StatisticsCsvReader : IStatisticsCsvReader
    {
        public List<StatisticsCsvEntry> ReadStatistics(string csvPath)
        {
            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // this reads all the records into memory.  This isn't a big deal here because the files are small.
                return csv.GetRecords<StatisticsCsvEntry>().ToList();
            }
        }
    }
}
