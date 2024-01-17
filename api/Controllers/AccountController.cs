using api.Dtos;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        [HttpPost("register")]
        public async Task<ActionResult<LoggedInDto?>> Register(RegisterDto userInput, CancellationToken cancellationToken)
        {
            if (userInput.PassWord != userInput.ConfrimPassword)
                return BadRequest("password dont match");

            LoggedInDto? loggedInDto = await _accountRepository.Create(userInput, cancellationToken);

            if (loggedInDto is null)
                return BadRequest("Email/Username is taken.");

            return loggedInDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoggedInDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
        {
            LoggedInDto? userDto = await _accountRepository.Login(userInput, cancellationToken);

            if (userDto is null)
                return Unauthorized ("Wrong username or password");

            return userDto;
        }

    }
}
