using ManagerHelper.CsvImporter;
using ManagerHelper.DAL;
using ManagerHelper.Data;
using ManagerHelper.Data.Entities;
using ManagerHelper.Jira;
using ManagerHelper.Resources;
using ManagerHelper.ViewModels.Support;
using RestSharp;
using RestSharp.Authenticators;
using System.Windows.Input;

namespace ManagerHelper.ViewModels
{
    public class ImportFromCsvViewModel : PropertyChangedNotifier, IDisposable, IImportFromCsvViewModel
    {
        private IStatisticsCsvImporter _statisticsCsvImporter;
        private ISqliteDataContextFactory<DataContext> _contextFactory;
        private IStatisticsCsvReader _reader;
        private IAlertService _alertService;

        #region Properties

        private string _csvPath = "";

        public string CsvPath
        {
            get => _csvPath;
            set
            {
                if (string.CompareOrdinal(_csvPath, value) == 0)
                    return;

                _csvPath = value;
                Preferences.Default.Set(PreferenceKey.csv_path.ToString(), value);
                OnPropertyChanged(nameof(CsvPath));
                refreshCanExecute(ImportCsvCommand);
            }
        }

        private IList<Developer> _developerOptions = new List<Developer>();

        public IList<Developer> DeveloperOptions
        {
            get => _developerOptions;
            private set
            {
                _developerOptions = value;
                OnPropertyChanged(nameof(DeveloperOptions));
            }
        }

        private string _jiraApiToken = "";

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

        private string _jiraUserName = "";

        public string JiraUserName
        {
            get => _jiraUserName;
            set
            {
                if (string.CompareOrdinal(_jiraUserName, value) == 0)
                    return;

                _jiraUserName = value;
                Preferences.Default.Set(PreferenceKey.jira_user_name.ToString(), value);
                refreshCanExecute(PullJiraDataCommand);
                OnPropertyChanged(nameof(JiraUserName));
            }
        }


        private Developer _selectedDeveloperOption;

        public Developer SelectedDeveloperOption
        {
            get => _selectedDeveloperOption;
            set
            {
                if (_selectedDeveloperOption == value)
                    return;

                _selectedDeveloperOption = value;
                OnPropertyChanged(nameof(SelectedDeveloperOption));
            }
        }

        private IList<JiraProject> _jiraProjectOptions = new List<JiraProject>();

        public IList<JiraProject> JiraProjectOptions
        {
            get => _jiraProjectOptions;
            private set
            {
                _jiraProjectOptions = value;
                OnPropertyChanged(nameof(JiraProjectOptions));
            }
        }

        private JiraProject _selectedJiraProject;

        public JiraProject SelectedJiraProject
        {
            get => _selectedJiraProject;
            set
            {
                if (_selectedJiraProject == value)
                    return;

                _selectedJiraProject = value;
                OnPropertyChanged(nameof(SelectedJiraProject));
            }
        }

        #endregion

        #region Commands
        // MAUI Commands: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/commanding?view=net-maui-7.0

        public ICommand ImportCsvCommand { get; set; }
        public ICommand PullJiraDataCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        #endregion

        public ImportFromCsvViewModel(ISqliteDataContextFactory<DataContext> contextFactory,
            IStatisticsCsvReader reader,
            IStatisticsCsvImporter statisticsCsvImporter,
            IAlertService alertService)
        {
            _statisticsCsvImporter = statisticsCsvImporter;
            _reader = reader;
            _contextFactory = contextFactory;
            _alertService = alertService;

            initializeViewModel();
        }

        private void initializeViewModel()
        {
            _jiraUserName = Preferences.Default.Get(PreferenceKey.jira_user_name.ToString(), "");
            _csvPath = Preferences.Default.Get(PreferenceKey.csv_path.ToString(), "");

            createImportCsvCommand();
            createExitCommand();
            createPullJiraDataCommand();
        }

        //private void setupContextFactory()
        //{
        //    _contextFactory.DbPath = DbPath;
        //}

        private void setupDeveloperOptions()
        {
            DeveloperOptions = createDeveloperOptions();

            if (DeveloperOptions.Count > 0)
            {
                SelectedDeveloperOption = _developerOptions[0];
            }
        }

        private void setupJiraProjectOptions()
        {
            JiraProjectOptions = createJiraProjectOptions();

            if (JiraProjectOptions.Count > 0)
            {
                SelectedJiraProject = _jiraProjectOptions[0];
            }
        }

        private IList<JiraProject> createJiraProjectOptions()
        {
            var unitOfWork = new UnitOfWork(_contextFactory.CreateDbContext());

            var projects = unitOfWork.JiraProjectRepository.Get().ToList();
            projects.Sort((a, b) => a.Name.CompareTo(b.Name));

            return projects;
        }

        private void createImportCsvCommand()
        {
            ImportCsvCommand = new Command(
                execute: () =>
                {
                    try
                    {
                        var entries = _reader.ReadStatistics(CsvPath);

                        if (entries.Count == 0)
                        {
                            _alertService.ShowAlert("Error", $"No entries found in file {CsvPath}");
                            return;
                        }

                        _statisticsCsvImporter.ImportData(entries, SelectedDeveloperOption, new UnitOfWork(_contextFactory.CreateDbContext()));
                        _alertService.ShowAlert("Success", $"{entries.Count} entries imported.");
                    }
                    catch (Exception ex)
                    {
                        _alertService.ShowAlert("Error", ex.Message);
                    }
                    refreshCanExecute(ImportCsvCommand);
                },
                canExecute: () =>
                {
                    return File.Exists(_csvPath);
                });

        }

        private void createPullJiraDataCommand()
        {
            PullJiraDataCommand = new Command(
                execute: async () =>
                {
                    // The Jira API token process was documented here: https://support.atlassian.com/atlassian-account/docs/manage-api-tokens-for-your-atlassian-account/
                    var options = new RestClientOptions($"https://{SelectedJiraProject.Domain}");
                    options.Authenticator = new HttpBasicAuthenticator(JiraUserName, JiraApiToken);
                    var restClient = new RestClient(options);
                    var jiraService = new JiraService(restClient);
                    await jiraService.GetIssueAsync("ORANGE-14446");
                },
                canExecute: () =>
                {
                    return !string.IsNullOrEmpty(JiraUserName);
                });
        }

        private void createExitCommand()
        {
            ExitCommand = new Command(
                execute: () =>
                {
                    _alertService.ShowConfirmation("Quit", "Exit the application?", r => { if (r) Application.Current.Quit(); });
                });
        }

        protected IList<Developer> createDeveloperOptions()
        {
            var unitOfWork = new UnitOfWork(_contextFactory.CreateDbContext());

            var developers = unitOfWork.DeveloperRepository.Get().ToList();
            developers.Sort((a, b) => a.LastName.CompareTo(b.LastName));

            return developers;
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
