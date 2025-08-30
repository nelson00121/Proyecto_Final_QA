using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Reportes
{
    public class IndexModel : PageModel
    {
        private readonly BodegaContext _context;

        public IndexModel(BodegaContext context)
        {
            _context = context;
        }

        public IList<Reporte> Reportes { get; set; } = default!;
        public IList<Empleado> EmpleadosDisponibles { get; set; } = default!;
        public IList<Equipo> EquiposDisponibles { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? EmpleadoFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? EquipoFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? FechaFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }
        
        public string DateSortParm => String.IsNullOrEmpty(SortOrder) ? "date_desc" : "";

        public async Task OnGetAsync()
        {
            // Cargar listas para filtros
            EmpleadosDisponibles = await _context.Empleados
                .Where(e => e.Estado == "activo")
                .OrderBy(e => e.PrimerApellido)
                .ToListAsync();

            EquiposDisponibles = await _context.Equipos
                .OrderBy(e => e.Identificador)
                .ToListAsync();

            IQueryable<Reporte> reportesIQ = _context.Reportes
                .Include(r => r.IdEmpleadoNavigation)
                .Include(r => r.IdEquipoNavigation)
                .Include(r => r.IdCausaNavigation)
                .Include(r => r.Imagens);

            // Filtros
            if (!String.IsNullOrEmpty(EmpleadoFilter) && int.TryParse(EmpleadoFilter, out int empleadoId))
            {
                reportesIQ = reportesIQ.Where(r => r.IdEmpleado == empleadoId);
            }

            if (!String.IsNullOrEmpty(EquipoFilter) && int.TryParse(EquipoFilter, out int equipoId))
            {
                reportesIQ = reportesIQ.Where(r => r.IdEquipo == equipoId);
            }

            // Filtro por fecha
            if (!String.IsNullOrEmpty(FechaFilter))
            {
                var hoy = DateTime.Today;
                switch (FechaFilter)
                {
                    case "hoy":
                        reportesIQ = reportesIQ.Where(r => r.FechaCommit.HasValue && r.FechaCommit.Value.Date == hoy);
                        break;
                    case "semana":
                        var inicioSemana = hoy.AddDays(-(int)hoy.DayOfWeek);
                        reportesIQ = reportesIQ.Where(r => r.FechaCommit.HasValue && r.FechaCommit.Value.Date >= inicioSemana);
                        break;
                    case "mes":
                        var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
                        reportesIQ = reportesIQ.Where(r => r.FechaCommit.HasValue && r.FechaCommit.Value.Date >= inicioMes);
                        break;
                }
            }

            // Ordenamiento
            switch (SortOrder)
            {
                case "date_desc":
                    reportesIQ = reportesIQ.OrderByDescending(r => r.FechaCommit);
                    break;
                default:
                    reportesIQ = reportesIQ.OrderBy(r => r.FechaCommit);
                    break;
            }

            Reportes = await reportesIQ.AsNoTracking().ToListAsync();
        }
    }
}