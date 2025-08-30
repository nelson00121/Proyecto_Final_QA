using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Empleados
{
    public class CreateModel : PageModel
    {
        private readonly BodegaContext _context;

        public CreateModel(BodegaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Obtener usuarios que no están asociados a ningún empleado
            var usuariosDisponibles = _context.Usuarios
                .Where(u => !_context.Empleados.Any(e => e.IdUsuario == u.Id))
                .Select(u => new { u.Id, u.Usuario1 })
                .ToList();

            ViewData["IdUsuario"] = new SelectList(usuariosDisponibles, "Id", "Usuario1");
            return Page();
        }

        [BindProperty]
        public Empleado Empleado { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar los usuarios disponibles si hay error
                var usuariosDisponibles = _context.Usuarios
                    .Where(u => !_context.Empleados.Any(e => e.IdUsuario == u.Id))
                    .Select(u => new { u.Id, u.Usuario1 })
                    .ToList();

                ViewData["IdUsuario"] = new SelectList(usuariosDisponibles, "Id", "Usuario1");
                return Page();
            }

            // Verificar que el usuario no esté ya asociado a otro empleado
            var usuarioExiste = await _context.Empleados
                .AnyAsync(e => e.IdUsuario == Empleado.IdUsuario);

            if (usuarioExiste)
            {
                ModelState.AddModelError("Empleado.IdUsuario", "Este usuario ya está asociado a otro empleado.");
                
                var usuariosDisponibles = _context.Usuarios
                    .Where(u => !_context.Empleados.Any(e => e.IdUsuario == u.Id))
                    .Select(u => new { u.Id, u.Usuario1 })
                    .ToList();

                ViewData["IdUsuario"] = new SelectList(usuariosDisponibles, "Id", "Usuario1");
                return Page();
            }

            // Establecer valores por defecto si no están asignados
            if (string.IsNullOrEmpty(Empleado.Estado))
            {
                Empleado.Estado = "activo";
            }

            _context.Empleados.Add(Empleado);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Empleado {Empleado.PrimerNombre} {Empleado.PrimerApellido} creado exitosamente.";
            return RedirectToPage("./Index");
        }
    }
}