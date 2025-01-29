using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Domain.Repositories;
using Npgsql;

namespace DigitalWalletAPI.Domain.Services
{
    public class WalletService
    {
        private readonly WalletRepository _walletRepository;

        public WalletService(WalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public Wallet GetOne(int userId)
        {
            if(userId <= 0)
            {
                throw new ArgumentException("Id do usuário inválido");
            }

            var wallet = _walletRepository.FindByUserId(userId);
            
            if(wallet == null)
            {
                throw new NpgsqlException("Não foi possível obter a carteira do usuário");
            }

            return wallet;
        }
    }
}
