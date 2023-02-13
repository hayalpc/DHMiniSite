using DHCore.Models;

namespace DHMiniSite.Operations.Interfaces
{
    public interface IBaseOperations<TEntity> 
    {
        Task<List<TEntity>> GetAsync();
        Task<DataList<TEntity>> GetAsync(int page = 0, int limit = 10);
        Task<TEntity?> GetAsync(string id);
        Task<TEntity> CreateAsync(TEntity newPost);
        Task UpdateAsync(string id, TEntity updatedBook);
        Task RemoveAsync(string id);
    }
}
