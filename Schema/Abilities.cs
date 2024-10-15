using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Schema
{
    public class Abilities
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

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}