using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class SongService{
    private readonly IMongoCollection<Song> _songsCollection;
    public SongService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
            IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                MongoStoreDatabaseSettings.Value.DatabaseName);
           _songsCollection = mongoDatabase.GetCollection<Song>(MongoStoreDatabaseSettings.Value.SongsCollectionName);
        }
        public async Task<List<Song>> GetAsync() => 
            (await _songsCollection.FindAsync(Song => true)).ToList();
            
        public async Task<Song?> GetAsync(string id) =>
            await _songsCollection.Find(x => x._ID== id).FirstOrDefaultAsync();
        public async Task<Song?> GetByAudioIDAsync(string audioID) =>
            await _songsCollection.Find(x => x.AudioId == audioID).FirstOrDefaultAsync();

        public async Task CreateAsync(Song newSong) =>
        await _songsCollection.InsertOneAsync(newSong);
        public async Task UpdateAsync(string id, Song updatedSong) =>
            await _songsCollection.ReplaceOneAsync(x => x._ID == id, updatedSong);
 
}
