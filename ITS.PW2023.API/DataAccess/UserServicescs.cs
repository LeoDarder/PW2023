using InfluxDB.Client.Core.Exceptions;
using ITS.PW2023.API.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ITS.PW2023.API.DataAccess
{
    public class UserServices
    {
        private const string _query = "SELECT device, desiredLaps FROM users WHERE username = @Username AND password = @Password";

        private string connString;
        public UserServices(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("SQLDB");
        }
        public UserData GetUserData(string username, string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            using var conn = new SqlConnection(connString);
            var comm = new SqlCommand(_query, conn);
            comm.Parameters.AddWithValue("@username", username);
            comm.Parameters.AddWithValue("@password", hashedPassword);
            conn.Open();
            var reader = comm.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                return new UserData
                {
                    GuidDevice = reader.GetGuid(0).ToString(),
                    DesiredLaps = reader.GetInt32(1)
                };
            }
            else
            {
                throw new NotFoundException("Wrong User or Password!");
            }
        }
    }
}
