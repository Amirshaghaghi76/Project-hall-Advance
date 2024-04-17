public record MemberDto(
    // string Schema,
    string Id,
    string Email,
    string KnownAs,
    DateTime LastActive,
    string Gender,
    string? LookingFor,
    string City,
    string Country,
    int Age,
    List<Photo> Photos
);
