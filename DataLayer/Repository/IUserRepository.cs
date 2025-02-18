using DataLayer.Models;


namespace DataLayer.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task AddAsync(UserModel user);
        public void Update(UserModel user);
        public Task DeleteAsync(UserModel user);
        public Task<UserModel> GetUserAsync(UserModel user);
        public Task<UserModel> GetByUsernameAsync(string username);
        public Task<UserModel> GetByEmailAsync(string email);
        public Task<UserModel> GetByIdAsync(int userId);
        public Task<bool> UserExistsAsync(int id);
        public Task<IEnumerable<UserModel>> ContainsUsernameAsync(string username);
        public Task SaveChangesAsync();
    }
}
