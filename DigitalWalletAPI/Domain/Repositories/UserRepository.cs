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
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return null;
        }

        public int Create(User user)
        {
            try
            {
                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    int rowsAffected = conn.Execute($"INSERT INTO USERS (Name) VALUES (@NAME)", new { NAME = user.Name });
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return 0;
        }

        public List<User> GetAll()
        {
            try
            {
                using(var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    List<User> users = conn.Query<User>("SELECT * FROM USERS").ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return new List<User>();
        }

        public bool Update(User user)
        {
            try
            {
                using( var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    int rowsAffected = conn.Execute("UPDATE USERS SET NAME = @NAME WHERE ID = @ID", new { ID = user.Id, NAME = user.Name });
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return false;
        }
    }
}
