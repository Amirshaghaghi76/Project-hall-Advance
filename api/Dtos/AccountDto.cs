namespace api.Dtos;

public record RegisterDto
(
// [MinLength(3), MaxLength(10)] string Name,
[MaxLength(20), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
[DataType(DataType.Password), Length(6, 15, ErrorMessage = "min of 6 and max of 15 chars are required")] string PassWord,
[DataType(DataType.Password), Length(6, 15)] string ConfrimPassword,
[Length(3, 10)] string KnownAs,
 DateOnly DateOfBirth,
[Length(3, 10)] string Gender,
[Length(5, 15)] string? Introduction,
[Length(5, 15)] string? LookingFor,
[Length(3, 10)] string Country,
[Length(3, 10)] string City,
[Range(18, 99)] int Age,
 List<Photo>? Photos
);

public record LoginDto(
   [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")]
   string Email,
   
   [DataType(DataType.Password), Length(6, 15)] string PassWord
);

public record LoggedInDto
(
string Email,
string Token,
string KnownAs
);