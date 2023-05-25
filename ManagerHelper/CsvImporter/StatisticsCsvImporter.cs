using ManagerHelper.DAL;
using ManagerHelper.Data.Entities;
using ManagerHelper.Extensions;

namespace ManagerHelper.CsvImporter
{
    /// <summary>
    /// Imports the data from CSV into the database
    /// </summary>
    public class StatisticsCsvImporter : IStatisticsCsvImporter
    {
        public void ImportData(List<StatisticsCsvEntry> csvEntries, Developer developer, IUnitOfWork unitOfWork)
        {
            if (csvEntries == null)
                return;

            foreach (var csvEntry in csvEntries) 
            {
                // If there isn't a Jira issue, there isn't a commitment
                if (string.IsNullOrEmpty(csvEntry.Jira))
                    continue;

                ensureSprintExists(unitOfWork, csvEntry);
                ensureJiraIssueExists(unitOfWork, csvEntry);
                ensureCommitmentExists(unitOfWork, developer, csvEntry);

                var commitment = unitOfWork.CommitmentRepository.Get(c => string.Compare(c.Sprint.Name, csvEntry.Sprint) == 0 &&
                    string.Compare(c.Sprint.Quarter.Name, csvEntry.Quarter) == 0 &&
                    string.Compare(c.JiraIssue.Number, csvEntry.Jira) == 0 &&
                    c.Developer.Id == developer.Id).First();

                // Each day is an entry for the commitment
                addEntry(unitOfWork, commitment.Id, csvEntry.Day1, 1);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day2, 2);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day3, 3);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day4, 4);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day5, 5);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day6, 6);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day7, 7);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day8, 8);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day9, 9);
                addEntry(unitOfWork, commitment.Id, csvEntry.Day10, 10);
            }
        }

        /// <summary>
        /// Note: This assumes that if the sprint exists, the quarter exists
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="csvEntry"></param>
        private void ensureSprintExists(IUnitOfWork unitOfWork, StatisticsCsvEntry csvEntry)
        {
            // If sprint doesn't exist...
            if (unitOfWork.SprintRepository.Get(s => string.Compare(s.Name, csvEntry.Sprint) == 0).Count() == 0)
            {
                if (unitOfWork.QuarterRepository.Get(q => string.Compare(q.Name, csvEntry.Quarter) == 0).Count() == 0)
                {
                    if (csvEntry.Quarter.TryParseQuarterString(out int quarterNum, out int yearNum))
                    {
                        unitOfWork.QuarterRepository.Insert(new Quarter() { Name = csvEntry.Quarter, Id = Guid.NewGuid(), Year = yearNum, QuarterNumber = quarterNum });
                        unitOfWork.Save();
                    }
                }

                var quarter = unitOfWork.QuarterRepository.Get(q => string.Compare(q.Name, csvEntry.Quarter) == 0).First();
                unitOfWork.SprintRepository.Insert(new Sprint() { Name = csvEntry.Sprint, Id = Guid.NewGuid(), QuarterId = quarter.Id, StartDate = csvEntry.Date, EndDate = csvEntry.Date.AddDays(14) });
                unitOfWork.Save();
            }
        }

        private void ensureJiraIssueExists(IUnitOfWork unitOfWork, StatisticsCsvEntry csvEntry)
        {
            if (unitOfWork.JiraIssueRepository.Get(j => string.Compare(j.Number, csvEntry.Jira) == 0).Count() > 0)
                return;

            var projects = unitOfWork.JiraProjectRepository.Get();

            var jiraIssue = new JiraIssue() {
                Id = Guid.NewGuid(),
                DateCreatedUtc = DateTime.UtcNow,
                Number = csvEntry.Jira,
                DateModifiedUtc = DateTime.UtcNow,
                TimeZone = TimeZoneInfo.Local.StandardName,
                StoryPoints = csvEntry.SP.HasValue ? csvEntry.SP.Value : 0,
                JiraProjectId = projects.First().Id
            };

            var csvProducts = csvEntry.Prod.Split(',');

            foreach (var product in csvProducts)
            {
                if (string.IsNullOrWhiteSpace(product))
                    continue;

                var prod = unitOfWork.ProductRepository.Get(p => string.Compare(p.Name, product) == 0).FirstOrDefault();

                if (prod != null)
                    jiraIssue.Products.Add(prod);
                else if (Enum.TryParse(typeof(ProductEnum), product, out var productId))
                {
                    var prod2 = unitOfWork.ProductRepository.GetByID((int)productId);

                    if (prod2 != null)
                        jiraIssue.Products.Add(prod2);
                }
            }

            unitOfWork.JiraIssueRepository.Insert(jiraIssue);
            unitOfWork.Save();
        }

        private void ensureCommitmentExists(IUnitOfWork unitOfWork, Developer developer, StatisticsCsvEntry csvEntry)
        {
            // Don't add commitment if it's already been added
            if (unitOfWork.CommitmentRepository.Get(c => string.Compare(c.Sprint.Name, csvEntry.Sprint) == 0 &&
                    string.Compare(c.Sprint.Quarter.Name, csvEntry.Quarter) == 0 &&
                    string.Compare(c.JiraIssue.Number, csvEntry.Jira) == 0 &&
                    c.Developer.Id == developer.Id).Count() > 0)
                return;

            // Each row is a commitment that was made.
            var commitment = new Commitment()
            {
                Id = Guid.NewGuid(),
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = DateTime.UtcNow,
                TimeZone = TimeZoneInfo.Local.StandardName,
                DeveloperId = developer.Id,
                SprintId = unitOfWork.SprintRepository.Get(s => string.Compare(s.Name, csvEntry.Sprint) == 0).First().Id,
                JiraIssueId = unitOfWork.JiraIssueRepository.Get(j => string.Compare(j.Number, csvEntry.Jira) == 0).First().Id,
                DidComplete = csvEntry.Done.HasValue,
                IncludeInData = string.CompareOrdinal(csvEntry.Include, "Yes") == 0,
                Notes = csvEntry.Notes,
                WasInitiallyCommitted = csvEntry.SP.HasValue && csvEntry.SP.Value > 0
            };

            unitOfWork.CommitmentRepository.Insert(commitment);
            unitOfWork.Save();
        }

        /// <summary>
        /// Note: If an entry for this commitment on this day already exists, this will not create another
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="commitmentId"></param>
        /// <param name="dayText"></param>
        /// <param name="dayOfSprint"></param>
        private void addEntry(IUnitOfWork unitOfWork, Guid commitmentId, string dayText, int dayOfSprint)
        {
            if (string.IsNullOrEmpty(dayText))
                return;

            var commitment = unitOfWork.CommitmentRepository.GetByID(commitmentId);

            var dateEntered = commitment.Sprint.StartDate.FindDateOfSprintDay(dayOfSprint);

            // For imports, we should only have one entry per day per issue
            if (unitOfWork.EntryRepository.Get(e =>
                e.DateEntered.Year == dateEntered.Year &&
                e.DateEntered.Month == dateEntered.Year &&
                e.DateEntered.Day == dateEntered.Day &&
                e.CommitmentId == commitment.Id).Count() > 0)
                return;

            var issueStatus = unitOfWork.IssueStatusRepository.Get(s => string.Compare(s.Name, dayText) == 0).FirstOrDefault();
            
            if (issueStatus == null) 
            {
                issueStatus = unitOfWork.IssueStatusRepository.Get(s => s.Name == "Unknown").First();
            }

            var entry = new Data.Entities.Entry()
            {
                CommitmentId = commitmentId,
                DateCreatedUtc = DateTime.UtcNow,
                DateModifiedUtc = DateTime.UtcNow,
                TimeZone = TimeZoneInfo.Local.StandardName,
                Id = Guid.NewGuid(),
                IsHoliday = string.CompareOrdinal(dayText, "Holiday") == 0,
                IsPto = string.CompareOrdinal(dayText, "PTO") == 0,
                DateEntered = dateEntered,
                IssueStatusId = issueStatus.Id
            };

            unitOfWork.EntryRepository.Insert(entry);
            unitOfWork.Save();
        }
    }
}
