using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Admin
{
    [Authorize]
    public class ListarUsuariosModel : PageModel
    {
        private readonly BodegaContext _context;

        public ListarUsuariosModel(BodegaContext context)
        {
            _context = context;
        }

        public List<UsuarioViewModel> Usuarios { get; set; } = new List<UsuarioViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.HasClaim("IsAdmin", "True"))
            {
                return Forbid();
            }

            Usuarios = await _context.Usuarios
                .Include(u => u.Empleado)
                .Select(u => new UsuarioViewModel
                {
                    Id = u.Id,
                    Usuario = u.Usuario1,
                    IsAdmin = u.IsAdmin,
                    NombreCompleto = u.Empleado != null 
                        ? $"{u.Empleado.PrimerNombre} {u.Empleado.PrimerApellido}"
                        : "Sin empleado asociado",
                    FechaCreacion = u.FechaCommit ?? DateTime.MinValue
                })
                .OrderBy(u => u.Usuario)
                .ToListAsync();

            return Page();
        }
    }

    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}