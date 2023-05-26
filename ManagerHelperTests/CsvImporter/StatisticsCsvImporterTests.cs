using ManagerHelper.CsvImporter;
using ManagerHelper.DAL;
using ManagerHelper.Data.Entities;
using ManagerHelperTests.Resources;

namespace ManagerHelperTests.Extensions;

public class StatisticsCsvImporterTests
{
    private readonly TestSqliteDataContextFactory _factory;

    public StatisticsCsvImporterTests() 
    {
        _factory = new TestSqliteDataContextFactory();

        var uow = new UnitOfWork(_factory.CreateDbContext());
        var developer = new Developer()
        {
            Id = Guid.Parse("5DC1E1D0-003F-4F03-B69E-C92D919CA81B"),
            FirstName = "Heritier",
            LastName = "Mashini",
            DateCreatedUtc = DateTime.Parse("04-12-2023 10:05:01"),
            DateModifiedUtc = DateTime.Parse("04-12-2023 11:35:21"),
            TimeZone = "Eastern Standard Time"
        };
        uow.DeveloperRepository.Insert(developer);

        var glen = new Developer()
        {
            Id = Guid.Parse("2CC641C7-0F3C-4DF4-B5D7-4F0FCA611DEA"),
            FirstName = "Glen",
            LastName = "Solsberry",
            DateCreatedUtc = DateTime.Parse("04-12-2023 10:05:01"),
            DateModifiedUtc = DateTime.Parse("04-12-2023 11:35:21"),
            TimeZone = "Eastern Standard Time"
        };
        uow.DeveloperRepository.Insert(glen);

        var jiraProject = new JiraProject()
        {
            Id = Guid.Parse("C977217C-46FA-4380-AFDC-96E8084F5AD0"),
            Name = "ORANGE",
            Domain = "hbuco.atlassian.net"
        };
        uow.JiraProjectRepository.Insert(jiraProject);
        uow.Save();
    }

    [Fact]
    public void ImportData_WhenValidDataIsGivenToFreshDb_CorrectDataIsInserted()
    {
        var context = _factory.CreateDbContext();
        var uow = new UnitOfWork(context);
        var importer = new StatisticsCsvImporter();
        var dev = new Developer()
        {
            Id = Guid.Parse("2CC641C7-0F3C-4DF4-B5D7-4F0FCA611DEA"),
            FirstName = "Glen",
            LastName = "Solsberry",
            DateCreatedUtc = DateTime.Parse("04-12-2023 10:05:01"),
            DateModifiedUtc = DateTime.Parse("04-12-2023 11:35:21"),
            TimeZone = "Eastern Standard Time"
        };

        var notes = "Anton started testing Monday Aug 22\r\nQA environment was taken up by regression in the 1st week.\r\nOnly bug that came back was at 10p yesterday and Charles found it.";

        var entry = new StatisticsCsvEntry()
        {
            Date = DateTime.Parse("8/15/2022"),
            Sprint = "2022-AUG-2",
            Quarter = "Q3 2022",
            Jira = "11569",
            Prod = "CR API",
            Include = "Yes",
            SP = 8,
            Day1 = "Open",
            Day2 = "Open",
            Day3 = "Open",
            Day4 = "In Progress",
            Day5 = "In Progress",
            Day6 = "Ready for Test",
            Day7 = "Ready for Test",
            Day8 = "In Test",
            Day9 = "In Test",
            Day10 = "In Test",
            Done = 8,
            Rollover = "",
            Notes = notes
        };

        importer.ImportData(new List<StatisticsCsvEntry>() { entry }, dev, uow);

        // TODO: More coverage could be added here.
        Assert.True(uow.SprintRepository.Get(s => string.Compare(s.Name, "2022-AUG-2") == 0).Count() == 1);
        Assert.True(uow.QuarterRepository.Get(q => string.Compare(q.Name, "Q3 2022") == 0 && q.QuarterNumber == 3 && q.Year == 2022).Count() == 1);
        Assert.True(uow.JiraIssueRepository.Get(i => string.Compare(i.Number, "11569") == 0 && !i.IsRegressionBug && i.StoryPoints == 8).Count() == 1);
        Assert.True(uow.CommitmentRepository.Get(c => c.DidComplete && string.Compare(c.Notes, notes) == 0).Count() == 1);
    }
}