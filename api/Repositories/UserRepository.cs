using api.Dtos;

namespace api.Repositories;

public class UserRepository : IUserRepository
{

    private const string _collectionName = "users";

    IMongoCollection<AppUser>? _collection;
    public UserRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
    }
    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<AppUser> appUsers = await _collection.Find<AppUser>(new BsonDocument())
        .ToListAsync(cancellationToken);

        List<UserDto> userDtos = new List<UserDto>();

        if (appUsers.Any())
        {
            foreach (AppUser appUser in appUsers)
            {
                UserDto userDto = _Mappers.ConvertAppUserToUserDto(appUser);

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos;
    }

    public async Task<UserDto?> GetByIdAsync(string userId, CancellationToken cancellationToken)
    {
        
        AppUser appUser = await _collection.Find<AppUser>(user => user.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appUser.Id is not null)
        {
            // UserDto userDto = GeneRateUserDto(appUser);
            // return userDto;
            return _Mappers.ConvertAppUserToUserDto(appUser);
        }

        return null;
    }

    public async Task<UserDto?> GetByEmailAsync(string userEmail, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user =>
        user.Email == userEmail).FirstOrDefaultAsync(cancellationToken);

        if (appUser.Id is not null)
        {
            return _Mappers.ConvertAppUserToUserDto(appUser);
            // return new UserDto(
            //     Id: appUser.Id!,
            //     Email: appUser.Email,
            //     KnownAs: appUser.KnownAs,
            //     Created: appUser.Created,
            //     Gender: appUser.Gender,
            //     LastActive: appUser.LastActive,
            //     LookingFor: appUser.LookingFor,
            //     City: appUser.City,
            //     Country: appUser.Country,
            //     Dob: appUser.DateOfBirth,
            //     Photos: appUser.Photos
            //  );
        }

        return null;
    }

}
