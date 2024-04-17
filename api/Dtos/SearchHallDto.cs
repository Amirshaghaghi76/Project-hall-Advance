namespace api.Dtos;

public record SearchHallDto
(
     string Name,
     string City,
     string? PriceLevel,
     int Capacity,
     string PhoneNumber,
     bool Parking,
     bool WeddingRoom,
     bool? FreeWifi,
     bool? Cofe,
     bool? Elevator,
     string Url_512, //upload PhotoMain,
     string Description
);

