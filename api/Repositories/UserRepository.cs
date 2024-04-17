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

    public async Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken)
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
}
