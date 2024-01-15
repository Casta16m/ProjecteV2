using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class HistorialService{
    private readonly IMongoCollection<Historial> _HistorialCollection;
    public HistorialService(
        IOptions<MongoStoreDatabaseSettings> mongoStoreDatabaseSettings){
            var mongoClient = new MongoClient(
                mongoStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                mongoStoreDatabaseSettings.Value.DatabaseName);            
            _HistorialCollection = mongoDatabase.GetCollection<Historial>(
                mongoStoreDatabaseSettings.Value.LletresCollectionName); 
        }
        public async Task<List<Historial>> GetAsync() => 
            (await _HistorialCollection.FindAsync(canço => true)).ToList();
        public async Task<Historial?> GetAsync(string id) =>
            await _HistorialCollection.Find(x => x._ID == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Historial newHistorial) =>
        await _HistorialCollection.InsertOneAsync(newHistorial);
        public async Task UpdateAsync(string id, Historial updatedHistorial) =>
            await _HistorialCollection.ReplaceOneAsync(x => x._ID == id, updatedHistorial);
        public async Task RemoveAsync(string id) =>
            await _HistorialCollection.DeleteOneAsync(x => x._ID == id);
}
