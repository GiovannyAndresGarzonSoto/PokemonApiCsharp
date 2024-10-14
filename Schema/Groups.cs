using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokemonApi.Schema
{
    public class Groups
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("name")]
        [BsonRequired]
        public String Name { get; set; }
        
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
