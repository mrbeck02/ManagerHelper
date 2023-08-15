using ManagerHelper.DAL;
using ManagerHelper.Data;
using ManagerHelper.Models;
using ManagerHelper.Resources;
using ManagerHelper.ViewModels.Support;
using System.Collections.ObjectModel;

namespace ManagerHelper.ViewModels
{
    public class TeamVelocityViewModel : ITeamVelocityViewModel
    {
        private IAlertService _alertService;
        private ISqliteDataContextFactory<DataContext> _contextFactory;

        public ObservableCollection<DeveloperSprintSummaryGroup> Groups { get; private set; } = new ObservableCollection<DeveloperSprintSummaryGroup>();

        public TeamVelocityViewModel(ISqliteDataContextFactory<DataContext> contextFactory, Guid developerId,
                                    IAlertService alertService)
        {
            _alertService = alertService;
            _contextFactory = contextFactory;
            // The following line shouldn't be necessary.   Specify the path before this point.
            //_contextFactory.DbPath = Preferences.Default.Get(PreferenceKey.db_location.ToString(), "");

            createDeveloperSprintSummaryGroups(new UnitOfWork(_contextFactory.CreateDbContext()), developerId);
        }

        private void createDeveloperSprintSummaryGroups(UnitOfWork unitOfWork, Guid developerId)
        {
            // Note: Assuming this is Mashini for now.
            // For this, groups are set up by quarters.  So I'll get all the entries for Mashini in a quarter so that I can print them out

            var developer = unitOfWork.DeveloperRepository.GetByID(developerId);

            if (developer == null)
                throw new Exception("Developer not found");

            // The sprints don't contain a list of commitments.  So I can't pull them that way.  I can pull all commitments for the developer.

            // Get all commitments
            var commitments = unitOfWork.CommitmentRepository.Get(c => c.DeveloperId == developer.Id, null, "JiraIssue,Sprint,Sprint.Quarter");

            // Group commitments into sprint items for display
            var sprintGroups = commitments.GroupBy(c => c.SprintId);

            // loop throgh each sprint group and create a sprint summary
            var sprintSummaries = new List<DeveloperSprintSummary>();

            foreach (var sprintCommitmentList in sprintGroups)
            {
                sprintSummaries.Add(new DeveloperSprintSummary(sprintCommitmentList.ToList()));
            }

            // Group sprint summaries by 

            // Add the sprint summaries to the appropriate quarters
            // We have a bunch of sprint summaries that need to be grouped by quarters and sorted by date.  Quarters from latest to oldest


            //foreach (var sprintSummary in sprintSummaries)
            //{
            //    if (Groups.Any(g => string.Compare(g.Name, sprintSummary.Quarter))
            //}
            //Groups.Add(new DeveloperSprintSummaryGroup())

        }
    }
}
