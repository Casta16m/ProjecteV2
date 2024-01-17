using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
public class HistorialService
{
    private readonly IMongoCollection<Historial> _historialCollection;
    public HistorialService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
        IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            MongoStoreDatabaseSettings.Value.DatabaseName);
         _historialCollection = mongoDatabase.GetCollection<Historial>(MongoStoreDatabaseSettings.Value.HistorialCollectionName);
    }
    public async Task<List<Historial>> GetAsync() => 
        (await _historialCollection.FindAsync(Historial => true)).ToList();
        
    public async Task<Historial?> GetAsync(string id) =>
        await _historialCollection.Find(x => x._ID== id).FirstOrDefaultAsync();
    public async Task CreateAsync(Historial newHistorial) =>
    await _historialCollection.InsertOneAsync(newHistorial);
    public async Task UpdateAsync(string id, Historial updatedHistorial) =>
        await _historialCollection.ReplaceOneAsync(x => x._ID == id, updatedHistorial);
   
  
}