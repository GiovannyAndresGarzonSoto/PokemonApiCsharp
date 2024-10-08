using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokemonApi.Schema
{
    public class Group
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("name")]
        public String Name { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
