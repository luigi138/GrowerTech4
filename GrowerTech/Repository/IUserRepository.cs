using GrowerTech_MVC.Models;

namespace GrowerTech_MVC.Repository
{
    public interface IUserRepository
    {
        bool Save(User user);
        User GetUserByEmail(string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public bool Save(User user)
        {
            _users.Add(user);
            return true;
        }

        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
    }
}
