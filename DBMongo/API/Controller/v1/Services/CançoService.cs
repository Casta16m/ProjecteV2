using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class CançoService{
    private readonly IMongoCollection<Canço> _cançonsCollection;
    public CançoService(
        IOptions<MongoStoreDatabaseSettings> mongoStoreDatabaseSettings){
            var mongoClient = new MongoClient(
                mongoStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                mongoStoreDatabaseSettings.Value.DatabaseName);            
            _cançonsCollection = mongoDatabase.GetCollection<Canço>(
                mongoStoreDatabaseSettings.Value.CançonsCollectionName); 
        }
        public async Task<List<Canço>> GetAsync() => 
            (await _cançonsCollection.FindAsync(canço => true)).ToList();
            
        public async Task<Canço?> GetAsync(string id) =>
            await _cançonsCollection.Find(x => x._ID== id).FirstOrDefaultAsync();
        public async Task CreateAsync(Canço newCanço) =>
        await _cançonsCollection.InsertOneAsync(newCanço);
        public async Task UpdateAsync(string id, Canço updatedCanço) =>
            await _cançonsCollection.ReplaceOneAsync(x => x._ID == id, updatedCanço);
        public async Task RemoveAsync(string id) =>
            await _cançonsCollection.DeleteOneAsync(x => x._ID == id);
}
