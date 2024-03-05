using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjecteV2.ApiMongoDB{
    /// <summary>
    /// Classe per a la col·lecció de lletres
    /// </summary>
    public class Lletra{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("UID")]
        public string UIDSong { get; set; }

        [BsonElement("Lletra")]
        public string lletra { get; set; }
        
    }
}