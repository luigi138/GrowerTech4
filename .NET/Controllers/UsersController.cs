using Microsoft.AspNetCore.Mvc;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Services;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Register(User user)
    {
        var result = _userService.SaveUser(user);
        if (result)
        {
            return Ok("Usuário registrado com sucesso.");
        }
        return BadRequest("Dados do usuário inválidos.");
    }
}

