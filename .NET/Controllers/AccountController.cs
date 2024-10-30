
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Threading.Tasks;

namespace GrowerTech_MVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("/signin-google")]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
                return BadRequest(); // 认证失败

            // 处理用户信息，进行登录等操作
            var claims = authenticateResult.Principal.Identities
                              .FirstOrDefault()?.Claims
                              .Select(claim => new
                              {
                                  claim.Type,
                                  claim.Value
                              });

            return RedirectToAction("Index", "Home");
        }
    }
}
    