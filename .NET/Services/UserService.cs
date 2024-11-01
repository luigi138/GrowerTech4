using GrowerTech_MVC.Models;
using GrowerTech_MVC.Repository;

namespace GrowerTech_MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public bool RegisterUser(User user)
        {
     
            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                return false;
            }

            return SaveUser(user);
        }

        public bool SaveUser(User user)
        {
 
            return _userRepository.Save(user);
        }
    }
}
