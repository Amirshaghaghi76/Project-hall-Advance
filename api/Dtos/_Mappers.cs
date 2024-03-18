using System.Security.Cryptography;
using api.Extensions;

namespace api.Dtos
{
    public static class _Mappers
    {

        public static AppUser ConvertRegisterDtoToAppUser(RegisterDto userInput)
        {
            using var hmac = new HMACSHA512();

            return new AppUser(
               Id: null,
               PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.PassWord)),
               PasswordSalt: hmac.Key,
               Email: userInput.Email.ToLower().Trim(),
               DateOfBirth: userInput.DateOfBirth,
               KnownAs: userInput.KnownAs.Trim(),
               Created: DateTime.UtcNow,
               LastActive: DateTime.UtcNow,
               Gender: userInput.Gender,
               Introduction: userInput.Introduction?.Trim(),
               LookingFor: userInput.LookingFor,
               City: userInput.City,
               Country: userInput.Country,
               Photos: []
              );
        }

        public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string toKen)
        {
            return new LoggedInDto(
                    Id: appUser.Id!,
                    Email: appUser.Email,
                    KnownAs: appUser.KnownAs,
                    Token: toKen
                );
        }
        public static UserDto ConvertAppUserToUserDto(AppUser appUser)
        {
            return new UserDto(
                Id: appUser.Id!,
                Email: appUser.Email,
                KnownAs: appUser.KnownAs,
                Created: appUser.Created,
                Gender: appUser.Gender,
                LastActive: appUser.LastActive,
                LookingFor: appUser.LookingFor,
                City: appUser.City,
                Country: appUser.Country,
                Age:CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
                Photos: appUser.Photos
             );
        }
    }
}