using ManagerHelper.CsvImporter;
using ManagerHelper.DAL;
using ManagerHelper.Data.Entities;
using Moq;
using System.Linq.Expressions;

namespace ManagerHelperTests.Extensions;

public class StatisticsCsvImporterTests
{
    [Fact]
    public void ImportData_WhenValidDataIsGivenToFreshDb_CorrectDataIsInserted()
    {
        var sprintRepoMock = new Mock<IGenericRepository<Sprint>>();
        var sprint = new Sprint()
        {
            Name = "2022-AUG-2"
        };

        Sprint sprintToInsert = null;
        bool sprintGetWasCalled = false;
        // Should insert sprint if it doesn't exist
        sprintRepoMock.Setup(x => x.Get(
            It.IsAny<Expression<Func<Sprint, bool>>>(),
            It.IsAny<Func<IQueryable<Sprint>, IOrderedQueryable<Sprint>>>(),
            It.IsAny<string>())).Callback(() => sprintGetWasCalled = true);
        sprintRepoMock.Setup(x => x.Insert(It.IsAny<Sprint>())).Callback<Sprint>(s => sprintToInsert = s);

        // Should insert quarter if it doesn't exist
        Quarter quarterToInsert = null;
        var quarterRepoMock = new Mock<IGenericRepository<Quarter>>();
        quarterRepoMock.Setup(x => x.Insert(It.IsAny<Quarter>())).Callback<Quarter>(q => quarterToInsert = q);

        // Should insert Jira issue if it doesn't exist
        JiraIssue jiraIssueToInsert = null;
        var jiraIssueRepoMock = new Mock<IGenericRepository<JiraIssue>>();
        jiraIssueRepoMock.Setup(x => x.Insert(It.IsAny<JiraIssue>())).Callback<JiraIssue>(j => jiraIssueToInsert = j);

        // For now, assuming that Jira project exists
        var project = new JiraProject()
        {
            Id = Guid.Parse("51ef85c1-aef0-45f3-8cbd-291a20009924"),
            Name = "ORANGE",
            Domain = "hbuco.atlassian.net"
        };

        var jiraProjectRepoMock = new Mock<IGenericRepository<JiraProject>>();
        jiraProjectRepoMock.Setup(x => x.Get(
            It.IsAny<Expression<Func<JiraProject, bool>>>(),
            It.IsAny<Func<IQueryable<JiraProject>, IOrderedQueryable<JiraProject>>>(),
            It.IsAny<string>())).Returns(new[] { project });

        // Should be able to find product
        var product = new Product()
        {
            Id = 14,
            Name = "Create Referral API",
        };

        var productRepoMock = new Mock<IGenericRepository<Product>>();
        productRepoMock.Setup(x => x.Get(
            It.IsAny<Expression<Func<Product, bool>>>(),
            It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(),
            It.IsAny<string>())).Returns(new[] { product });

        // Should have a single commitment
        Commitment commitmentToInsert = null;
        var commitmentRepoMock = new Mock<IGenericRepository<Commitment>>();
        commitmentRepoMock.Setup(x => x.Insert(It.IsAny<Commitment>())).Callback<Commitment>(c => commitmentToInsert = c);

        // Check the last entry
        Entry entryToInsert = null;
        var entryRepoMock = new Mock<IGenericRepository<Entry>>();
        entryRepoMock.Setup(x => x.Insert(It.IsAny<Entry>())).Callback<Entry>(e => entryToInsert = e);

        var unknownIssueStatus = new IssueStatus() { Id = 8, Name = "Unknown" };

        var issueStatusRepoMock = new Mock<IGenericRepository<IssueStatus>>();
        int issueStatusRetrieved = 0;
        issueStatusRepoMock.Setup(x => x.Get(
            It.IsAny<Expression<Func<IssueStatus, bool>>>(),
            It.IsAny<Func<IQueryable<IssueStatus>, IOrderedQueryable<IssueStatus>>>(),
            It.IsAny<string>())).Returns(new[] { unknownIssueStatus }).Callback(() => issueStatusRetrieved++);

        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.SprintRepository).Returns(sprintRepoMock.Object);
        unitOfWorkMock.Setup(x => x.QuarterRepository).Returns(quarterRepoMock.Object);
        unitOfWorkMock.Setup(x => x.JiraIssueRepository).Returns(jiraIssueRepoMock.Object);
        unitOfWorkMock.Setup(x => x.JiraProjectRepository).Returns(jiraProjectRepoMock.Object);
        unitOfWorkMock.Setup(x => x.ProductRepository).Returns(productRepoMock.Object);
        unitOfWorkMock.Setup(x => x.CommitmentRepository).Returns(commitmentRepoMock.Object);
        unitOfWorkMock.Setup(x => x.EntryRepository).Returns(entryRepoMock.Object);
        unitOfWorkMock.Setup(x => x.IssueStatusRepository).Returns(issueStatusRepoMock.Object);

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

        var entry = new StatisticsCsvEntry()
        {
            Date = DateTime.Parse("8/15/2022"),
            Sprint = "2022-AUG-2",
            Quarter = "Q3 2022",
            Jira = "11569",
            Prod = "CR API",
            Include = "Yes",
            SP =8,
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
            Notes = "Anton started testing Monday Aug 22\r\nQA environment was taken up by regression in the 1st week.\r\nOnly bug that came back was at 10p yesterday and Charles found it."
        };

        importer.ImportData(new List<StatisticsCsvEntry>() { entry }, dev, unitOfWorkMock.Object);

        Assert.True(sprintGetWasCalled);
    }
}