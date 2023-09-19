using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewProject.Data;
using NewProject.Models;

namespace NewProject.Reponsitories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(ApplicationDbContext context,IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddRoleAsync(RequestRoleModel model)
        {
            var newRole=_mapper.Map<Role>(model);
            _context.Roles!.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole.RoleID;
        }
        public async Task DeleteRoleAsync(int roleId)
        {
            var deleteRole = _context.Roles!.SingleOrDefault(r => r.RoleID == roleId);
            if (deleteRole != null)
            {
                _context.Roles!.Remove(deleteRole);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<RoleModel>> GetAllRoleAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<List<RoleModel>>(roles);
        }

        public async Task<RoleModel> GetRoleAsync(int roleId)
        {
            var role = await _context.Roles!.FindAsync(roleId);
            return _mapper.Map<RoleModel>(role);
        }

        public async Task UpdateRoleAsync(int roleId, RoleModel model)
        {
            if (roleId == model.RoleID)
            {
                var updateRole = _mapper.Map<Role>(model);
                _context.Roles!.Update(updateRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
