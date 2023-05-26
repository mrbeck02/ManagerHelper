using ManagerHelper.DAL;
using ManagerHelper.Data.Entities;
using ManagerHelper.ViewModels;
using ManagerHelper.ViewModels.Support;
using ManagerHelperTests.Resources;
using Moq;

namespace ManagerHelperTests.ViewModels
{
    public class TeamVelocityViewModelTests
    {
        private readonly TestSqliteDataContextFactory _factory;
        private readonly Guid _mashinisId;
        private readonly Guid _jiraProjectId;

        public TeamVelocityViewModelTests()
        {
            _factory = new TestSqliteDataContextFactory();
            _mashinisId = Guid.Parse("5DC1E1D0-003F-4F03-B69E-C92D919CA81B");
            _jiraProjectId = Guid.Parse("C977217C-46FA-4380-AFDC-96E8084F5AD0");

            var uow = new UnitOfWork(_factory.CreateDbContext());
            var developer = new Developer()
            {
                Id = _mashinisId,
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
                Id = _jiraProjectId,
                Name = "ORANGE",
                Domain = "hbuco.atlassian.net"
            };
            uow.JiraProjectRepository.Insert(jiraProject);
            uow.Save();

            // Add more than 1 quarter
            var quarter1 = new Quarter()
            {
                Id = Guid.Parse("FEDAF1E4-8891-4ED0-9315-BC23304B9BC0"),
                Name = "Q3 2022",
                QuarterNumber = 3,
                Year = 2022
            };

            var quarter2 = new Quarter()
            {
                Id = Guid.Parse("6EBF37AF-FE25-4E57-8C7F-2B7CF25A98BA"),
                Name = "Q4 2022",
                QuarterNumber = 4,
                Year = 2022
            };

            uow.QuarterRepository.Insert(quarter1);
            uow.QuarterRepository.Insert(quarter2);
            uow.Save();

            var sprint1 = new Sprint()
            {
                Id = Guid.Parse("0869B2F0-B7FE-4F2C-8EC4-E36933CC9954"),
                Name = "2022-AUG-2",
                StartDate = DateTime.Parse("2022-08-15 00:00:00"),
                EndDate = DateTime.Parse("2022-08-29 00:00:00"),
                QuarterId = Guid.Parse("FEDAF1E4-8891-4ED0-9315-BC23304B9BC0")
            };

            var sprint2 = new Sprint()
            {
                Id = Guid.Parse("B3D097E6-79A8-4668-A281-5BC2091A4923"), 
                Name = "2022-AUG-3",
                StartDate = DateTime.Parse("2022-08-26 00:00:00"),
                EndDate = DateTime.Parse("2022-09-09 00:00:00"),
                QuarterId = Guid.Parse("FEDAF1E4-8891-4ED0-9315-BC23304B9BC0")
            };

            var sprint3 = new Sprint()
            {
                Id = Guid.Parse("872DA25A-23A9-4B89-A95B-0A7EAEEF0E6D"),	
                Name = "2022-SEPT-1",
                StartDate = DateTime.Parse("2022-09-09 00:00:00"),
                EndDate = DateTime.Parse("2022-09-23 00:00:00"),
                QuarterId = Guid.Parse("6EBF37AF-FE25-4E57-8C7F-2B7CF25A98BA")
            };

            var sprint4 = new Sprint()
            {
                Id = Guid.Parse("C805A8FA-6D22-42DF-91BB-64C8AFE69035"),	
                Name = "2022-SEPT-2",
                StartDate = DateTime.Parse("2022-09-23 00:00:00"),
                EndDate = DateTime.Parse("2022-10-07 00:00:00"),
                QuarterId = Guid.Parse("6EBF37AF-FE25-4E57-8C7F-2B7CF25A98BA")
            };

            uow.SprintRepository.Insert(sprint1);
            uow.SprintRepository.Insert(sprint2);
            uow.SprintRepository.Insert(sprint3);
            uow.SprintRepository.Insert(sprint4);
            uow.Save();

            // Add Jira issues, then commitments because of FK constraints
            var jiraIssue1 = new JiraIssue()
            {
                Id = Guid.Parse("5541EE44-52C8-48C9-A28F-F649E854ABA5"),
                Number = "11703",
                StoryPoints = 2,
                IsRegressionBug = false,
                DateCreatedUtc = DateTime.Parse("2023-05-25 14:20:33.9415534"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.9415535"),
                TimeZone = "Eastern Standard Time",
                JiraProjectId = _jiraProjectId
            };
            var jiraIssue2 = new JiraIssue()
            {
                Id = Guid.Parse("98A0D079-2922-4F71-945C-02209BB19C67"),
                Number = "11569",
                StoryPoints = 8,
                IsRegressionBug = false,
                DateCreatedUtc = DateTime.Parse("2023 -05-25 14:20:33.8089288"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.8089289"),
                TimeZone = "Eastern Standard Time",
                JiraProjectId = _jiraProjectId
            };
            var jiraIssue3 = new JiraIssue()
            {
                Id = Guid.Parse("B17DCE57-F6F8-4FE0-AD2F-C98C4D01E1D7"),
                Number = "11960",
                StoryPoints = 3,
                IsRegressionBug = false,
                DateCreatedUtc = DateTime.Parse("2023 -05-25 14:20:33.8682268"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.8682269"),
                TimeZone = "Eastern Standard Time",
                JiraProjectId = _jiraProjectId
            };
            var jiraIssue4 = new JiraIssue()
            {
                Id = Guid.Parse("CDD37CF8-D36D-4774-8344-5804B9A40049"),
                Number = "12048",
                StoryPoints = 5,
                IsRegressionBug = false,
                DateCreatedUtc = DateTime.Parse("2023 -05-25 14:20:33.9188383"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.9188385"),
                TimeZone = "Eastern Standard Time",
                JiraProjectId = _jiraProjectId
            };

            uow.JiraIssueRepository.Insert(jiraIssue1);
            uow.JiraIssueRepository.Insert(jiraIssue2);
            uow.JiraIssueRepository.Insert(jiraIssue3);
            uow.JiraIssueRepository.Insert(jiraIssue4);
            uow.Save();

            // Add a few commitments / Jira issues to each
            var commitment1 = new Commitment() { 
                Id = Guid.Parse("4203EDC2-EABF-4D72-A9ED-7FE7381A4659"),
                DidComplete = true,
                IncludeInData = true,
                WasInitiallyCommitted = true,
                Notes = "Anton started testing Monday Aug 22... QA environment was taken up by regression in the 1st week.Only bug that came back was at 10p yesterday and Charles found it.",
                DateCreatedUtc = DateTime.Parse("2023-05-25 14:20:33.810352"),	
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.8103521"),
                TimeZone = "Eastern Standard Time",
                SprintId = Guid.Parse("0869B2F0-B7FE-4F2C-8EC4-E36933CC9954"),	
                JiraIssueId = Guid.Parse("98A0D079-2922-4F71-945C-02209BB19C67"),
                DeveloperId = _mashinisId
            };

            var commitment2 = new Commitment()
            {
                Id = Guid.Parse("97791BA8-7CA2-4B6A-8C8D-F913CE70AB0E"),
                DidComplete = true,
                IncludeInData = true,
                WasInitiallyCommitted = true,
                DateCreatedUtc = DateTime.Parse("2023 -05-25 14:20:33.9203918"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.9203918"),
                TimeZone = "Eastern Standard Time",
                SprintId = Guid.Parse("872DA25A-23A9-4B89-A95B-0A7EAEEF0E6D"),
                JiraIssueId = Guid.Parse("CDD37CF8-D36D-4774-8344-5804B9A40049"),
                DeveloperId = _mashinisId
            };

            var commitment3 = new Commitment()
            {
                Id = Guid.Parse("41B533B5-71CD-4BCE-A498-016DB4B97155"),
                DidComplete = true,
                IncludeInData = false,
                WasInitiallyCommitted = true,
                DateCreatedUtc = DateTime.Parse("2023 -05-25 14:20:33.8720033"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.8720033"),
                TimeZone = "Eastern Standard Time",
                SprintId = Guid.Parse("B3D097E6-79A8-4668-A281-5BC2091A4923"),
                JiraIssueId = Guid.Parse("B17DCE57-F6F8-4FE0-AD2F-C98C4D01E1D7"),
                DeveloperId = _mashinisId
            };

            var commitment4 = new Commitment()
            {
                Id = Guid.Parse("DCC346F4-BB7E-4A3A-8F3C-C0F5D9FC0B63"),
                DidComplete = true,
                IncludeInData = true,
                WasInitiallyCommitted = true,
                Notes = "He finished everything he was assigned, but I'm not counting this sprint since he was out on paternity leave and couldn't take more.  It would hurt his numbers unfairly",
                DateCreatedUtc = DateTime.Parse("2023-05-25 14:20:33.9432575"),
                DateModifiedUtc = DateTime.Parse("2023-05-25 14:20:33.9432576"),
                TimeZone = "Eastern Standard Time",
                SprintId = Guid.Parse("C805A8FA-6D22-42DF-91BB-64C8AFE69035"),
                JiraIssueId = Guid.Parse("5541EE44-52C8-48C9-A28F-F649E854ABA5"),
                DeveloperId = _mashinisId
            };
            uow.CommitmentRepository.Insert(commitment1);
            uow.CommitmentRepository.Insert(commitment2);
            uow.CommitmentRepository.Insert(commitment3);
            uow.CommitmentRepository.Insert(commitment4);
        }

        [Fact]
        public void TeamVelocityViewModel_WhenInitialized_CreatesGroupOfQuarters()
        {
            var alertServiceMock = new Mock<IAlertService>();

            var blah = new TeamVelocityViewModel(_factory, alertServiceMock.Object);
        }
    }
}
