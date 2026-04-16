namespace TalentInsights.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> Create(T entity);
        public IQueryable<T> Queryable();
        public Task<T> Update(T entity);
        public Task<bool> Delete(T entity);
    }
}
