namespace MongoStoreApi.Models;
/// <summary>
/// Classe per a la col·lecció de songs
/// </summary>
public class MongoStoreDatabaseSettings{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string HistorialCollectionName { get; set; } = null!;
    public string SongsCollectionName { get; set; } = null!;
    public string LletresCollectionName { get; set; } = null!;
    
}