using Microsoft.EntityFrameworkCore;

namespace ManagerHelper.Data
{
    public interface ISqliteDataContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext
    {
        public string DbPath { get; set; }
    }
}
