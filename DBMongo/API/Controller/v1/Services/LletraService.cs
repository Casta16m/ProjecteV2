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
                mongoStoreDatabaseSettings.Value.LletresCollectionName); 
        }
        public async Task<List<Lletra>> GetAsync() => 
            (await _LletraCollection.FindAsync(canço => true)).ToList();
        public async Task<Lletra?> GetAsync(string id) =>
            await _LletraCollection.Find(x => x._ID == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Lletra newLletra) =>
        await _LletraCollection.InsertOneAsync(newLletra);
        public async Task UpdateAsync(string id, Historial updatedHistorial) =>
            await _LletraCollection.ReplaceOneAsync(x => x._ID == id, updatedHistorial);
        public async Task RemoveAsync(string id) =>
            await _LletraCollection.DeleteOneAsync(x => x._ID == id);
}
