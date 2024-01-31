using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
/// <summary>
/// Servei per a la col·lecció de lletres
/// </summary>
public class LletraService{
    private readonly IMongoCollection<Lletra> _LletraCollection;

    /// <summary>
    /// Constructor de la classe
    /// </summary>
    /// <param name="MongoStoreDatabaseSettings"></param>
    public LletraService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
        IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            MongoStoreDatabaseSettings.Value.DatabaseName);
         _LletraCollection = mongoDatabase.GetCollection<Lletra>(MongoStoreDatabaseSettings.Value.LletresCollectionName);
    }

    /// <summary>
    /// Aconseguir totes les lletres
    /// </summary>
    /// <returns></returns>
    public async Task<List<Lletra>> GetAsync() => 
        (await _LletraCollection.FindAsync(Lletra => true)).ToList();
    
    /// <summary>
    /// Aconseguir lletra específica
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Lletra?> GetAsync(string id) =>
        await _LletraCollection.Find(x => x._ID== id).FirstOrDefaultAsync();

    /// <summary>
    /// Aconseguir lletra per UIDSong
    /// </summary>
    /// <param name="UIDSong"></param>
    /// <returns></returns>
    public async Task<Lletra?> GetByUIDSongAsync(string UIDSong) =>
        await _LletraCollection.Find(x => x.UIDSong== UIDSong).FirstOrDefaultAsync();

    /// <summary>
    /// Crear lletra
    /// </summary>
    /// <param name="newLletra"></param>
    /// <returns></returns>
    public async Task CreateAsync(Lletra newLletra) =>
    await _LletraCollection.InsertOneAsync(newLletra);

    /// <summary>
    /// Actualitzar lletra
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedLletra"></param>
    /// <returns></returns>
    public async Task UpdateAsync(string id, Lletra updatedLletra) =>
        await _LletraCollection.ReplaceOneAsync(x => x._ID == id, updatedLletra);
}