using GrowerTech_MVC.Models;

namespace GrowerTech_MVC.Services
{
    public class UserService : IUserService
    {
        public (bool Success, string Message) SaveUser(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Name))
            {
                return (false, "Dados do usuário inválidos.");
            }

            return (true, "Usuário salvo com sucesso.");
        }

        public (bool Success, string Message) Authenticate(UserLoginModel model)
        {
            // 示例验证逻辑：假设用户名和密码都是 "admin" 则登录成功
            if (model.Username == "admin" && model.Password == "admin")
            {
                return (true, "Login bem-sucedido.");
            }

            return (false, "Nome de usuário ou senha inválidos.");
        }
    }
}
