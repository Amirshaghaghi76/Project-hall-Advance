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
                    // Id: appUser.Id!, id is rescued from Token
                    Email: appUser.Email,
                    KnownAs: appUser.KnownAs,
                    Token: toKen
                );
        }

        public static UserDto ConvertAppUserToUserDto(AppUser appUser)
        {
            return new UserDto(
                // Id: appUser.Id!, id is rescued from Token
                Email: appUser.Email,
                KnownAs: appUser.KnownAs,
                Created: appUser.Created,
                Gender: appUser.Gender,
                LastActive: appUser.LastActive,
                LookingFor: appUser.LookingFor,
                City: appUser.City,
                Country: appUser.Country,
                Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
                Photos: appUser.Photos
             );
        }

        public static MemberDto ConvertAppUserToMemberDto(AppUser appUser)
        {
            return new MemberDto(
                Id: appUser.Id!,
                Email: appUser.Email,
                KnownAs: appUser.KnownAs,
                Gender: appUser.Gender,
                LastActive: appUser.LastActive,
                LookingFor: appUser.LookingFor,
                City: appUser.City,
                Country: appUser.Country,
                Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
                Photos: appUser.Photos
             );
        }

        public static Hall ConvertRegisterHallDtoToHall(RegisterHallDto userInput)
        {
            return new Hall(
             Id: null,
             Name: userInput.Name,
             City: userInput.City,
             PriceLevel: userInput.PriceLevel,
             Capacity: userInput.Capacity,
             PhoneNumber: userInput.PhoneNumber,
             Parking: userInput.Parking,
             WeddingRoom: userInput.WeddingRoom,
             FreeWifi: userInput.FreeWifi,
             Cofe: userInput.Cofe,
             Elevator: userInput.Elevator,
             Url_64: userInput.Url_64,
             Url_512: userInput.Url_512,
             Description: userInput.Description!
          // Lighting: userInput.Lighting
          );
        }
        public static LoggedInHallDto ConvertHallToLoggedInHallDto(Hall hall)
        {
            return new LoggedInHallDto(
              Name: hall.Name,
              Url_64: hall.Url_64,
              Url_512: hall.Url_512,
              Description: hall.Description
            );
        }

        public static SearchHallDto ConvertHallToSearchHallDto(Hall hall)
        {
            return new SearchHallDto(
              Name:hall.Name,
              City:hall.City,
              PriceLevel:null,
              Capacity:hall.Capacity,
              PhoneNumber:hall.PhoneNumber,
              Parking:hall.Parking,
              WeddingRoom:hall.WeddingRoom,
              FreeWifi:null,
              Cofe:null,
              Elevator:null,
              Url_512:hall.Url_512, //upload PhotoMain,
              Description:hall.Description
            );
        }
    }
}