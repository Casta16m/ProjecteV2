using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class AudioService{
    private readonly IMongoCollection<Audio> _audioCollection;
    public AudioService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
            IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                MongoStoreDatabaseSettings.Value.DatabaseName);
           _audioCollection = mongoDatabase.GetCollection<Audio>(MongoStoreDatabaseSettings.Value.AudiosCollectionName);
        }
        public async Task<List<Audio>> GetAsync() => 
            (await _audioCollection.FindAsync(Audio => true)).ToList();
            
        public async Task<Audio?> GetAsync(string id) =>
            await _audioCollection.Find(x => x._ID== id).FirstOrDefaultAsync();

        public async Task CreateAsync(Audio newAudio) =>
        await _audioCollection.InsertOneAsync(newAudio);
        public async Task UpdateAsync(string id, Audio updatedAudio) =>
            await _audioCollection.ReplaceOneAsync(x => x._ID == id, updatedAudio);
}