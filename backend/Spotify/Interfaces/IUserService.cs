using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify.Entities;
using Spotify.Models.User;

namespace Spotify.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        
        Task<User> GetByUsername(string username);
        
        Task<User> GetByEmail(string email);

        Task<CreateUserResponse> Create(CreateUserRequest req);

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest req);
        Task<UpdateUserResponse> Update(string id, UpdateUserRequest req);
    }
}