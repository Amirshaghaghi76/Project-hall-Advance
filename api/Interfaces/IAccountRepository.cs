using api.Dtos;

namespace api.Interfaces;

public interface IAccountRepository
{

    public Task<LoggedInDto?> Create(RegisterDto userInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> Login(LoginDto userInput, CancellationToken cancellationToken);
}
