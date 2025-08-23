using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Services;

public class RoleService : IRoleService
{
    private readonly BodegaContext _context;

    public RoleService(BodegaContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<List<Role>> GetActiveRolesAsync()
    {
        return await _context.Roles.Where(r => r.Activo).ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
    }

    public async Task<Role?> GetRoleByNameAsync(string roleName)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == roleName && r.Activo);
    }

    public async Task<List<UsuarioRole>> GetUserRolesAsync(int userId)
    {
        return await _context.UsuarioRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UsuarioId == userId && ur.Activo && ur.Role.Activo)
            .ToListAsync();
    }

    public async Task<bool> UserHasRoleAsync(int userId, string roleName)
    {
        return await _context.UsuarioRoles
            .Include(ur => ur.Role)
            .AnyAsync(ur => ur.UsuarioId == userId 
                         && ur.Activo 
                         && ur.Role.Nombre == roleName 
                         && ur.Role.Activo);
    }

    public async Task<bool> UserHasAnyRoleAsync(int userId, params string[] roleNames)
    {
        return await _context.UsuarioRoles
            .Include(ur => ur.Role)
            .AnyAsync(ur => ur.UsuarioId == userId 
                         && ur.Activo 
                         && roleNames.Contains(ur.Role.Nombre) 
                         && ur.Role.Activo);
    }

    public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
    {
        var existingAssignment = await _context.UsuarioRoles
            .FirstOrDefaultAsync(ur => ur.UsuarioId == userId && ur.RoleId == roleId);

        if (existingAssignment != null)
        {
            if (!existingAssignment.Activo)
            {
                existingAssignment.Activo = true;
                existingAssignment.FechaAsignacion = DateTime.Now;
            }
        }
        else
        {
            var usuarioRole = new UsuarioRole
            {
                UsuarioId = userId,
                RoleId = roleId,
                Activo = true,
                FechaAsignacion = DateTime.Now
            };

            _context.UsuarioRoles.Add(usuarioRole);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
    {
        var usuarioRole = await _context.UsuarioRoles
            .FirstOrDefaultAsync(ur => ur.UsuarioId == userId && ur.RoleId == roleId && ur.Activo);

        if (usuarioRole != null)
        {
            usuarioRole.Activo = false;
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> UpdateUserRolesAsync(int userId, List<int> roleIds)
    {
        var currentRoles = await _context.UsuarioRoles
            .Where(ur => ur.UsuarioId == userId && ur.Activo)
            .ToListAsync();

        foreach (var currentRole in currentRoles)
        {
            if (!roleIds.Contains(currentRole.RoleId))
            {
                currentRole.Activo = false;
            }
        }

        foreach (var roleId in roleIds)
        {
            var existingRole = currentRoles.FirstOrDefault(cr => cr.RoleId == roleId);
            if (existingRole == null)
            {
                _context.UsuarioRoles.Add(new UsuarioRole
                {
                    UsuarioId = userId,
                    RoleId = roleId,
                    Activo = true,
                    FechaAsignacion = DateTime.Now
                });
            }
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<string>> GetUserRoleNamesAsync(int userId)
    {
        return await _context.UsuarioRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UsuarioId == userId && ur.Activo && ur.Role.Activo)
            .Select(ur => ur.Role.Nombre)
            .ToListAsync();
    }
}