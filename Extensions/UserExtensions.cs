using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Extensions;

public static class UserExtensions
{
    public static async Task<List<string>> GetUserRolesAsync(this ClaimsPrincipal user, BodegaContext context)
    {
        if (!user.Identity?.IsAuthenticated ?? true)
            return new List<string>();

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            return new List<string>();

        var roles = await context.UsuarioRoles
            .Where(ur => ur.UsuarioId == userIdInt && ur.Activo)
            .Include(ur => ur.Role)
            .Where(ur => ur.Role.Activo)
            .Select(ur => ur.Role.Nombre)
            .ToListAsync();

        return roles;
    }

    public static async Task<bool> HasRoleAsync(this ClaimsPrincipal user, BodegaContext context, string roleName)
    {
        var roles = await user.GetUserRolesAsync(context);
        return roles.Contains(roleName);
    }

    public static async Task<bool> HasAnyRoleAsync(this ClaimsPrincipal user, BodegaContext context, params string[] roleNames)
    {
        var roles = await user.GetUserRolesAsync(context);
        return roles.Any(r => roleNames.Contains(r));
    }

    public static async Task<bool> IsSuperAdminAsync(this ClaimsPrincipal user, BodegaContext context)
    {
        return await user.HasRoleAsync(context, "SuperAdmin");
    }

    public static async Task<bool> CanManageFinancesAsync(this ClaimsPrincipal user, BodegaContext context)
    {
        return await user.HasAnyRoleAsync(context, "SuperAdmin", "AdminFinanciero");
    }

    public static async Task<bool> CanManageAreaAsync(this ClaimsPrincipal user, BodegaContext context)
    {
        return await user.HasAnyRoleAsync(context, "SuperAdmin", "AdminArea");
    }

    public static async Task<bool> CanViewReportsAsync(this ClaimsPrincipal user, BodegaContext context)
    {
        return await user.HasAnyRoleAsync(context, "SuperAdmin", "AdminFinanciero", "AdminArea", "Auditor");
    }

    public static async Task<bool> CanOperateAsync(this ClaimsPrincipal user, BodegaContext context)
    {
        return await user.HasAnyRoleAsync(context, "SuperAdmin", "AdminArea", "UsuarioOperativo");
    }
}