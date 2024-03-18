
namespace api.Models;

public record AppUser
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string Email,
    byte[]? PasswordSalt,
    byte[]? PasswordHash,
    DateOnly DateOfBirth,
    string KnownAs,
    DateTime Created,
    DateTime LastActive,
    string Gender,
    string? Introduction,
    string? LookingFor,
    string City,
    string Country,
    List<Photo> Photos
);
