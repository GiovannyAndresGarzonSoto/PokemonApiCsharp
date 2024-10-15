using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Schema
{
    public class Moves
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [BsonElement("description")]
        [BsonRequired]
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Description { get; set; }

        [BsonElement("power")]
        public int? Power { get; set; }

        [BsonElement("accuracy")]
        public int? Accuracy { get; set; }

        [BsonElement("type")]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TypeId { get; set; }

        [BsonElement("class")]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ClassId { get; set; }

        [BsonElement("createdAt")] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
