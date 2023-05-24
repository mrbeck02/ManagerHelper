using ManagerHelper.DAL;
using ManagerHelper.Data;
using ManagerHelper.Models;
using ManagerHelper.Resources;
using ManagerHelper.ViewModels.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerHelper.ViewModels
{
    public class TeamVelocityViewModel : ITeamVelocityViewModel
    {
        private IAlertService _alertService;
        private ISqliteDataContextFactory<DataContext> _contextFactory;

        public ObservableCollection<DeveloperSprintSummaryGroup> Groups { get; private set; } = new ObservableCollection<DeveloperSprintSummaryGroup>();

        public TeamVelocityViewModel(ISqliteDataContextFactory<DataContext> contextFactory,
                                    IAlertService alertService)
        {
            _alertService = alertService;
            _contextFactory = contextFactory;
            _contextFactory.DbPath = Preferences.Default.Get(PreferenceKey.db_location.ToString(), "");

            createDeveloperSprintSummaryGroups(new UnitOfWork(_contextFactory.CreateDbContext()));
        }

        private void createDeveloperSprintSummaryGroups(UnitOfWork unitOfWork)
        {
            // Note: Assuming this is Mashini for now.
            // For this, groups are set up by quarters.  So I'll get all the entries for Mashini in a quarter so that I can print them out

            var result = unitOfWork.DeveloperRepository.Get(d => d.LastName == "Mashini").ToList();

            if (result.Count == 0)
                throw new Exception("Mashini is missing!");

            var commitments = unitOfWork.CommitmentRepository.Get(c => c.DeveloperId == result[0].Id);

            var quarterlyCommitments = commitments.GroupBy(g => g.Sprint.QuarterId);

        }
    }
}
