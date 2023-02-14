using DHCore.Models;
using DHData.Models;
using DHMiniSite.Models;
using DHMiniSite.Operations.Interfaces;
using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Consts;
using DHRabbitMQCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DHMiniSite.Operations
{
    public class PostOperations : IPostOperations
    {
        private readonly IMongoCollection<Post> postCollection;
        private readonly IPublisherService publisherService;

        public PostOperations(IOptions<DHMiniSiteDatabaseSettings> dhMiniSiteDatabaseSettings, IPublisherService publisherService)
        {
            var mongoClient = new MongoClient(
          dhMiniSiteDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dhMiniSiteDatabaseSettings.Value.DatabaseName);

            postCollection = mongoDatabase.GetCollection<Post>(
                dhMiniSiteDatabaseSettings.Value.PostsCollectionName);

            this.publisherService = publisherService;
        }

        public async Task<List<Post>> GetAsync() => 
            await postCollection.Find(x => true).SortByDescending(x=>x.CreateTime).ToListAsync(); 
        
        public async Task<DataList<Post>> GetAsync(int page = 0,int limit = 10)
        {
            var postList = new DataList<Post>();
            postList.Data = await postCollection.Find(x => x.Active).Skip(page * limit).Limit(10).SortByDescending(x => x.CreateTime).ToListAsync();
            postList.CurrentPage = page;
            postList.Count = limit;
            postList.TotalCount = await postCollection.Find(x => x.Active).CountDocumentsAsync();

            return postList;
        }

        public async Task<Post?> GetAsync(string id) =>
            await postCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Post> CreateAsync(Post newModel)
        {
            newModel.CreateTime = DateTime.Now;
            
            await postCollection.InsertOneAsync(newModel);

            MailSend(newModel);

            return newModel;
        }

        private void MailSend(Post newModel)
        {
            var messages = new List<MailMessageData>();
            messages.Add(new MailMessageData()
            {
                To = newModel.Email,
                Subject = newModel.Title,
                Body = newModel.Content
            });

            publisherService.Enqueue(messages,RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString());
        }

        public async Task UpdateAsync(string id, Post updatedModel) =>
            await postCollection.ReplaceOneAsync(x => x.Id == id, updatedModel);

        public async Task RemoveAsync(string id) =>
            await postCollection.DeleteOneAsync(x => x.Id == id);

        public async Task ChangeStatus(Post model)
        {
            model.Active = !model.Active;
            model.UpdateTime = DateTime.Now;

            await postCollection.ReplaceOneAsync(x => x.Id == model.Id, model);
        }
    }
}
