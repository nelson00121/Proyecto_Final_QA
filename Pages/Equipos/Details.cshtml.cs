using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Equipos
{
    public class DetailsModel : PageModel
    {
        private readonly BodegaContext _context;

        public DetailsModel(BodegaContext context)
        {
            _context = context;
        }

        public Equipo Equipo { get; set; } = default!;

        // Información específica por tipo
        public Vehiculo? Vehiculo { get; set; }
        public Electronico? Electronico { get; set; }
        public Mobiliario? Mobiliario { get; set; }
        public Herramientum? Herramienta { get; set; }

        // Contadores de registros relacionados
        public int AsignacionesCount { get; set; }
        public int ReportesCount { get; set; }
        public int MantenimientosCount { get; set; }
        public int TotalRegistros { get; set; }

        // Últimos reportes
        public IList<Reporte> UltimosReportes { get; set; } = new List<Reporte>();

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

            // Cargar información específica por tipo de equipo
            Vehiculo = await _context.Vehiculos
                .FirstOrDefaultAsync(v => v.IdEquipo == id);

            Electronico = await _context.Electronicos
                .FirstOrDefaultAsync(e => e.IdEquipo == id);

            Mobiliario = await _context.Mobiliarios
                .FirstOrDefaultAsync(m => m.IdEquipo == id);

            Herramienta = await _context.Herramienta
                .FirstOrDefaultAsync(h => h.IdEquipo == id);

            // Contar registros relacionados
            AsignacionesCount = await _context.Asignacions.CountAsync(a => a.IdEquipo == id);
            ReportesCount = await _context.Reportes.CountAsync(r => r.IdEquipo == id);
            MantenimientosCount = await _context.Mantenimientos.CountAsync(m => m.IdEquipo == id);
            
            var vehiculosCount = await _context.Vehiculos.CountAsync(v => v.IdEquipo == id);
            var electronicosCount = await _context.Electronicos.CountAsync(e => e.IdEquipo == id);
            var mobiliariosCount = await _context.Mobiliarios.CountAsync(m => m.IdEquipo == id);
            var herramientasCount = await _context.Herramienta.CountAsync(h => h.IdEquipo == id);

            TotalRegistros = AsignacionesCount + ReportesCount + MantenimientosCount + 
                           vehiculosCount + electronicosCount + mobiliariosCount + herramientasCount;

            // Cargar últimos 5 reportes
            UltimosReportes = await _context.Reportes
                .Include(r => r.IdCausaNavigation)
                .Include(r => r.IdEmpleadoNavigation)
                .Where(r => r.IdEquipo == id)
                .OrderByDescending(r => r.FechaCommit)
                .Take(5)
                .ToListAsync();

            return Page();
        }
    }
}