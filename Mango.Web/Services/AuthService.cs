using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Mango.Web.Utils;

namespace Mango.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetail.AuthApiBaseUrl + "/api/auth/assign-role"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticDetail.AuthApiBaseUrl + "/api/auth/login"
            }, false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utils.StaticDetail.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticDetail.AuthApiBaseUrl + "/api/auth/register"
            }, false);
        }
    }
}
