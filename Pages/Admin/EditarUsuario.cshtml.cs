using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace ProFinQA.Pages.Admin
{
    [Authorize]
    public class EditarUsuarioModel : PageModel
    {
        private readonly BodegaContext _context;

        public EditarUsuarioModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditarUsuarioAdminRequest EditarUsuario { get; set; } = new();

        public string? MensajeExito { get; set; }
        public string? MensajeError { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (!User.HasClaim("IsAdmin", "True"))
            {
                return Forbid();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            EditarUsuario = new EditarUsuarioAdminRequest
            {
                Id = usuario.Id,
                Usuario = usuario.Usuario1,
                IsAdmin = usuario.IsAdmin,
                PrimerNombre = usuario.Empleado?.PrimerNombre ?? "",
                SegundoNombre = usuario.Empleado?.SegundoNombre ?? "",
                PrimerApellido = usuario.Empleado?.PrimerApellido ?? "",
                SegundoApellido = usuario.Empleado?.SegundoApellido ?? ""
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.HasClaim("IsAdmin", "True"))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == EditarUsuario.Id);

            if (usuario == null)
            {
                MensajeError = "Usuario no encontrado.";
                return Page();
            }

            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == EditarUsuario.Usuario && u.Id != EditarUsuario.Id);

            if (usuarioExistente != null)
            {
                ModelState.AddModelError("EditarUsuario.Usuario", "Este nombre de usuario ya está en uso.");
                return Page();
            }

            try
            {
                usuario.Usuario1 = EditarUsuario.Usuario;
                usuario.IsAdmin = EditarUsuario.IsAdmin;

                if (!string.IsNullOrEmpty(EditarUsuario.NuevaPassword))
                {
                    usuario.Password = BCrypt.Net.BCrypt.HashPassword(EditarUsuario.NuevaPassword);
                }

                if (usuario.Empleado != null)
                {
                    usuario.Empleado.PrimerNombre = EditarUsuario.PrimerNombre;
                    usuario.Empleado.SegundoNombre = EditarUsuario.SegundoNombre;
                    usuario.Empleado.PrimerApellido = EditarUsuario.PrimerApellido;
                    usuario.Empleado.SegundoApellido = EditarUsuario.SegundoApellido;
                }

                await _context.SaveChangesAsync();

                MensajeExito = "Usuario actualizado exitosamente.";
                EditarUsuario.NuevaPassword = "";
                EditarUsuario.ConfirmarPassword = "";

                return Page();
            }
            catch (Exception ex)
            {
                MensajeError = "Error al actualizar el usuario. Por favor, inténtelo de nuevo.";
                return Page();
            }
        }
    }

    public class EditarUsuarioAdminRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [MinLength(3, ErrorMessage = "El nombre de usuario debe tener al menos 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "El primer nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El primer nombre no puede tener más de 100 caracteres")]
        public string PrimerNombre { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "El segundo nombre no puede tener más de 100 caracteres")]
        public string SegundoNombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido es requerido")]
        [MaxLength(100, ErrorMessage = "El primer apellido no puede tener más de 100 caracteres")]
        public string PrimerApellido { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "El segundo apellido no puede tener más de 100 caracteres")]
        public string SegundoApellido { get; set; } = string.Empty;

        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede tener más de 100 caracteres")]
        public string? NuevaPassword { get; set; }

        [Compare("NuevaPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmarPassword { get; set; }
    }
}