using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Services;
using System.Linq;
using System.Threading.Tasks;

namespace GrowerTech_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/signin-google")]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
                return BadRequest(); 

            var claims = authenticateResult.Principal.Identities
                              .FirstOrDefault()?.Claims
                              .Select(claim => new
                              {
                                  claim.Type,
                                  claim.Value
                              });

            var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var name = claims.FirstOrDefault(c => c.Type == "name")?.Value;

            var user = new User
            {
                Email = email,
                Name = name
            };

            _userService.SaveUser(user);

            return RedirectToAction("Index", "Home");
        }
    }
}
