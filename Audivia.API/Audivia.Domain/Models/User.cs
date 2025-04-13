using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("username")]
    public string? Username { get; set; }

    [BsonElement("password")]
    public string Password { get; set; } = null!;

    [BsonElement("phone")]
    public string? Phone { get; set; }

    [BsonElement("avatar_url")]
    public string? AvatarUrl { get; set; }

    [BsonElement("bio")]
    public string? Bio { get; set; }

    [BsonElement("balance_wallet")]
    public int BalanceWallet { get; set; } = 0;

    [BsonElement("audio_character_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AudioCharacterId { get; set; }

    [BsonElement("auto_play_distance")]
    public int? AutoPlayDistance { get; set; } = 0;

    [BsonElement("travel_distance")]
    public int? TravelDistance { get; set; } = 0;

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [BsonElement("role_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? RoleId { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
