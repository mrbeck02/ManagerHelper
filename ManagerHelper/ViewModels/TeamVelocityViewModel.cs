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

            // Group the commitments by sprint and place them into sprint summary items
            var sprintSummaries = commitments.GroupBy(c => c.SprintId).Select(c => new DeveloperSprintSummary(c.ToList()));

            // Group the sprints by quarter and place them into quarter summaries.

            // We'll also have to sort them.

        }
    }
}
