using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class LletraService{
    private readonly IMongoCollection<Lletra> _LletraCollection;
    public LletraService(
        IOptions<MongoStoreDatabaseSettings> mongoStoreDatabaseSettings){
            var mongoClient = new MongoClient(
                mongoStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                mongoStoreDatabaseSettings.Value.DatabaseName);            
            _LletraCollection = mongoDatabase.GetCollection<Lletra>(
                mongoStoreDatabaseSettings.Value.LletransCollectionName); 
        }
        public async Task<List<Lletra>> GetAsync() => 
            await _LletraCollection.Find(_ => true).toListAsync();
        public async Task<Lletra?> GetAsync(string id) =>
            await _LletraCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Lletra newLletra) =>
        await _LletraCollection.InsertOneAsync(newLletra);
        public async Task UpdateAsync(string id, Lletra updatedLletra) =>
            await _LletraCollection.ReplaceOneAsync(x => x.Id == id, updatedLletra);
        public async Task RemoveAsync(string id) =>
            await _LletraCollection.DeleteOneAsync(x => x.Id == id);
}
