using Dapper;
using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.Infraestructure;
using Npgsql;

namespace DigitalWalletAPI.Domain.Repositories
{
    public class TransferRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        private readonly WalletRepository _walletRepository;

        public TransferRepository(DbConnectionFactory connectionFactory, WalletRepository walletRepository)
        {
            _connectionFactory = connectionFactory;
            _walletRepository = walletRepository;
        }

        public void Transfer(TransferDTO model)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();

                    using (var transation = conn.BeginTransaction())
                    {
                        var senderWallet = _walletRepository.FindByWalletId(model.SenderWalletId);

                        if (senderWallet == null)
                        {
                            throw new NpgsqlException("Não foi possível encontrar a carteira do solicitante");
                        }

                        if (senderWallet.Balance < model.Amount)
                        {
                            throw new ArgumentException("Saldo insuficiente para esta transação");
                        }

                        var ReceiverWallet = _walletRepository.FindByWalletId(model.ReceiverWalletId);

                        if (ReceiverWallet == null)
                        {
                            throw new NpgsqlException("Não foi possível encontrar a carteira do destinatário");
                        }

                        conn.Execute("UPDATE WALLETS SET BALANCE = @BALANCE WHERE ID = @ID", new { ID = model.ReceiverWalletId, BALANCE = (ReceiverWallet.Balance + model.Amount) });
                        conn.Execute("UPDATE WALLETS SET BALANCE = @BALANCE WHERE ID = @ID", new { ID = model.SenderWalletId, BALANCE = (senderWallet.Balance - model.Amount) });
                        conn.Execute("INSERT INTO TRANSFERS (RECEIVERWALLETID, SENDERWALLETID, DATETIME, AMOUNT) VALUES (@RECEIVERWALLETID, @SENDERWALLETID, NOW(), @AMOUNT)", new
                        {
                            RECEIVERWALLETID = model.ReceiverWalletId,
                            SENDERWALLETID = model.SenderWalletId,
                            AMOUNT = model.Amount
                        });

                        transation.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                throw new NpgsqlException($"A transferência falhou, motivo: {ex.Message}");
            }
        }
    }
}
