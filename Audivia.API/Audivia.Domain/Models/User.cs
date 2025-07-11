﻿using MongoDB.Bson.Serialization.Attributes;
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

    [BsonElement("full_name")]
    public string? FullName { get; set; }

    [BsonElement("birth_day")]
    public DateOnly? BirthDay { get; set; }

    [BsonElement("gender")]
    public bool Gender { get; set; } // true - female, false - male

    [BsonElement("job")]
    public string? Job { get; set; }

    [BsonElement("country")]
    public string? Country { get; set; }

    [BsonElement("password")]
    public string Password { get; set; } = null!;

    [BsonElement("phone")]
    public string? Phone { get; set; }

    [BsonElement("avatar_url")]
    public string? AvatarUrl { get; set; }

    [BsonElement("cover_photo")]
    public string? CoverPhoto { get; set; }

    [BsonElement("bio")]
    public string? Bio { get; set; }

    [BsonElement("balance_wallet")]
    public double BalanceWallet { get; set; } = 0;

    [BsonElement("audio_character_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AudioCharacterId { get; set; }

    [BsonElement("auto_play_distance")]
    public int? AutoPlayDistance { get; set; } = 0;

    [BsonElement("travel_distance")]
    public int? TravelDistance { get; set; } = 0;

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;
    
    [BsonElement("confirmed_email")]
    public bool ConfirmedEmail { get; set; }

    [BsonElement("token_confirm_email")]
    public string? TokenConfirmEmail { get; set; }

    [BsonElement("role_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string RoleId { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [BsonElement("email_otp")]
    public int? EmailOtp { get; set; }
    [BsonElement("email_otp_created_at")]
    public DateTime? EmailOtpCreatedAt { get; set; }

    [BsonElement("confirmed_otp")]
    public bool? ConfirmedOtp { get; set; }
}
