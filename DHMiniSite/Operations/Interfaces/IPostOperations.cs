using DHData.Models;

namespace DHMiniSite.Operations.Interfaces
{
    public interface IPostOperations : IBaseOperations<Post>
    {
        Task ChangeStatus(Post model);

    }
}
