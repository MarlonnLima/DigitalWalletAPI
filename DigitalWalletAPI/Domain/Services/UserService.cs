using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Domain.Repositories;
using Npgsql;

namespace DigitalWalletAPI.Domain.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetById(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("O id é inválido");
            }

            User user = _userRepository.FindById(Id);

            if (user == null)
            {
                throw new NpgsqlException("O usuário não foi encontrado");
            }
            else
            {
                return user;
            }
        }

        public void Create(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException("O nome não foi preenchido");
            }

            int rowsAffected = _userRepository.Create(user);

            if(rowsAffected <= 0)
            {
                throw new NpgsqlException("Não foi possível adicionar o usuário");
            }
        }

        public List<User> GetAll()
        {
            var users = _userRepository.GetAll();

            if(users == null)
            {
                throw new NpgsqlException("Não foi possível recuperar os usuários");
            }

            return users;
        }
    }
}
