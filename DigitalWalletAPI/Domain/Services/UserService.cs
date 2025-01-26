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
    }
}
