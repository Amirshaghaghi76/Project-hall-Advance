namespace api.Controllers
{
    public class MemberController(IMemberRepository _memberRepository) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll(CancellationToken cancellationToken)
        {
            List<MemberDto> userDtos = await _memberRepository.GetAllAsync(cancellationToken);
            if (userDtos.Count == 0) // (!userDtos.Any()) dotnet7 
                return NoContent();

            return userDtos;
        }

        [HttpGet("get-by-id/{memberId}")]
        public async Task<ActionResult<MemberDto>> GetById(string memberId, CancellationToken cancellationToken)
        {
            MemberDto? memberDto = await _memberRepository.GetByIdAsync(memberId, cancellationToken);

            if (memberDto is null)
                return NotFound("No user with this ID");

            return memberDto;
        }

        [HttpGet("get-by-email/{memberEmail}")]
        public async Task<ActionResult<MemberDto>> GetByEmail(string memberEmail, CancellationToken cancellationToken)
        {
            MemberDto? memberDto = await _memberRepository.GetByEmailAsync(memberEmail, cancellationToken);

            if (memberDto is null)
                return NotFound("No user with this email address");

            return memberDto;
        }

    }
}