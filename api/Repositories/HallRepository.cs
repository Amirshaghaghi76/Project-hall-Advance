namespace api.Repositories;

public class HallRepository : IHallRepository
{
    #region Constructor
    private const string _collectionName = "halls";
    private readonly IMongoCollection<Hall>? _collection;

    public HallRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Hall>(_collectionName);
    }

    #endregion Constructor 
    public async Task<LoggedInHallDto> CreateAsync(RegisterHallDto userInput, CancellationToken cancellationToken)
    {
        bool doseHallExist = await _collection.Find<Hall>(user =>
        user.Name == userInput.Name.ToLower()).AnyAsync(cancellationToken);

        if (doseHallExist)
            return null!;

        Hall hall = _Mappers.ConvertRegisterHallDtoToHall(userInput);

        if (_collection is not null)
            await _collection.InsertOneAsync(hall, null, cancellationToken);

        if (hall.Id is not null)
        {
            LoggedInHallDto loggedInHallDto = _Mappers.ConvertHallToLoggedInHallDto(hall);

            return loggedInHallDto;
        }

        return null!;
    }

    public async Task<SearchHallDto?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        Hall hall = await _collection.Find<Hall>(hall =>
        hall.Name == name).FirstOrDefaultAsync(cancellationToken);

        if (hall.Name is not null)
        {
            SearchHallDto searchHallDto = _Mappers.ConvertHallToSearchHallDto(hall);

            return searchHallDto;
        }

        return null;
    }

    public async Task<List<SearchHallDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Hall> halls = await _collection.Find<Hall>(new BsonDocument())
        .ToListAsync(cancellationToken);

        List<SearchHallDto> searchHallDtos = [];

        if (halls.Any())
        {
            foreach (Hall hall in halls)
            {
                SearchHallDto searchHallDto = _Mappers.ConvertHallToSearchHallDto(hall);

                searchHallDtos.Add(searchHallDto);
            }

            return searchHallDtos;
        }

        return searchHallDtos; //[]
    }
}


