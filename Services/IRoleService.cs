using ProFinQA.Models;

namespace ProFinQA.Services;

public interface IRoleService
{
    Task<List<Role>> GetAllRolesAsync();
    Task<List<Role>> GetActiveRolesAsync();
    Task<Role?> GetRoleByIdAsync(int roleId);
    Task<Role?> GetRoleByNameAsync(string roleName);
    
    Task<List<UsuarioRole>> GetUserRolesAsync(int userId);
    Task<bool> UserHasRoleAsync(int userId, string roleName);
    Task<bool> UserHasAnyRoleAsync(int userId, params string[] roleNames);
    
    Task<bool> AssignRoleToUserAsync(int userId, int roleId);
    Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
    Task<bool> UpdateUserRolesAsync(int userId, List<int> roleIds);
    
    Task<List<string>> GetUserRoleNamesAsync(int userId);
}