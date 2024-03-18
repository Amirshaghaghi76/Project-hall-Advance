namespace api.Dtos;

public record UserDto(
    string Id,
    string Email,
    string KnownAs,
    DateTime Created,
    DateTime LastActive,
    string Gender,
    string? LookingFor,
    string City,
    string Country,
    int Age,
    List<Photo> Photos
);

