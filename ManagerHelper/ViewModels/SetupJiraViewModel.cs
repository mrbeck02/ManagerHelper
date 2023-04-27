using ManagerHelper.DAL;
using ManagerHelper.Data;
using ManagerHelper.Jira;
using ManagerHelper.Resources;
using ManagerHelper.ViewModels.Support;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Windows.Input;

namespace ManagerHelper.ViewModels
{
    public class SetupJiraViewModel : PropertyChangedNotifier, ISetupJiraViewModel
    {
        private string _jiraApiToken = "";
        private string _jiraUserName = "";
        private string _projectName = "";
        private string _projectUrl = "";
        private IAlertService _alertService;
        private ISqliteDataContextFactory<DataContext> _contextFactory;

        #region Properties

        public string JiraApiToken
        {
            get => _jiraApiToken;
            set
            {
                if (string.CompareOrdinal(_jiraApiToken, value) == 0)
                    return;

                _jiraApiToken = value;
                OnPropertyChanged(nameof(JiraApiToken));
            }
        }

        public string JiraUserName
        {
            get => _jiraUserName;
            set
            {
                if (string.CompareOrdinal(_jiraUserName, value) == 0)
                    return;

                _jiraUserName = value;
                Preferences.Default.Set(PreferenceKey.jira_user_name.ToString(), value);
                OnPropertyChanged(nameof(JiraUserName));
            }
        }

        public string ProjectName
        {
            get => _projectName;
            set
            {
                if (string.CompareOrdinal(_projectName, value) == 0)
                    return;

                _projectName = value;
                OnPropertyChanged(nameof(ProjectName));
            }
        }
        public string ProjectUrl
        {
            get => _projectUrl;
            set
            {
                if (string.CompareOrdinal(_projectUrl, value) == 0)
                    return;

                _projectUrl = value;
                OnPropertyChanged(nameof(ProjectUrl));
            }
        }

        #endregion

        #region Commands

        public ICommand PullJiraDataCommand { get; set; }

        #endregion

        public SetupJiraViewModel(ISqliteDataContextFactory<DataContext> contextFactory, 
            IAlertService alertService)
        {
            _alertService = alertService;
            _contextFactory = contextFactory;
            initializeViewModel();
        }

        private void initializeViewModel() 
        {
            _jiraUserName = Preferences.Default.Get(PreferenceKey.jira_user_name.ToString(), "");

            // TODO: Set a variable based on whether the db loaded.  Then show the user a label
            // explaining that there was a problem loading the db and direct them to the datasource
            // setup page.
            loadJiraProject();
            createPullJiraDataCommand();
        }

        /// <summary>
        /// Note: At this point, I'm assuming that the database already exists and has a project in
        /// it.  Future iterations will create the db from scratch using migrations when I get it 
        /// working.
        /// </summary>
        private void loadJiraProject()
        {
            try
            {
                _contextFactory.DbPath = Preferences.Default.Get(PreferenceKey.db_location.ToString(), "");
                var unitOfWork = new UnitOfWork(_contextFactory.CreateDbContext());

                var projects = unitOfWork.JiraProjectRepository.Get().ToList();
                projects.Sort((a, b) => a.Name.CompareTo(b.Name));

                // Just use the 1st project for now.
                _projectUrl = $"https://{projects[0].Domain}";
                _projectName = projects[0].Name;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"********************************** Exception occurred while loading data from datastore: {e.Message}");
            }
        }

        private void createPullJiraDataCommand()
        {
            PullJiraDataCommand = new Command(
                execute: async () =>
                {
                    // The Jira API token process was documented here: https://support.atlassian.com/atlassian-account/docs/manage-api-tokens-for-your-atlassian-account/
                    var options = new RestClientOptions(ProjectUrl);
                    options.Authenticator = new HttpBasicAuthenticator(JiraUserName, JiraApiToken);
                    var restClient = new RestClient(options);
                    var jiraService = new JiraService(restClient);
                    await jiraService.GetIssueAsync("ORANGE-14446");
                },
                canExecute: () =>
                {
                    // TODO: Validate the Project URL is a valid url?
                    return !string.IsNullOrEmpty(JiraUserName) && !string.IsNullOrEmpty(ProjectUrl);
                });
        }
    }
}
