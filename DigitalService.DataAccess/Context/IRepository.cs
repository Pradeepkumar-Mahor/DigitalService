using System.Linq.Expressions;

namespace DigitalService.DataAccess
{
    internal interface IRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> ListAsync();

        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task EditAsync(T entity);
    }

    public abstract class EntityBase
    {
        public int Id { get; protected set; }
    }
}