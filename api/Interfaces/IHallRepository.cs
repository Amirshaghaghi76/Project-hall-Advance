namespace api.Interfaces;

public interface IHallRepository
{
    public Task<LoggedInHallDto> CreateAsync(RegisterHallDto userInput, CancellationToken cancellationToken);
    public Task<SearchHallDto?> GetByNameAsync(string name , CancellationToken cancellationToken);
    public Task<List<SearchHallDto>> GetAllAsync(CancellationToken cancellationToken);
}