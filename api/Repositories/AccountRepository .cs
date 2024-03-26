using System.Security.Cryptography;
using api.Dtos;


namespace api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private const string _collectionName = "users";
        private readonly IMongoCollection<AppUser>? _collection;
        private readonly ITokenService _tokenService;

        public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
        {
            var database = client.GetDatabase(dbSettings.DatabaseName);
            _collection = database.GetCollection<AppUser>(_collectionName);
            _tokenService = tokenService;
        }

        public async Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
        {
            bool doesAccountExist = await _collection.Find<AppUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

            if (doesAccountExist)
                return null;
            // using var hmac = new HMACSHA512();

            AppUser appUser = _Mappers.ConvertRegisterDtoToAppUser(userInput);

            if (_collection is not null)
                await _collection.InsertOneAsync(appUser, null, cancellationToken);

            if (appUser.Id is not null)
            {
                // LoggedInDto loggedInDto = new LoggedInDto(
                //     Id: appUser.Id,
                //     Email: appUser.Email,
                //     KnownAs: appUser.KnownAs,
                //     Token: _tokenService.CreateToken(appUser)
                // );

                string token = _tokenService.CreateToken(appUser);

                LoggedInDto loggedInDto = _Mappers.ConvertAppUserToLoggedInDto(appUser, token);

                return loggedInDto;
            }
            
            return null;
        }

        public async Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
        {
            AppUser appUser = await _collection.Find<AppUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

            using var hmac = new HMACSHA512(appUser.PasswordSalt!);

            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.PassWord));

            if (appUser.PasswordHash is not null && appUser.PasswordHash.SequenceEqual(ComputedHash))

                UpdateLastActiveInDb(appUser, cancellationToken);

            // AppUser appUser = await _collection.Find<AppUser>(user =>
            // user.Email == userInput.Email.ToLower().Trim() && user.PassWord == userInput.PassWord)
            // .FirstOrDefaultAsync(cancellationToken);

            // if (appUser is null)
            //     return null;

            if (appUser.Id is not null)
            {
                string toKen = _tokenService.CreateToken(appUser);

                return _Mappers.ConvertAppUserToLoggedInDto(appUser,toKen);
            }

            return null;
        }


        private async void UpdateLastActiveInDb(AppUser appUser, CancellationToken cancellationToken)
        {
            UpdateDefinition<AppUser> newLastActive = Builders<AppUser>.Update.Set(user =>
                 user.LastActive, DateTime.UtcNow);

            await _collection.UpdateOneAsync<AppUser>(user =>
             user.Id == appUser.Id, newLastActive, null, cancellationToken);
        }
    }
}