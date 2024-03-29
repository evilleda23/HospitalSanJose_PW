﻿
using HospitalSanJoseModel.DTO.Auth;
using HospitalSanJose.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSanJose.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserRolesService _userRolesService;
        private readonly AuthService _authRolesService;
        public AuthController( AuthService authRolesService, UserRolesService userRolesService)
        {

            _authRolesService = authRolesService;
             _userRolesService = userRolesService;
        }

        public IActionResult Login()
        {

            return View("Login");
        }

        public IActionResult Register()
        {
            return View("Register");
        }
        [Authorize]
        [Route("dashboard")] 
        public async Task<IActionResult> Dashboard()
        {
          
            return View("Dashboard");
        }



        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            var response = await _authRolesService.Login(login);
            if (response == null)
                return View();
            if(response.Response!=null && response.Response.ShowWarning)
            {
                login.Response = response.Response;
                return View(login);
            }
            HttpContext.Response.Cookies.Append("loggedIn", "true");
            HttpContext.Session.SetString("Username", login.Username);
            HttpContext.Session.SetInt32("UserId", (int)response.UserId!);
            HttpContext.Session.SetString("Token", response.Token);
            var roles = response.Roles ?? "";
            
            if (roles == "")
                return RedirectToAction("logout");
            HttpContext.Session.SetString("Roles", roles);
            var claims = new List<Claim>
            {
                    new Claim("Username", login.Username),
                    new Claim("TokenAPI", response.Token)
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index", "Dashboard");

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            var response = await _authRolesService.Register(register);
            if (response.Response != null && response.Response.ShowWarning)
                return View(response);
            HttpContext.Response.Cookies.Append("loggedIn", "true");
            HttpContext.Session.SetString("Username", register.Username);
            HttpContext.Session.SetInt32("UserId", (int)response.UserId!);
            return RedirectToAction("Auth", "Login");

        }

        private async Task<bool> SaveRolesInSession()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
           
            var userRoles = await _userRolesService.GetRolesByUserId((int)userid);
            if(userRoles == null || userRoles.Roles == null  || userRoles.Roles == "")
            {
                return false;
            }
            var roles = userRoles.Roles ?? "";
            HttpContext.Session.SetString("Roles", roles);
            return true;
        }
 
    }
}

