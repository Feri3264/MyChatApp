using Chat.Domain.Models;

namespace Chat.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task AddAsync(UserModel user);
        public void Update(UserModel user);
        public Task DeleteAsync(UserModel user);
        public Task<UserModel> GetUserAsync(UserModel user);
        public Task<UserModel> GetByEmailOrUsernameAsync(string emailOrUsername);
        public Task<UserModel> GetByIdAsync(int userId);
        public Task<List<UserModel>> GetByTakeAsync(int take, int skip);
        public Task<int> GetCount();        
        public Task<bool> UserExistsAsync(int id);
        public Task<bool> EmailExistsAsync(string email);
        public Task<bool> UsernameExistsAsync(string username);
        public Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
        public Task SaveChangesAsync();
    }
}
