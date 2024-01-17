
namespace api.Models;

public record AppUser
(
    [property: BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] string? Id,
    [MinLength(3), MaxLength(10)] string Name,
    byte[] PasswordSalt,
    byte[] PasswordHash,    
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    [Range(18, 99)] int Age
);
