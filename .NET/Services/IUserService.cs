using GrowerTech_MVC.Models;

namespace GrowerTech_MVC.Services
{
    public interface IUserService
    {
        bool RegisterUser(User user);
        bool SaveUser(User user);
    }
}
