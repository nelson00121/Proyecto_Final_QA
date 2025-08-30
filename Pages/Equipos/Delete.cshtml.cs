using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Equipos
{
    public class DeleteModel : PageModel
    {
        private readonly BodegaContext _context;

        public DeleteModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipo Equipo { get; set; } = default!;

        public bool HasRelatedRecords { get; set; }
        public int AsignacionesCount { get; set; }
        public int ReportesCount { get; set; }
        public int MantenimientosCount { get; set; }
        public int VehiculosCount { get; set; }
        public int ElectronicosCount { get; set; }
        public int MobiliariosCount { get; set; }
        public int HerramientasCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .Include(e => e.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            Equipo = equipo;

            // Contar registros relacionados
            AsignacionesCount = await _context.Asignacions.CountAsync(a => a.IdEquipo == id);
            ReportesCount = await _context.Reportes.CountAsync(r => r.IdEquipo == id);
            MantenimientosCount = await _context.Mantenimientos.CountAsync(m => m.IdEquipo == id);
            VehiculosCount = await _context.Vehiculos.CountAsync(v => v.IdEquipo == id);
            ElectronicosCount = await _context.Electronicos.CountAsync(e => e.IdEquipo == id);
            MobiliariosCount = await _context.Mobiliarios.CountAsync(m => m.IdEquipo == id);
            HerramientasCount = await _context.Herramienta.CountAsync(h => h.IdEquipo == id);

            HasRelatedRecords = AsignacionesCount > 0 || ReportesCount > 0 || MantenimientosCount > 0 ||
                               VehiculosCount > 0 || ElectronicosCount > 0 || MobiliariosCount > 0 || HerramientasCount > 0;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.Asignacions)
                .Include(e => e.Reportes)
                    .ThenInclude(r => r.Imagens)
                .Include(e => e.Mantenimientos)
                .Include(e => e.Vehiculos)
                .Include(e => e.Electronicos)
                .Include(e => e.Mobiliarios)
                .Include(e => e.Herramienta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipo != null)
            {
                // Eliminar registros relacionados en el orden correcto
                // Primero las imágenes de reportes
                foreach (var reporte in equipo.Reportes)
                {
                    _context.Imagens.RemoveRange(reporte.Imagens);
                }

                // Luego los registros que dependen del equipo
                _context.Asignacions.RemoveRange(equipo.Asignacions);
                _context.Reportes.RemoveRange(equipo.Reportes);
                _context.Mantenimientos.RemoveRange(equipo.Mantenimientos);
                _context.Vehiculos.RemoveRange(equipo.Vehiculos);
                _context.Electronicos.RemoveRange(equipo.Electronicos);
                _context.Mobiliarios.RemoveRange(equipo.Mobiliarios);
                _context.Herramienta.RemoveRange(equipo.Herramienta);

                // Finalmente el equipo
                _context.Equipos.Remove(equipo);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { successMessage = "Equipo eliminado exitosamente." });
        }
    }
}