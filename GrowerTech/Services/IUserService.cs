using GrowerTech_MVC.Models;

namespace GrowerTech_MVC.Services
{
    public interface IUserService
    {
        (bool Success, string Message) SaveUser(User user);
    }
}
