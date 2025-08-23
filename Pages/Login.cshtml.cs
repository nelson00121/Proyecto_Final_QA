using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;
using ProFinQA.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using BCrypt.Net;

namespace ProFinQA.Pages
{
    public class LoginModel : PageModel
    {
        private readonly BodegaContext _context;
        private readonly IRoleService _roleService;

        public LoginModel(BodegaContext context, IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new();

        public string? ErrorMessage { get; set; }

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

            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Usuario1 == LoginRequest.Usuario);

            if (usuario == null || !VerifyPassword(LoginRequest.Password, usuario.Password))
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Usuario1),
                new Claim("IsAdmin", usuario.IsAdmin.ToString())
            };

            if (usuario.Empleado != null)
            {
                claims.Add(new Claim("EmpleadoId", usuario.Empleado.Id.ToString()));
                claims.Add(new Claim("NombreCompleto", 
                    $"{usuario.Empleado.PrimerNombre} {usuario.Empleado.PrimerApellido}"));
            }

            var userRoles = await _roleService.GetUserRoleNamesAsync(usuario.Id);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
            }
            catch
            {
                return inputPassword == storedPassword;
            }
        }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = string.Empty;
    }
}