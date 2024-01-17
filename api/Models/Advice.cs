namespace api.Models;

public record Advice
(
[property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
 [MinLength(11), MaxLength(11)] string PhoneNumber
);
