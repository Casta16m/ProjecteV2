using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;

namespace MongoStoreApi.Services;
/// <summary>
/// Servei per a la col·lecció de historial
/// </summary>
public class HistorialService
{
    private readonly IMongoCollection<Historial> _historialCollection;

    /// <summary>
    /// Constructor de la classe
    /// </summary>
    /// <param name="MongoStoreDatabaseSettings"></param>
    public HistorialService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
        IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            MongoStoreDatabaseSettings.Value.DatabaseName);
        _historialCollection = mongoDatabase.GetCollection<Historial>(MongoStoreDatabaseSettings.Value.HistorialCollectionName);
    }

    /// <summary>
    /// Aconseguir totes les historials
    /// </summary>
    /// <returns></returns>
    public async Task<List<Historial>> GetAsync() => 
        (await _historialCollection.FindAsync(Historial => true)).ToList();
        
    /// <summary>
    /// Aconseguir historial específica
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Historial?> GetAsync(string id) =>
        await _historialCollection.Find(x => x._ID== id).FirstOrDefaultAsync();

    /// <summary>
    /// Aconseguir historial per MAC
    /// </summary>
    /// <param name="MAC"></param>
    /// <returns></returns>
    public async Task<Historial?> GetByMACAsync(string MAC) =>
        await _historialCollection.Find(x => x.MAC == MAC).FirstOrDefaultAsync();

    /// <summary>
    /// Aconseguir historial per UIDSong
    /// </summary>
    /// <param name="UIDSong"></param>
    /// <param name="MAC"></param>
    /// <returns></returns>
    public async Task<Historial?> GetByUIDSongAndMACAsync(string UIDSong, string MAC) =>
        await _historialCollection.Find(x => x.UIDSong == UIDSong && x.MAC == MAC).FirstOrDefaultAsync();
    
    /// <summary>
    /// Crear historial
    /// </summary>
    /// <param name="newHistorial"></param>
    /// <returns></returns>
    public async Task CreateAsync(Historial newHistorial) =>
    await _historialCollection.InsertOneAsync(newHistorial);

    /// <summary>
    /// Actualitzar historial
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedHistorial"></param>
    /// <returns></returns>
    public async Task UpdateAsync(string id, Historial updatedHistorial) =>
        await _historialCollection.ReplaceOneAsync(x => x._ID == id, updatedHistorial);

}