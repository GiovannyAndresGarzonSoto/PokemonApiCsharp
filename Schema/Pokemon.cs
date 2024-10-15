namespace PokemonApi.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

public class Pokemon
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("number")]
    [BsonRequired] 
    public int Number { get; set; }

    [BsonElement("name")]
    [BsonRequired]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Name { get; set; }

    [BsonElement("description")]
    [BsonRequired]
    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string Description { get; set; }

    [BsonElement("weight")]
    [BsonRequired] 
    public double Weight { get; set; }

    [BsonElement("height")]
    [BsonRequired] 
    public double Height { get; set; }

    [BsonElement("hp")]
    [BsonRequired] 
    public int HP { get; set; }

    [BsonElement("attack")]
    [BsonRequired] 
    public int Attack { get; set; }

    [BsonElement("defense")]
    [BsonRequired] 
    public int Defense { get; set; }

    [BsonElement("spAttack")]
    [BsonRequired] 
    public int SpAttack { get; set; }

    [BsonElement("spDefense")]
    [BsonRequired] 
    public int SpDefense { get; set; }

    [BsonElement("speed")]
    [BsonRequired] 
    public int Speed { get; set; }

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; }

    [BsonElement("publicId")]
    public string PublicId { get; set; }

    [BsonElement("type1")]
    [BsonRequired] 
    public ObjectId Type1Id { get; set; }

    [BsonElement("type2")]
    public ObjectId? Type2Id { get; set; }

    [BsonElement("group1")]
    [BsonRequired] 
    public ObjectId Group1Id { get; set; }

    [BsonElement("group2")]
    public ObjectId? Group2Id { get; set; }

    [BsonElement("ability1")]
    [BsonRequired] 
    public ObjectId Ability1Id { get; set; }

    [BsonElement("ability2")]
    public ObjectId? Ability2Id { get; set; }

    [BsonElement("ability3")]
    public ObjectId? Ability3Id { get; set; }
}