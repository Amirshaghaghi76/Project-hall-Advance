namespace api.Dtos;

public record UserDto(
    string Email,
    string KnownAs,
    DateTime Created,
    DateTime LastActive,
    // bool IsPrivate,
    string Gender,
    string? LookingFor,
    string City,
    string Country,
    int Age,
   List<Photo> Photos
);

