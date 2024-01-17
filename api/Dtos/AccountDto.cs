namespace api.Dtos;

public record RegisterDto
(
[MinLength(3), MaxLength(10)] string Name,
[DataType(DataType.Password), MinLength(6), MaxLength(15)] string PassWord,
[DataType(DataType.Password), MinLength(6), MaxLength(15)] string ConfrimPassword,
[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
[Range(18, 99)] int Age
);

public record LoginDto(
   [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")]
   string Email,
   
   [DataType(DataType.Password), MinLength(6), MaxLength(15)] string PassWord
);

public record LoggedInDto
(
string Id,
string Email,
string Token
);