
using api.Dtos;
using api.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<UserDto> userDtos = await _userRepository.GetAllAsync(cancellationToken);
        if (!userDtos.Any())
            return NoContent();

        return userDtos;
    }
    [Authorize]
    [HttpGet("get-by-id")]
    public async Task<ActionResult<UserDto>> GetById(CancellationToken cancellationToken)
    {
        // string? userId = ClaimPrincipalExtensions.GetUserId(User);
        //TODO UserDto? userDto = await _userRepository.GetByIdAsync(ClaimPrincipalExtensions.GetUserId(User), cancellationToken);
        // UserDto? userDto = await _userRepository.GetByIdAsync(userId, cancellationToken);
        UserDto? userDto = await _userRepository.GetByIdAsync(User.GetUserId(), cancellationToken);


        if (userDto is null)
            return NotFound("No user was found");

        return userDto;
    }

    // [HttpGet("get-by-id/{userId}")]
    // public async Task<ActionResult<UserDto>> GetById(string userId, CancellationToken cancellationToken)
    // {
    //     UserDto? userDto = await _userRepository.GetByIdAsync(userId, cancellationToken);

    //     if (userDto is null)
    //         return NotFound("No user was found");

    //     return userDto;
    // }

    [HttpGet("get-by-email/{userEmail}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string userEmail, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userRepository.GetByEmailAsync(userEmail, cancellationToken);

        if (userDto is null)
            return NotFound("no user with this email address");

        return userDto;
    }

}





//     [HttpPut("update/{userId}")]
//     public ActionResult<UpdateResult> UpdateUser(string userId, AppUser userIn)
//     {
//         var UpdateUser = Builders<AppUser>.Update
//         .Set((AppUser doc) => doc.Name, userIn.Name.ToLower())
//         .Set((AppUser doc) => doc.PassWord, userIn.PassWord)
//         .Set((AppUser doc) => doc.ConfrimPassword, userIn.ConfrimPassword)
//         .Set((AppUser doc) => doc.Age, userIn.Age);

//         return _collection.UpdateOne<AppUser>(doc => doc.Id == userId, UpdateUser);
//     }

//     [HttpDelete("delete/{userId}")]
//     public ActionResult<DeleteResult> Delete(string userId)
//     {
//         return _collection.DeleteOne<AppUser>(doc => doc.Id == userId);
//     }
// }
