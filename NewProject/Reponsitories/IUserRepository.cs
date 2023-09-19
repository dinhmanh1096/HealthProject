using NewProject.Models;

namespace NewProject.Reponsitories
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAllUserAsync();
        public Task<UserModel> GetUserAsync(int userID);
        public Task<int> AddUserAsync(RequestUserModel model);
        public Task UpdateUserAsync(int userID, UserModel model);
        public Task DeleteUserAsync(int userID);
    }
}
