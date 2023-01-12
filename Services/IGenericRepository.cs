namespace RepositoryGeneric.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> Lista();
        Task<T> Insert();
        Task<T> Update();
        Task<T> Delete();
    }
}
