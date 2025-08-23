using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using BCrypt.Net;

namespace ProFinQA.Pages
{
    [Authorize]
    public class EditarPerfilModel : PageModel
    {
        private readonly BodegaContext _context;

        public EditarPerfilModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditarPerfilRequest EditarPerfil { get; set; } = new();

        public string? MensajeExito { get; set; }
        public string? MensajeError { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
            {
                return RedirectToPage("/Login");
            }

            EditarPerfil = new EditarPerfilRequest
            {
                Usuario = usuario.Usuario1,
                PrimerNombre = usuario.Empleado?.PrimerNombre ?? "",
                SegundoNombre = usuario.Empleado?.SegundoNombre ?? "",
                PrimerApellido = usuario.Empleado?.PrimerApellido ?? "",
                SegundoApellido = usuario.Empleado?.SegundoApellido ?? ""
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
            {
                MensajeError = "Usuario no encontrado.";
                return Page();
            }

            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == EditarPerfil.Usuario && u.Id != usuarioId);

            if (usuarioExistente != null)
            {
                ModelState.AddModelError("EditarPerfil.Usuario", "Este nombre de usuario ya está en uso.");
                return Page();
            }

            try
            {
                usuario.Usuario1 = EditarPerfil.Usuario;

                if (!string.IsNullOrEmpty(EditarPerfil.NuevaPassword))
                {
                    usuario.Password = BCrypt.Net.BCrypt.HashPassword(EditarPerfil.NuevaPassword);
                }

                if (usuario.Empleado != null)
                {
                    usuario.Empleado.PrimerNombre = EditarPerfil.PrimerNombre;
                    usuario.Empleado.SegundoNombre = EditarPerfil.SegundoNombre;
                    usuario.Empleado.PrimerApellido = EditarPerfil.PrimerApellido;
                    usuario.Empleado.SegundoApellido = EditarPerfil.SegundoApellido;
                }

                await _context.SaveChangesAsync();

                MensajeExito = "Perfil actualizado exitosamente.";
                EditarPerfil.NuevaPassword = "";
                EditarPerfil.ConfirmarPassword = "";

                return Page();
            }
            catch (Exception ex)
            {
                MensajeError = "Error al actualizar el perfil. Por favor, inténtelo de nuevo.";
                return Page();
            }
        }
    }

    public class EditarPerfilRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [MinLength(3, ErrorMessage = "El nombre de usuario debe tener al menos 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres")]
        public string Usuario { get; set; } = string.Empty;

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