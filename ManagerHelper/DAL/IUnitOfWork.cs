using ManagerHelper.Data.Entities;

namespace ManagerHelper.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<Commitment> CommitmentRepository { get; }
        IGenericRepository<Data.Entities.Entry> EntryRepository { get; }
        IGenericRepository<Developer> DeveloperRepository { get; }

        IGenericRepository<Quarter> QuarterRepository { get; }

        IGenericRepository<Sprint> SprintRepository { get; }

        IGenericRepository<JiraIssue> JiraIssueRepository { get; }

        IGenericRepository<Product> ProductRepository { get; }

        IGenericRepository<JiraProject> JiraProjectRepository { get; }

        IGenericRepository<IssueStatus> IssueStatusRepository { get; }

        void Save();
    }
}
