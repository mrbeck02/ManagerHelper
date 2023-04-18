using System;
using ManagerHelper.Data;
using ManagerHelper.Data.Entities;

namespace ManagerHelper.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext _dataContext;
        private GenericRepository<Commitment> _commitmentRepository;
        private GenericRepository<Data.Entities.Entry> _entryRepository;
        private GenericRepository<Developer> _developerRepository;
        private GenericRepository<Quarter> _quarterRepository;
        private GenericRepository<Sprint> _sprintRepository;
        private GenericRepository<JiraIssue> _jiraIssueRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<JiraProject> _jiraProjectRepository;
        public GenericRepository<IssueStatus> _issueStatusRepository;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IGenericRepository<Commitment> CommitmentRepository { get => _commitmentRepository ??= new GenericRepository<Commitment>(_dataContext); }
        public IGenericRepository<Data.Entities.Entry> EntryRepository { get => _entryRepository ??= new GenericRepository<Data.Entities.Entry>(_dataContext); }

        public IGenericRepository<Developer> DeveloperRepository { get => _developerRepository ??= new GenericRepository<Developer>(_dataContext); }

        public IGenericRepository<Quarter> QuarterRepository { get => _quarterRepository ??= new GenericRepository<Quarter>(_dataContext); }

        public IGenericRepository<Sprint> SprintRepository { get => _sprintRepository ??= new GenericRepository<Sprint>(_dataContext); }

        public IGenericRepository<JiraIssue> JiraIssueRepository { get => _jiraIssueRepository ??= new GenericRepository<JiraIssue>(_dataContext); }

        public IGenericRepository<Product> ProductRepository { get => _productRepository ??= new GenericRepository<Product>(_dataContext); }

        public IGenericRepository<JiraProject> JiraProjectRepository { get => _jiraProjectRepository ??= new GenericRepository<JiraProject>(_dataContext); }

        public IGenericRepository<IssueStatus> IssueStatusRepository { get => _issueStatusRepository ??= new GenericRepository<IssueStatus>(_dataContext); }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}