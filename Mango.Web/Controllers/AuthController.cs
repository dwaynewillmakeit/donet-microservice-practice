using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Mango.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {

            ResponseDto loginResponse = await _authService.LoginAsync(loginRequestDto);

            if (loginResponse != null && loginResponse.IsSuccess)
            {
                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(loginResponse.Result));

                await SignInUser(loginResponseDto);

                _tokenProvider.SetToken(loginResponseDto.Token);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("CustomError", loginResponse.Message);

            return View(loginRequestDto);
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>(){
            new SelectListItem{Text=StaticDetail.RoleAdmin,Value=StaticDetail.RoleAdmin},
            new SelectListItem{Text=StaticDetail.RoleCustomer,Value=StaticDetail.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {

            ResponseDto registrationResponse = await _authService.RegisterAsync(registrationRequestDto);

            if (registrationResponse != null && registrationResponse.IsSuccess)
            {

                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {

                    registrationRequestDto.Role = StaticDetail.RoleCustomer;
                }

                var assignRoleReponse = await _authService.AssignRoleAsync(registrationRequestDto);

                if (assignRoleReponse != null && assignRoleReponse.IsSuccess)
                {

                    TempData["success"] = "Registration Successful";

                    return RedirectToAction(nameof(Login));
                }

            }

            var roleList = new List<SelectListItem>(){
            new SelectListItem{Text=StaticDetail.RoleAdmin,Value=StaticDetail.RoleAdmin},
            new SelectListItem{Text=StaticDetail.RoleCustomer,Value=StaticDetail.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(registrationRequestDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(loginResponseDto.Token);

            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            claimsIdentity.AddClaim(
                new Claim(JwtRegisteredClaimNames.Email,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            claimsIdentity.AddClaim(
                new Claim(JwtRegisteredClaimNames.Sub,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            claimsIdentity.AddClaim(
                new Claim(JwtRegisteredClaimNames.Name,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            claimsIdentity.AddClaim(
                new Claim(ClaimTypes.Name,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));


            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }
    }
}
