using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class RegisterModel : PageModel
    {
        private readonly BodegaContext _context;

        public RegisterModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterRequest RegisterRequest { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Verificar si el usuario ya existe
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == RegisterRequest.Usuario);

            if (existingUser != null)
            {
                ModelState.AddModelError(nameof(RegisterRequest.Usuario), "El usuario ya existe.");
                return Page();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear el usuario
                var usuario = new Usuario
                {
                    Usuario1 = RegisterRequest.Usuario,
                    Password = BCrypt.Net.BCrypt.HashPassword(RegisterRequest.Password),
                    IsAdmin = RegisterRequest.IsAdmin,
                    FechaCommit = DateTime.Now
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Crear el empleado asociado
                var empleado = new Empleado
                {
                    PrimerNombre = RegisterRequest.PrimerNombre,
                    SegundoNombre = RegisterRequest.SegundoNombre,
                    PrimerApellido = RegisterRequest.PrimerApellido,
                    SegundoApellido = RegisterRequest.SegundoApellido,
                    IdUsuario = usuario.Id,
                    Estado = "activo"
                };

                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Log de movimiento
                var logMovimiento = new LogMovimiento
                {
                    IdUsuario = usuario.Id,
                    Accion = "Registro de usuario",
                    Descripcion = $"Usuario {usuario.Usuario1} se registró en el sistema",
                    FechaCommit = DateTime.Now
                };
                
                _context.LogMovimientos.Add(logMovimiento);
                await _context.SaveChangesAsync();

                // Auto-login después del registro
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Usuario1),
                    new Claim("IsAdmin", usuario.IsAdmin.ToString()),
                    new Claim("EmpleadoId", empleado.Id.ToString()),
                    new Claim("NombreCompleto", $"{empleado.PrimerNombre} {empleado.PrimerApellido}")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Error al crear el usuario. Inténtalo de nuevo.");
                return Page();
            }
        }
    }

    public class RegisterRequest
    {
        [Required(ErrorMessage = "El primer nombre es requerido")]
        [StringLength(50, ErrorMessage = "El primer nombre no puede exceder 50 caracteres")]
        public string PrimerNombre { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "El segundo nombre no puede exceder 50 caracteres")]
        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido")]
        [StringLength(50, ErrorMessage = "El primer apellido no puede exceder 50 caracteres")]
        public string PrimerApellido { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "El segundo apellido no puede exceder 50 caracteres")]
        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "El usuario solo puede contener letras, números y guiones bajos")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmar la contraseña es requerido")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;
    }
}