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
    }
}
