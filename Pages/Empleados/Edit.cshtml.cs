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
    public class EditModel : PageModel
    {
        private readonly BodegaContext _context;

        public EditModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Empleado Empleado { get; set; } = default!;

        public int EquiposCount { get; set; }
        public int ReportesCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Equipos)
                .Include(e => e.Reportes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            Empleado = empleado;
            EquiposCount = empleado.Equipos.Count;
            ReportesCount = empleado.Reportes.Count;

            // Obtener usuarios disponibles (incluyendo el actual)
            var usuariosDisponibles = _context.Usuarios
                .Where(u => !_context.Empleados.Any(e => e.IdUsuario == u.Id) || u.Id == empleado.IdUsuario)
                .Select(u => new { u.Id, u.Usuario1 })
                .ToList();

            ViewData["IdUsuario"] = new SelectList(usuariosDisponibles, "Id", "Usuario1");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar información necesaria
                var empleadoOriginal = await _context.Empleados
                    .Include(e => e.Equipos)
                    .Include(e => e.Reportes)
                    .FirstOrDefaultAsync(m => m.Id == Empleado.Id);

                if (empleadoOriginal != null)
                {
                    EquiposCount = empleadoOriginal.Equipos.Count;
                    ReportesCount = empleadoOriginal.Reportes.Count;
                }

                var usuariosDisponibles = _context.Usuarios
                    .Where(u => !_context.Empleados.Any(e => e.IdUsuario == u.Id) || u.Id == Empleado.IdUsuario)
                    .Select(u => new { u.Id, u.Usuario1 })
                    .ToList();

                ViewData["IdUsuario"] = new SelectList(usuariosDisponibles, "Id", "Usuario1");
                return Page();
            }

            // Verificar que el usuario no esté ya asociado a otro empleado (excepto el actual)
            var usuarioExiste = await _context.Empleados
                .AnyAsync(e => e.IdUsuario == Empleado.IdUsuario && e.Id != Empleado.Id);

            if (usuarioExiste)
            {
                ModelState.AddModelError("Empleado.IdUsuario", "Este usuario ya está asociado a otro empleado.");
                
                var empleadoOriginal = await _context.Empleados
                    .Include(e => e.Equipos)
                    .Include(e => e.Reportes)
                    .FirstOrDefaultAsync(m => m.Id == Empleado.Id);

                if (empleadoOriginal != null)
                {
                    EquiposCount = empleadoOriginal.Equipos.Count;
                    ReportesCount = empleadoOriginal.Reportes.Count;
                }

                var usuariosDisponibles = _context.Usuarios
                    .Where(u => !_context.Empleados.Any(e => e.IdUsuario == u.Id) || u.Id == Empleado.IdUsuario)
                    .Select(u => new { u.Id, u.Usuario1 })
                    .ToList();

                ViewData["IdUsuario"] = new SelectList(usuariosDisponibles, "Id", "Usuario1");
                return Page();
            }

            _context.Attach(Empleado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Empleado {Empleado.PrimerNombre} {Empleado.PrimerApellido} actualizado exitosamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(Empleado.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}