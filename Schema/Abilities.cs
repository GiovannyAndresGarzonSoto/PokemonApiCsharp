using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokemonApi.Schema
{
    public class Abilities
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("description")]
        [BsonRequired]
        public string Description { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}