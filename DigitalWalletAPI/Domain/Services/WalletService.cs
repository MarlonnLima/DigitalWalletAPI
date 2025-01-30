using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Domain.Repositories;
using DigitalWalletAPI.DTOs;
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
            if (userId <= 0)
            {
                throw new ArgumentException("Id do usuário inválido");
            }

            var wallet = _walletRepository.FindByUserId(userId);

            if (wallet == null)
            {
                throw new NpgsqlException("Não foi possível obter a carteira do usuário");
            }

            return wallet;
        }

        public void AddBalance(AddBalanceDTO model)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException("Id da carteira é inválido");
            }

            if(model.Amount <= 0)
            {
                throw new ArgumentException("A quantidade a ser adicionada é invalida");
            }

            int rowsAffected = _walletRepository.AddBalanceByWallet(model);

            if (rowsAffected <= 0)
            {
                throw new NpgsqlException("Não foi possível adicionar saldo a carteira do usuário");
            }
        }
    }
}
