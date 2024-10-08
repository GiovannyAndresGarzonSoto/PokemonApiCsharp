using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokemonApi.Schema
{
    public class Move
    {
        [BsonId] 
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("description")]
        [BsonRequired]
        public string Description { get; set; }

        [BsonElement("power")]
        public int? Power { get; set; }

        [BsonElement("accuracy")]
        public int? Accuracy { get; set; }

        [BsonElement("type")] 
        //[BsonRequired] 
        public ObjectId? TypeId { get; set; } 

        [BsonElement("class")] 
        //[BsonRequired]
        public ObjectId? ClassId { get; set; }

        [BsonElement("createdAt")] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
