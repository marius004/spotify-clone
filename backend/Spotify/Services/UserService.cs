using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spotify.Entities;
using Spotify.Interfaces;
using Spotify.Models;
using Spotify.Models.User;
using Spotify.Util;

namespace Spotify.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly JwtSettings _jwtSettings;

        public UserService(DatabaseSettings databaseSettings, IOptions<JwtSettings> jwtSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _jwtSettings = jwtSettings.Value;
            _users = database.GetCollection<User>(databaseSettings.UsersCollectionName);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var query = await _users.FindAsync(user => true);
            return await query.ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            var query = await _users.FindAsync(user => user.Id == id);
            return (await query.ToListAsync()).FirstOrDefault();
        }

        public async Task<User> GetByUsername(string username)
        {
            var query = await _users.FindAsync(user => user.Username == username);
            return (await query.ToListAsync()).FirstOrDefault(); 
        }

        public async Task<User> GetByEmail(string email)
        {
            var query = await _users.FindAsync(user => user.Email == email);
            return (await query.ToListAsync()).FirstOrDefault();
        }

        public async Task<CreateUserResponse> Create(CreateUserRequest req)
        {
            var query1 = await _users.FindAsync(usr => usr.Username == req.Username);
            if (query1.ToList().FirstOrDefault() != null)
                throw new Exception("Username already in use");

            var query2 = await _users.FindAsync(usr => usr.Email == req.Email);
            if (query2.ToList().FirstOrDefault() != null)
                throw new Exception("Email already in use");

            var user = new User(req);
            await _users.InsertOneAsync(user);

            var token = Jwt.GenerateToken(_jwtSettings, user);
            return new CreateUserResponse(user, token);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest req)
        {
            var userQuery = await _users.FindAsync(usr => usr.Username == req.Username);
            var user = userQuery.ToList().FirstOrDefault();
            
            if (user == null)
                return null;

            if (!PasswordHash.Verify(req.Password, user.Password))
                return null;

            var token = Jwt.GenerateToken(_jwtSettings, user);

            return new AuthenticateResponse(user, token);
        }
        
    }
}