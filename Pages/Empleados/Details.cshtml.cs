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
    public class DetailsModel : PageModel
    {
        private readonly BodegaContext _context;

        public DetailsModel(BodegaContext context)
        {
            _context = context;
        }

        public Empleado? Empleado { get; set; } = default!;

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
                .Include(e => e.Reportes)
                    .ThenInclude(r => r.IdEquipoNavigation)
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
    }
}