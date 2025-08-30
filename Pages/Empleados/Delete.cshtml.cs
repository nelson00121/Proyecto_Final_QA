using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Empleados
{
    public class DeleteModel : PageModel
    {
        private readonly BodegaContext _context;

        public DeleteModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Empleado Empleado { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdUsuarioNavigation)
                .Include(e => e.Equipos)
                .Include(e => e.Reportes)
                .Include(e => e.Asignacions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                Empleado = empleado;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Equipos)
                .Include(e => e.Reportes)
                .Include(e => e.Asignacions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (empleado != null)
            {
                try
                {
                    // Liberar equipos asignados
                    foreach (var equipo in empleado.Equipos)
                    {
                        equipo.IdEmpleado = null;
                        equipo.Estado = "Disponible";
                    }

                    // Eliminar reportes asociados
                    _context.Reportes.RemoveRange(empleado.Reportes);

                    // Eliminar asignaciones
                    _context.Asignacions.RemoveRange(empleado.Asignacions);

                    // Eliminar el empleado
                    _context.Empleados.Remove(empleado);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Empleado {empleado.PrimerNombre} {empleado.PrimerApellido} eliminado exitosamente.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al eliminar el empleado: {ex.Message}";
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}