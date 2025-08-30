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
    public class EquiposModel : PageModel
    {
        private readonly BodegaContext _context;

        public EquiposModel(BodegaContext context)
        {
            _context = context;
        }

        public Empleado? Empleado { get; set; }
        public List<Asignacion>? AsignacionesActivas { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdUsuarioNavigation)
                .Include(e => e.Equipos)
                    .ThenInclude(eq => eq.IdMarcaNavigation)
                .Include(e => e.Asignacions)
                    .ThenInclude(a => a.IdEquipoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            Empleado = empleado;

            // Obtener asignaciones activas para mostrar opciones de devolución
            AsignacionesActivas = await _context.Asignacions
                .Where(a => a.IdEmpleado == id && a.FechaFin == null)
                .ToListAsync();

            return Page();
        }
    }
}