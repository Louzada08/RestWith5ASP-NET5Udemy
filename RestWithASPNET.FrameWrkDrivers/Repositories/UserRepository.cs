using AutoMapper;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.ValueObjects;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using RestWithASPNET.FrameWrkDrivers.Repositories.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNET.FrameWrkDrivers.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MySQLContext context, IMapper mapper) : base(context, mapper) { }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(input: user.Password, SHA256.Create());
            var userResponse = _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
            return userResponse;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}