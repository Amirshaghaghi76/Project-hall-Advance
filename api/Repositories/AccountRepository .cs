using System.Security.Cryptography;
using System.Text;
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

        public async Task<LoggedInDto?> Create(RegisterDto userInput, CancellationToken cancellationToken)
        {
            bool doseAccountExist = await _collection.Find<AppUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

            if (doseAccountExist)
                return null;
            using var hmac = new HMACSHA512();

            AppUser appUser = new AppUser(
             Id: null,
             Name: userInput.Name,
             PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.PassWord)),
             PasswordSalt: hmac.Key,
             Email: userInput.Email,
             Age: userInput.Age
            );

            if (_collection is not null)
                await _collection.InsertOneAsync(appUser, null, cancellationToken);

            if (appUser.Id is not null)
            {
                LoggedInDto loggedInDto = new LoggedInDto(
                    Id: appUser.Id,
                    Email: appUser.Email,
                    Token: _tokenService.CreateToken(appUser)
                );

                return loggedInDto;
            }
            return null;
        }

        public async Task<LoggedInDto?> Login(LoginDto userInput, CancellationToken cancellationToken)
        {
            AppUser appUser = await _collection.Find<AppUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

            using var hmac = new HMACSHA512(appUser.PasswordSalt);

            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.PassWord));

            if (appUser.PasswordHash is not null && appUser.PasswordHash.SequenceEqual(ComputedHash))

                // AppUser appUser = await _collection.Find<AppUser>(user =>
                // user.Email == userInput.Email.ToLower().Trim() && user.PassWord == userInput.PassWord)
                // .FirstOrDefaultAsync(cancellationToken);

                // if (appUser is null)
                //     return null;

                if (appUser.Id is not null)
                {
                    return new LoggedInDto(
                        Id: appUser.Id,
                        Token: _tokenService.CreateToken(appUser),
                        Email: appUser.Email

                        );
                }

            return null;
        }
    }
}