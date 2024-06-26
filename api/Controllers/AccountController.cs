using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
// dotnet version 8(line 11-)
public class AccountController(IAccountRepository _accountRepository) : ControllerBase
{

    /// <summary>
    /// Create accounts
    /// Concurrency => async is used
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>

    // private readonly IAccountRepository _accountRepository;
    // public AccountController(IAccountRepository accountRepository)
    // {
    //     _accountRepository = accountRepository; // dotnet versio 7
    // }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto?>> Register(RegisterDto userInput, CancellationToken cancellationToken)
    {
        if (userInput.PassWord != userInput.ConfrimPassword)
            return BadRequest("password dont match");

        LoggedInDto? loggedInDto = await _accountRepository.CreateAsync(userInput, cancellationToken);

        if (loggedInDto is null)
            return BadRequest("Email/Username is taken.");

        return loggedInDto;
    }

/// <summary>
    /// Login accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto? userDto = await _accountRepository.LoginAsync(userInput, cancellationToken);

        if (userDto is null)
            return Unauthorized("Wrong username or password");

        return userDto;
    }

}

