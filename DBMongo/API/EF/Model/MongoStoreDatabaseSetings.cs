namespace MongoStoreApi.Models;
public class MongoStoreDatabaseSettings{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CançonsCollectionName { get; set; } = null!;
    public string LletresCollectionName { get; set; } = null!;
}