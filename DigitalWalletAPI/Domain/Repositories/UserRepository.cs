using Dapper;
using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Infraestructure;

namespace DigitalWalletAPI.Domain.Repositories
{
    public class UserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public User FindById(int id)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    var user = conn.QueryFirstOrDefault<User>("SELECT * FROM USERS WHERE ID = @ID", new { ID = id });
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
