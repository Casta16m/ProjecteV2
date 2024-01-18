using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class LletraService{
    private readonly IMongoCollection<Lletra> _LletraCollection;
    public LletraService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
        IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            MongoStoreDatabaseSettings.Value.DatabaseName);
         _LletraCollection = mongoDatabase.GetCollection<Lletra>(MongoStoreDatabaseSettings.Value.LletresCollectionName);
    }
    public async Task<List<Lletra>> GetAsync() => 
        (await _LletraCollection.FindAsync(Lletra => true)).ToList();
    public async Task<Lletra?> GetAsync(string id) =>
        await _LletraCollection.Find(x => x._ID== id).FirstOrDefaultAsync();
    public async Task<Lletra?> GetByAudioIDAsync(string audioID) =>
        await _LletraCollection.Find(x => x.AudioID== audioID).FirstOrDefaultAsync();
    public async Task CreateAsync(Lletra newLletra) =>
    await _LletraCollection.InsertOneAsync(newLletra);
    public async Task UpdateAsync(string id, Lletra updatedLletra) =>
        await _LletraCollection.ReplaceOneAsync(x => x._ID == id, updatedLletra);
}