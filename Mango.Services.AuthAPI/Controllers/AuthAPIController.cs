using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            this._responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {

            var errorMessage = await _authService.Register(registrationRequestDto);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(errorMessage);
            }

            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);

            if (loginResponse.User == null)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";

                return BadRequest(_responseDto);

            }
            _responseDto.Result = loginResponse;

            return Ok(_responseDto);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registerRequestDto)
        {
            var isAssignRoleSuccessful = await _authService.AssignRole(registerRequestDto.Email, registerRequestDto.Role.ToUpper());

            if (!isAssignRoleSuccessful)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";

                return BadRequest(_responseDto);

            }

            return Ok(_responseDto);
        }
    }


}
