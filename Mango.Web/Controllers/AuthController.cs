using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Mango.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
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

        public IActionResult Logout()
        {
            return View();
        }
    }
}
