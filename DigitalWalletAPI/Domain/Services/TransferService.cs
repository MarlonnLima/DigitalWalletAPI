using DigitalWalletAPI.Domain.Repositories;
using DigitalWalletAPI.DTOs;
using Npgsql;

namespace DigitalWalletAPI.Domain.Services
{
    public class TransferService
    {
        private readonly TransferRepository _transferRepository;
        public TransferService(TransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public void Transfer(TransferDTO model) 
        {
            if (model.SenderWalletId <= 0)
            {
                throw new ArgumentException("O id da carteira do solicitante é inválido");
            }

            if(model.ReceiverWalletId <= 0)
            {
                throw new ArgumentException("O id da carteira do destinatário é inválido");
            }

            _transferRepository.Transfer(model);
        }
    }
}
