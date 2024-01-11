namespace MongoStoreApi.Models;
public class MongostoreDatabaseSettings{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CançonsCollectionName { get; set; } = null!;
}