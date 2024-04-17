namespace api.Interfaces;

public interface IUserRepository
{
    public Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken);
}
