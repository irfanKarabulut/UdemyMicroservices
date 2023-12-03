﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Web.Models;
using UdemyMicroservices.Web.Services.Interfaces;

namespace UdemyMicroservices.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {
            if (!ModelState.IsValid)            
                return View();
            
            var response = await _identityService.SignIn(signInInput);

            if (!response.IsSuccesful)
            {
                response.Errors.ForEach(error => {                 
                    ModelState.AddModelError(String.Empty,error);                
                });

                return View();
            }

           return RedirectToAction(nameof(Index),"Home");           
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}