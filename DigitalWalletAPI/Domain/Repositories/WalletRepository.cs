using Dapper;
using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.Infraestructure;
using Npgsql;

namespace DigitalWalletAPI.Domain.Repositories
{
    public class WalletRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public WalletRepository(DbConnectionFactory dbConnectionFactory)
        {
            _connectionFactory = dbConnectionFactory;
        }

        public int Create(int userId)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    using (var transation = conn.BeginTransaction())
                    {
                        int rowsAffected = conn.Execute($"INSERT INTO WALLETS (userid) VALUES (@UserId)", new { UserId = userId });
                        transation.Commit();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return 0;
        }

        public Wallet FindByUserId(int id)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    var wallet = conn.QuerySingle<Wallet>("SELECT * FROM WALLETS WHERE USERID = @ID", new { ID = id });
                    return wallet;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return null;
        }

        public Wallet FindByWalletId(int id)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    var wallet = conn.QuerySingle<Wallet>("SELECT * FROM WALLETS WHERE ID = @ID", new { ID = id });
                    return wallet;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return null;
        }

        public int AddBalanceByWallet(AddBalanceDTO model)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    var wallet = FindByWalletId(model.Id);

                    if (wallet == null)
                    {
                        throw new NpgsqlException("Não foi possível encontrar nenhuma carteira");
                    }

                    int rowsAffected = conn.Execute("UPDATE WALLETS SET BALANCE = @BALANCE WHERE ID = @ID", new { ID = model.Id, BALANCE = (wallet.Balance + model.Amount) });
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return 0;
        }
    }
}
