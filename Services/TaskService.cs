using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MVC.Models;

namespace MVC.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<TodoTaskModel> _tasksCollection;

        public TaskService(
            IOptions<TasksDatabaseSettings> taskDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                taskDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                taskDatabaseSettings.Value.DatabaseName);

            _tasksCollection = mongoDatabase.GetCollection<TodoTaskModel>(
                taskDatabaseSettings.Value.TasksCollectionName);
        }

        public async Task<List<TodoTaskModel>> GetAsync() =>
            await _tasksCollection.Find(_ => true).ToListAsync();

        public async Task<List<TodoTaskModel>> GetAsync(bool completed) =>
            await _tasksCollection.Find(x => x.Completed == completed).ToListAsync();

        public async Task<TodoTaskModel?> GetAsync(string id) =>
            await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TodoTaskModel task) =>
            await _tasksCollection.InsertOneAsync(task);

        public async Task UpdateAsync(string id, TodoTaskModel updastedToDoTask) =>
            await _tasksCollection.ReplaceOneAsync(x => x.Id == id, updastedToDoTask);

        public async Task RemoveAsync(string id) =>
            await _tasksCollection.DeleteOneAsync(x => x.Id == id);

        public async Task RemoveAsync(bool completed) =>
            await _tasksCollection.DeleteManyAsync(x => x.Completed == completed);

    }
}