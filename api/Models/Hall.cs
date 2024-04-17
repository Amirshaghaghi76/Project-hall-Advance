namespace api.Models;

public record Hall
(
     [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
      string Name, 
      string City,
      int PriceLevel,
      int Capacity,
     string PhoneNumber,
     bool Parking,
     bool WeddingRoom,
     bool FreeWifi,
     bool Cofe,
     bool Elevator,
     string Url_64,  //upload  Photo PhotoLogo,
     string Url_512, //upload PhotoMain,
     string Description
    // bool Lighting
    );
