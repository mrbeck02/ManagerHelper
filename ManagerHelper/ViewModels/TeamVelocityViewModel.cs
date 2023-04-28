using ManagerHelper.Data;
using ManagerHelper.ViewModels.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerHelper.ViewModels
{
    public class TeamVelocityViewModel : ITeamVelocityViewModel
    {
        private IAlertService _alertService;
        private ISqliteDataContextFactory<DataContext> _contextFactory;

        public TeamVelocityViewModel(ISqliteDataContextFactory<DataContext> contextFactory,
                                    IAlertService alertService)
        {
            _alertService = alertService;
            _contextFactory = contextFactory;
        }
    }
}
