using Microsoft.AspNetCore.Mvc;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Services;


namespace GrowerTech_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Account/Login")]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Authenticate(model);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return View(model);
        }

        [HttpGet("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/Account/Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.SaveUser(user);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return View(user);
        }
    }
}
