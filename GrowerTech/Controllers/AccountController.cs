using Microsoft.AspNetCore.Mvc;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Services;
using System.Threading.Tasks;

namespace GrowerTech_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
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
