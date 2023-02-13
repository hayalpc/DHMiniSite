using DHCore.Models;
using DHData.Models;

namespace DHMiniSite.Operations.Interfaces
{
    public interface ICommentOperations : IBaseOperations<Comment>
    {
        Task<DataList<Comment>> GetByPostId(string id);
        Task<string?> Add(string id, Comment comment);
        Task ChangeApprove(Comment comment);
    }
}
