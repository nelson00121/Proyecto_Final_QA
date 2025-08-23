using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProFinQA.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Cerrar sesión del usuario
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            // Limpiar cualquier cache local del navegador
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            
            // Redirigir inmediatamente al login
            return RedirectToPage("/Login");
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            // También manejar solicitudes POST si es necesario
            return await OnGetAsync();
        }
    }
}