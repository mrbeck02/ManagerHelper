using System.Linq.Expressions;

namespace ManagerHelper.DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Returns a non-null list of entries found in the repository that match the filter 
        /// criteria.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties">A comma seprated list of properties to eager load</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Returns the entity or null if not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetByID(object id);
        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
