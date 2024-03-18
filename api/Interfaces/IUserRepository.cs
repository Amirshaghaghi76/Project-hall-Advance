using api.Dtos;

namespace api.Interfaces;

public interface IUserRepository
{
    public Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);

    public Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken);

    public Task<UserDto?> GetByEmailAsync(string userEmail, CancellationToken cancellationToken);

}
