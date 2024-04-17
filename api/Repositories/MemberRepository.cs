namespace api.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        #region Constructor
        private const string _collectionName = "users";
        IMongoCollection<AppUser>? _collection;
        public MemberRepository(IMongoClient client, IMongoDbSettings dbSettings)
        {
            var database = client.GetDatabase(dbSettings.DatabaseName);
            _collection = database.GetCollection<AppUser>(_collectionName);
        }
        #endregion Constructor

        public async Task<List<MemberDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<AppUser> appUsers = await _collection.Find<AppUser>(new BsonDocument())
            .ToListAsync(cancellationToken);

            List<MemberDto> memberDtos = []; //new List<MemberDto>() befor dotnet 8 (dotnet7)

            if (appUsers.Any())
            {
                foreach (AppUser appUser in appUsers)
                {
                    MemberDto memberDto = _Mappers.ConvertAppUserToMemberDto(appUser);

                memberDtos.Add(memberDto);
            }

            return memberDtos;
        }

        return memberDtos; // []
        }

        public async Task<MemberDto?> GetByIdAsync(string memberId, CancellationToken cancellationToken)
        {
            AppUser appUser = await _collection.Find<AppUser>(appUser =>
             appUser.Id == memberId).FirstOrDefaultAsync(cancellationToken);

            if (appUser.Id is not null)
            {
                return _Mappers.ConvertAppUserToMemberDto(appUser);
            }

            return null;
        }

        public async Task<MemberDto?> GetByEmailAsync(string memberEmail, CancellationToken cancellationToken)
        {
            AppUser appUser = await _collection.Find<AppUser>(appUser =>
            appUser.Email == memberEmail).FirstOrDefaultAsync(cancellationToken);

            if (appUser.Id is not null)
            {
                return _Mappers.ConvertAppUserToMemberDto(appUser);
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
}

