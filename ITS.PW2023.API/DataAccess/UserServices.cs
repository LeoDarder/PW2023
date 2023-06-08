﻿using InfluxDB.Client.Core.Exceptions;
using ITS.PW2023.API.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ITS.PW2023.API.DataAccess
{
    public class UserServices
    {
        private const string _query = $"SELECT ud.*  FROM [dbo].[UsersDevices] ud  JOIN users u ON ud.Username = u.username  WHERE u.username = '@username' AND  u.password = '@password'";

        private string connString;
        public UserServices(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("SQLDB");
        }
        public List<UserData> GetUserData(string username, string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            string query = _query.Replace("@username", username).Replace("@password", hashedPassword);

            using var conn = new SqlConnection(connString);
            var comm = new SqlCommand(query, conn);
            //comm.Parameters.AddWithValue("username", username);
            //comm.Parameters.AddWithValue("password", hashedPassword);
            conn.Open();
            var reader = comm.ExecuteReader();
            if(reader.HasRows)
            {
                List<UserData> users = new();
                while (reader.Read())
                {
                    users.Add(new UserData
                    {
                        GuidDevice = reader.GetGuid(0).ToString(),
                        Username = reader.GetString(1),
                        DesiredLaps = reader.GetInt32(2),
                        DeviceName = reader.GetString(3)
                    });
                }
                return users;
            }
            else
            {
                throw new NotFoundException("Wrong User or Password!");
            }
        }
    }
}
