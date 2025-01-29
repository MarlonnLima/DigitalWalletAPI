using Dapper;
using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Infraestructure;

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
    }
}
