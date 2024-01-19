namespace MongoStoreApi.Models;
public class MongoStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SongsCollectionName { get; set; } = null!;
    public string AudiosCollectionName { get; set; } = null!;
}