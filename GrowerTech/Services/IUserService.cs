    using GrowerTech_MVC.Models;
    using GrowerTech_MVC.Services;

    namespace GrowerTech_MVC.Services
    {
        public interface IUserService
        {
            (bool Success, string Message) SaveUser(User user);
        }
    }
