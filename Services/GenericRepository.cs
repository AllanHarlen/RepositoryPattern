namespace RepositoryGeneric.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        Task<T> IGenericRepository<T>.Delete()
        {
            throw new NotImplementedException();
        }

        Task<T> IGenericRepository<T>.Insert()
        {
            throw new NotImplementedException();
        }

        Task<IList<T>> IGenericRepository<T>.Lista()
        {
            throw new NotImplementedException();
        }

        Task<T> IGenericRepository<T>.Update()
        {
            throw new NotImplementedException();
        }
    }
}
