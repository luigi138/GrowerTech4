using GrowerTech_MVC.Models;

namespace GrowerTech_MVC.Services
{
    public interface IUserService
    {
        (bool Success, string Message) SaveUser(User user);

        // 保留一个 Authenticate 方法，用于用户登录
        (bool Success, string Message) Authenticate(UserLoginModel model);
    }
}
