using DHCore.Models;
using DHData.Models;
using DHMiniSite.Models;
using DHMiniSite.Operations.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

namespace DHMiniSite.Operations
{
    public class CommentOperations : ICommentOperations
    {

        private readonly IMongoCollection<Comment> commentCollection;

        public CommentOperations(IOptions<DHMiniSiteDatabaseSettings> dhMiniSiteDatabaseSettings)
        {
            var mongoClient = new MongoClient(
          dhMiniSiteDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dhMiniSiteDatabaseSettings.Value.DatabaseName);

            commentCollection = mongoDatabase.GetCollection<Comment>(
                dhMiniSiteDatabaseSettings.Value.CommentsCollectionName);
        }

        public async Task<List<Comment>> GetAsync() =>
             await commentCollection.Find(x => true).SortByDescending(x => x.CreateTime).ToListAsync();

        public async Task<DataList<Comment>> GetAsync(int page = 0, int limit = 10)
        {
            var postList = new DataList<Comment>();
            postList.Data = await commentCollection.Find(x => true).Skip(page * limit).Limit(10).SortByDescending(x => x.CreateTime).ToListAsync();
            postList.CurrentPage = page;
            postList.Count = limit;
            postList.TotalCount = await commentCollection.Find(x => true).CountDocumentsAsync();

            return postList;
        }

        public async Task<Comment?> GetAsync(string id) =>
            await commentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Comment> CreateAsync(Comment newModel)
        {
            newModel.CreateTime = DateTime.Now;
            await commentCollection.InsertOneAsync(newModel);
            return newModel;
        }

        public async Task UpdateAsync(string id, Comment updatedModel) =>
            await commentCollection.ReplaceOneAsync(x => x.Id == id, updatedModel);

        public async Task RemoveAsync(string id) =>
            await commentCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<DataList<Comment>> GetByPostId(string id)
        {
            var commentList = new DataList<Comment>();
            commentList.Data = await commentCollection.Find(x => x.PostId == id && x.Approved).SortByDescending(x => x.CreateTime).ToListAsync();
            commentList.CurrentPage = 0;
            commentList.Count = commentList.Data.Count;
            commentList.TotalCount = commentList.Data.Count;

            return commentList;
        }

        public async Task<string?> Add(string id, Comment comment)
        {
            comment.PostId = id;
            comment.CreateTime = DateTime.Now;
            comment.Approved = true;
            await CreateAsync(comment);
            return comment.Id;
        }

        public async Task ChangeApprove(Comment comment)
        {
            comment.Approved = !comment.Approved;
            comment.UpdateTime = DateTime.Now;
            await UpdateAsync(comment.Id, comment);
        }
    }
}
