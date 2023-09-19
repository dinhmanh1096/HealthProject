using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewProject.Data;
using NewProject.Models;
using NewProject.Models.Authentication;

namespace NewProject.Reponsitories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddUserAsync(RequestUserModel model)
        {        
                var newUser = _mapper.Map<User>(model);
                _context.Users!.Add(newUser);
                await _context.SaveChangesAsync();
                return newUser.UserID;

        }

        public async Task DeleteUserAsync(int userID)
        {
            var deleteUser = _context.Users!.SingleOrDefault(u => u.UserID == userID);
            if (deleteUser != null)
            {
                _context.Users!.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<UserModel>> GetAllUserAsync()
        {
            var users = await _context.Users!.ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetUserAsync(int userID)
        {
            var user = await _context.Users!.FindAsync(userID);
            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateUserAsync(int userID, UserModel model)
        {
            if (userID == model.UserID)
            {
                var updateUser = _mapper.Map<User>(model);
                _context.Users!.Update(updateUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
