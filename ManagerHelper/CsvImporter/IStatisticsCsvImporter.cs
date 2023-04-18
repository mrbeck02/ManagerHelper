using System.Collections.Generic;
using ManagerHelper.DAL;
using ManagerHelper.Data.Entities;

namespace ManagerHelper.CsvImporter
{
    public interface IStatisticsCsvImporter
    {
        void ImportData(List<StatisticsCsvEntry> csvEntries, Developer developer, IUnitOfWork unitOfWork);
    }
}
