using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoStoreApi.Models;
using ProjecteV2.ApiMongoDB;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;  // Para GridFSUploadOptions


namespace MongoStoreApi.Services;
/// <summary>
/// Servei per a la col·lecció de songs
/// </summary>
public class SongService{
    private readonly IMongoCollection<Song> _songsCollection;
    private readonly GridFSBucket _gridFsBucket;

    /// <summary>
    /// Constructor de la classe
    /// </summary>
    /// <param name="MongoStoreDatabaseSettings"></param>
    public SongService(IOptions<MongoStoreDatabaseSettings> MongoStoreDatabaseSettings)
    {
            IMongoClient mongoClient = new MongoClient(MongoStoreDatabaseSettings.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                MongoStoreDatabaseSettings.Value.DatabaseName);
            _songsCollection = mongoDatabase.GetCollection<Song>(MongoStoreDatabaseSettings.Value.SongsCollectionName);
            _gridFsBucket = new GridFSBucket(mongoDatabase);

    }

        /// <summary>
        /// Aconseguir totes les songs
        /// </summary>
        /// <returns></returns>
        public async Task<List<Song>> GetAsync() => 
            (await _songsCollection.FindAsync(Song => true)).ToList();
            
        /// <summary>
        /// Aconseguir song específica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Song?> GetAsync(string id) =>
            await _songsCollection.Find(x => x._ID== id).FirstOrDefaultAsync();
        
        /// <summary>
        /// Aconseguir song per UID
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public async Task<Song?> GetByAudioIDAsync(string UID) =>
            await _songsCollection.Find(x => x.UID == UID).FirstOrDefaultAsync();

        /// <summary>
        /// Crear song
        /// </summary>
        /// <param name="newSong"></param>
        /// <returns></returns>
        public async Task CreateAsync(Song newSong) =>
        await _songsCollection.InsertOneAsync(newSong);
    
        /// <summary>
        /// Actualitzar song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedSong"></param>
        /// <returns></returns>
        public async Task UpdateAsync(string id, Song updatedSong) =>
            await _songsCollection.ReplaceOneAsync(x => x._ID == id, updatedSong);

        /// <summary>
        /// Pujar song
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="stream"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<ObjectId> UploadAudioAsync(string filename, Stream stream, GridFSUploadOptions options)
        {
            return await _gridFsBucket.UploadFromStreamAsync(filename, stream, options);
        }

        /// <summary>
        /// Aconseguir Auidio
        /// </summary>
        /// <param name="audioFileId"></param>
        /// <returns></returns>
        public async Task<Stream?> GetAudioStreamAsync(ObjectId audioFileId)
        {
            try
            {
                var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", audioFileId);
                var cursor = await _gridFsBucket.FindAsync(filter);

                // Esperar a que la tarea se complete antes de intentar acceder a los resultados
                await cursor.MoveNextAsync();

                var fileInfo = cursor.Current.FirstOrDefault();

                if (fileInfo == null)
                {
                    return null;
                }

                var audioStream = await _gridFsBucket.OpenDownloadStreamAsync(audioFileId);
                return audioStream;
            }
            catch (Exception ex)
            {
                // Log de depuración
                Console.WriteLine($"Error in GetAudioStreamAsync: {ex.Message}");
                return null;
            }
        }

}
