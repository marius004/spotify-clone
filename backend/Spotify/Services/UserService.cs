using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Spotify.Attributes;
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

        [Authorize]
        public async Task<UpdateUserResponse> Update(string id, UpdateUserRequest req)
        {
            var res = new UpdateUserResponse();
            var filter = Builders<User>.Filter.Eq("Id", id);
            
            var tasks = new List<Task>();

            if (req.Email != null)
            {
                var query = await _users.FindAsync(usr => usr.Email == req.Email);
                
                if (query.ToList().FirstOrDefault() != null)
                {
                    res.Errors.Add("Email already in use");
                }
                else
                {
                    var update = Builders<User>.Update.Set("Email", req.Email);
                    tasks.Add(_users.UpdateOneAsync(filter, update));
                }
            }

            if (req.Password != null)
            {
                if (req.Password.Length < 8)
                {
                    res.Errors.Add("Password too weak");
                }
                else
                {
                    var hashedPassword = PasswordHash.Hash(req.Password);
                    var update = Builders<User>.Update.Set("Password", hashedPassword);
                    tasks.Add(_users.UpdateOneAsync(filter, update));
                }
            }

            if (req.Username != null)
            {
                var query = await _users.FindAsync(usr => usr.Username == req.Username);
                if (query.ToList().FirstOrDefault() != null)
                {
                    res.Errors.Add("Username already taken");
                }
                else
                {
                    var update = Builders<User>.Update.Set("Username", req.Username);
                    tasks.Add(_users.UpdateOneAsync(filter, update));
                }
            }

            Console.WriteLine("in here");
            Console.WriteLine(req.ToJson());
            if (req.ArtistsLiked != null)
            {
                Console.WriteLine("In here");
                var user = (await _users.FindAsync(usr => usr.Id == id)).FirstOrDefault();

                if (user.ArtistsLiked == null)
                    user.ArtistsLiked = new List<string>();
                
                foreach (var artist in req.ArtistsLiked)
                {
                    if (!user.ArtistsLiked.Contains(artist))
                        user.ArtistsLiked.Add(artist);
                }

                var update = Builders<User>.Update.Set("ArtistsLiked", user.ArtistsLiked);
                tasks.Add(_users.UpdateOneAsync(filter, update));
            }

            if (req.ArtistsUnliked != null)
            {
                var user = (await _users.FindAsync(usr => usr.Id == id)).FirstOrDefault();

                if (user.ArtistsLiked != null)
                {
                    foreach (var unlikedArtist in req.ArtistsUnliked)
                    {
                        if (user.ArtistsLiked.Contains(unlikedArtist))
                            user.ArtistsLiked.Remove(unlikedArtist);
                    }
                }
                
                var update = Builders<User>.Update.Set("ArtistsLiked", user.ArtistsLiked);
                tasks.Add(_users.UpdateOneAsync(filter, update));
            }
           
            if (req.SongsLiked != null)
            {
                var user = (await _users.FindAsync(usr => usr.Id == id)).FirstOrDefault();

                if (user.SongsLiked == null)
                    user.SongsLiked = new List<string>();
                
                foreach (var song in req.SongsLiked)
                {
                    if (!user.SongsLiked.Contains(song))
                        user.SongsLiked.Add(song);
                }

                var update = Builders<User>.Update.Set("SongsLiked", user.SongsLiked);
                tasks.Add(_users.UpdateOneAsync(filter, update));
            }

            if (req.SongsUnliked != null)
            {
                var user = (await _users.FindAsync(usr => usr.Id == id)).FirstOrDefault();

                if (user.SongsLiked != null)
                {
                    foreach (var song in req.SongsUnliked)
                    {
                        if (user.SongsLiked.Contains(song))
                            user.SongsLiked.Remove(song);
                    }         
                }
               
                var update = Builders<User>.Update.Set("SongsLiked", user.SongsLiked);
                tasks.Add(_users.UpdateOneAsync(filter, update));
            }

            Task.WaitAll(tasks.ToArray());
            
            return res;
        }

        public async Task Delete(string id)
        {
            await _users.DeleteOneAsync(usr => usr.Id == id);
        }
    }
}