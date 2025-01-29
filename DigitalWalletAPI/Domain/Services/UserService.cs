using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Domain.Repositories;
using Npgsql;

namespace DigitalWalletAPI.Domain.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly WalletRepository _walletRepository;

        public UserService(UserRepository userRepository, WalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        public User GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O id é inválido");
            }

            User user = _userRepository.FindById(id);

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

            int id = _userRepository.Create(user);

            if(id <= 0)
            {
                throw new NpgsqlException("Não foi possível adicionar o usuário");
            }

            var rowsAffected = _walletRepository.Create(id);

            if (rowsAffected <= 0)
            {
                throw new NpgsqlException("Não foi possível adicionar a carteira do usuário");
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

        public bool Update(User user)
        {

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException("O nome não foi preenchido");
            }

            if (user.Id <= 0)
            {
                throw new ArgumentException("O id é invalido");
            }

            var result = _userRepository.Update(user);

            if(result == false)
            {
                throw new NpgsqlException("Não foi possível atualizar o usuário");
            }

            return result;
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O id é inválido");
            }

            int rowsAffected = _userRepository.Delete(id);

            if (rowsAffected <= 0)
            {
                throw new NpgsqlException("Não foi possível deletar o usuário");
            }
        }
    }
}
