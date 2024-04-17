namespace api.Dtos;

public record RegisterHallDto
(
     [Length(3, 9)] string Name,
     [Length(3, 8)] string City,
     [Range(3, 10)] int PriceLevel,
     [Range(50, 2000)] int Capacity,
     [Length(5, 11)] string PhoneNumber,
     bool Parking,
     bool WeddingRoom,
     bool FreeWifi,
     bool Cofe,
     bool Elevator,
     string Url_64, //upload  Photo PhotoLogo,
     string Url_512, //upload PhotoMain,
    [Length(10, 50)] string? Description
// bool Lighting
);

public record LoggedInHallDto(
string Name,
string Url_64,
string Url_512,
string Description
);