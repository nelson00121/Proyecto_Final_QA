using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;
using ProFinQA.Services;

namespace ProFinQA.Pages.Admin;

[Authorize(Policy = "SuperAdminOnly")]
public class GestionRolesModel : PageModel
{
    private readonly BodegaContext _context;
    private readonly IRoleService _roleService;

    public GestionRolesModel(BodegaContext context, IRoleService roleService)
    {
        _context = context;
        _roleService = roleService;
    }

    [BindProperty]
    public int SelectedUserId { get; set; }

    [BindProperty]
    public List<int> SelectedRoleIds { get; set; } = new();

    public List<Usuario> Users { get; set; } = new();
    public List<Role> AllRoles { get; set; } = new();
    public List<int> UserRoleIds { get; set; } = new();
    public string? Message { get; set; }

    public async Task OnGetAsync()
    {
        await LoadDataAsync();
    }

    public async Task<IActionResult> OnPostSelectUserAsync()
    {
        await LoadDataAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateRolesAsync()
    {
        if (SelectedUserId <= 0)
        {
            TempData["ErrorMessage"] = "Debe seleccionar un usuario";
            return RedirectToPage();
        }

        try
        {
            var success = await _roleService.UpdateUserRolesAsync(SelectedUserId, SelectedRoleIds);
            
            if (success)
            {
                Message = "Roles actualizados correctamente";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar los roles";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error: {ex.Message}";
        }

        await LoadDataAsync();
        return Page();
    }

    private async Task LoadDataAsync()
    {
        Users = await _context.Usuarios
            .Include(u => u.Empleado)
            .OrderBy(u => u.Usuario1)
            .ToListAsync();

        AllRoles = await _roleService.GetActiveRolesAsync();

        if (SelectedUserId > 0)
        {
            var userRoles = await _roleService.GetUserRolesAsync(SelectedUserId);
            UserRoleIds = userRoles.Select(ur => ur.RoleId).ToList();
        }
    }
}