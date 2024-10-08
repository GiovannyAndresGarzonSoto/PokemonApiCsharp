using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokemonApi.Schema
{
    public class Type
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("publicId")]
        public string PublicId { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}